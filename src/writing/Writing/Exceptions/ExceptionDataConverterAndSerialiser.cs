using System.Diagnostics;
using System.Linq.Expressions;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Loggers;

namespace TNO.Logging.Writing.Exceptions;

/// <summary>
/// Represents an <see cref="IExceptionDataConverter"/> and an <see cref="IExceptionDataSerialiser"/>.
/// </summary>
public class ExceptionDataConverterAndSerialiser : IExceptionDataConverter, IExceptionDataSerialiser
{
   private delegate IExceptionData ConversionDelegate(object converter, Exception exception);
   private delegate ulong CountDelegate(object serialiser, IExceptionData exceptionData);
   private delegate void SerialiseDelegate(object serialiser, BinaryWriter writer, IExceptionData exceptionData);

   #region Fields
   private readonly ExceptionGroupStore _exceptionGroupStore;
   private readonly ILogWriteContext _writeContext;
   private readonly ILogDataCollector _dataCollector;
   private readonly ILogger _internalLogger;

   private readonly Dictionary<Type, ConversionDelegate> _conversionCache = new Dictionary<Type, ConversionDelegate>();
   private readonly Dictionary<Type, CountDelegate> _countCache = new Dictionary<Type, CountDelegate>();
   private readonly Dictionary<Type, SerialiseDelegate> _serialisationCache = new Dictionary<Type, SerialiseDelegate>();

   private readonly Dictionary<Type, object> _convertersCache = new Dictionary<Type, object>();
   private readonly Dictionary<Type, object> _serialisersCache = new Dictionary<Type, object>();
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionDataConverterAndSerialiser"/>.</summary>
   /// <param name="exceptionGroupStore">The <see cref="ExceptionGroupStore"/> to use.</param>
   /// <param name="writeContext">The <see cref="ILogWriteContext"/> to use.</param>
   /// <param name="dataCollector">The <see cref="ILogDataCollector"/> to use.</param>
   /// <param name="internalLogger">The <see cref="ILogger"/> to use as the internal logger.</param>
   public ExceptionDataConverterAndSerialiser(
      ExceptionGroupStore exceptionGroupStore,
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      ILogger internalLogger)
   {
      _exceptionGroupStore = exceptionGroupStore;
      _writeContext = writeContext;
      _dataCollector = dataCollector;
      _internalLogger = internalLogger;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IExceptionData Convert(Exception exception, out ulong exceptionDataTypeId, out Guid exceptionGroupId)
   {
      Type exceptionType = exception.GetType();

      GetDataConverter(exceptionType, out Type dataExceptionType, out Type converterType, out exceptionGroupId);
      if (exceptionType != dataExceptionType && _writeContext.ShouldLogUnknownException(exceptionType))
      {
         _internalLogger
            .StartEntry(Severity.Substantial | Purpose.Diagnostics)
            .WithTag(CommonTags.UnknownExceptionType)
            .FinishEntry();
      }

      exceptionDataTypeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(_writeContext, _dataCollector, dataExceptionType);
      ConversionDelegate conversionDelegate = GetConversionDelegate(converterType, dataExceptionType);
      object converter = _convertersCache[converterType];

      return conversionDelegate.Invoke(converter, exception);
   }

   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IExceptionData data, Guid exceptionGroupId)
   {
      bool hasGroup = _exceptionGroupStore.TryGet(exceptionGroupId, out ExceptionGroup? group);
      Debug.Assert(hasGroup && group is not null);

      CountDelegate countDelegate = GetCountDelegate(group.SerialiserType, group.ExceptionDataType);
      object serialiser = _serialisersCache[group.SerialiserType];

      ulong count = countDelegate.Invoke(serialiser, data);
      writer.Write(count);

      SerialiseDelegate serialiseDelegate = GetSerialiseDelegate(group.SerialiserType, group.ExceptionDataType);
      serialiseDelegate.Invoke(serialiser, writer, data);
   }

   /// <inheritdoc/>
   public ulong Count(IExceptionData data, Guid exceptionGroupId)
   {
      bool hasGroup = _exceptionGroupStore.TryGet(exceptionGroupId, out ExceptionGroup? group);
      Debug.Assert(hasGroup && group is not null);

      CountDelegate countDelegate = GetCountDelegate(group.SerialiserType, group.ExceptionDataType);
      object serialiser = _serialisersCache[group.SerialiserType];

      return countDelegate.Invoke(serialiser, data);
   }
   #endregion

   #region Helpers
   private ConversionDelegate GetConversionDelegate(Type converterType, Type dataExceptionType)
   {
      if (_conversionCache.TryGetValue(converterType, out ConversionDelegate? conversionDelegate) == false)
      {
         object converter = Activator.CreateInstance(converterType)!;
         _convertersCache.Add(converterType, converter);

         conversionDelegate = GenerateConversionDelegate(converterType, dataExceptionType);
      }

      return conversionDelegate;
   }
   private CountDelegate GetCountDelegate(Type serialiserType, Type exceptionDataType)
   {
      if (_countCache.TryGetValue(serialiserType, out CountDelegate? countDelegate) == false)
      {
         if (_serialisersCache.ContainsKey(serialiserType) == false)
            _serialisersCache.Add(serialiserType, Activator.CreateInstance(serialiserType)!);

         countDelegate = GenerateCountDelegate(serialiserType, exceptionDataType);
      }

      return countDelegate;
   }
   private SerialiseDelegate GetSerialiseDelegate(Type serialiserType, Type exceptionDataType)
   {
      if (_serialisationCache.TryGetValue(serialiserType, out SerialiseDelegate? serialiseDelegate) == false)
      {
         if (_serialisersCache.ContainsKey(serialiserType) == false)
            _serialisersCache.Add(serialiserType, Activator.CreateInstance(serialiserType)!);

         serialiseDelegate = GenerateSerialiseDelegate(serialiserType, exceptionDataType);
      }

      return serialiseDelegate;
   }
   private ConversionDelegate GenerateConversionDelegate(Type converterType, Type dataExceptionType)
   {
      ParameterExpression converterParam = Expression.Parameter(typeof(object), "converter");
      ParameterExpression exceptionParam = Expression.Parameter(typeof(Exception), "exception");

      Expression typedConverter = Expression.Convert(converterParam, converterType);
      Expression typedException = Expression.Convert(exceptionParam, dataExceptionType);

      Expression convertCall = Expression.Call(
         typedConverter,
         nameof(IExceptionDataConverter<Exception, IExceptionData>.Convert),
         null,
         typedException);

      Expression<ConversionDelegate> expression = Expression.Lambda<ConversionDelegate>(convertCall, converterParam, exceptionParam);
      return expression.Compile();
   }
   private CountDelegate GenerateCountDelegate(Type serialiserType, Type exceptionDataType)
   {
      ParameterExpression serialiserParam = Expression.Parameter(typeof(object), "serialiser");
      ParameterExpression exceptionDataParam = Expression.Parameter(typeof(IExceptionData), "exceptionData");

      Expression typedSerialiser = Expression.Convert(serialiserParam, serialiserType);
      Expression typedExceptionData = Expression.Convert(exceptionDataParam, exceptionDataType);

      Expression countCall = Expression.Call(
         typedSerialiser,
         nameof(IExceptionDataSerialiser<IExceptionData>.Count),
         null,
         typedExceptionData);

      Expression<CountDelegate> expression = Expression.Lambda<CountDelegate>(countCall, serialiserParam, exceptionDataParam);
      return expression.Compile();
   }
   private SerialiseDelegate GenerateSerialiseDelegate(Type serialiserType, Type exceptionDataType)
   {
      ParameterExpression writerParam = Expression.Parameter(typeof(BinaryWriter), "writer");
      ParameterExpression serialiserParam = Expression.Parameter(typeof(object), "serialiser");
      ParameterExpression exceptionDataParam = Expression.Parameter(typeof(IExceptionData), "exceptionData");

      Expression typedSerialiser = Expression.Convert(serialiserParam, serialiserType);
      Expression typedExceptionData = Expression.Convert(exceptionDataParam, exceptionDataType);

      Expression serialiseCall = Expression.Call(
         typedSerialiser,
         nameof(IExceptionDataSerialiser<IExceptionData>.Serialise),
         null,
         writerParam,
         typedExceptionData);

      Expression<SerialiseDelegate> expression = Expression.Lambda<SerialiseDelegate>(serialiseCall, serialiserParam, writerParam, exceptionDataParam);
      return expression.Compile();
   }
   private void GetDataConverter(Type exceptionType, out Type dataExceptionType, out Type converterType, out Guid exceptionGroupId)
   {
      Type originalExceptionType = exceptionType;
      while (true)
      {
         if (_exceptionGroupStore.TryGet(exceptionType, out ExceptionGroup? group))
         {
            converterType = group.ConverterType;
            exceptionGroupId = group.GroupId;
            dataExceptionType = exceptionType;
            return;
         }

         if (exceptionType.BaseType == typeof(object) || exceptionType.BaseType is null)
            throw new Exception($"No data converter found for the given exception type ({originalExceptionType}). This should never happen.");

         exceptionType = exceptionType.BaseType;
      }
   }
   #endregion
}
