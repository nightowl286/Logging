using System.Diagnostics;
using System.Linq.Expressions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Abstractions;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Logging.Helpers;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Exceptions;

/// <summary>
/// Represents an <see cref="IExceptionDataHandler"/>.
/// </summary>
public class ExceptionDataHandler : IExceptionDataHandler
{
   #region Delegates
   private delegate IExceptionData ConversionDelegate(object handler, Exception exception);
   private delegate int CountDelegate(object handler, IExceptionData exceptionData);
   private delegate void SerialiseDelegate(object handler, BinaryWriter writer, IExceptionData exceptionData);
   #endregion

   #region Fields
   private readonly IExceptionDataHandlerRequester _requester;
   private readonly IServiceBuilder _builder;
   private readonly ILogWriteContext _writeContext;
   private readonly ILogDataCollector _dataCollector;
   private ILogger? _internalLogger;

   private readonly Dictionary<Type, ConversionDelegate> _conversionCache = new Dictionary<Type, ConversionDelegate>();
   private readonly Dictionary<Type, CountDelegate> _countCache = new Dictionary<Type, CountDelegate>();
   private readonly Dictionary<Type, SerialiseDelegate> _serialisationCache = new Dictionary<Type, SerialiseDelegate>();

   private readonly Dictionary<Type, object> _handlerCache = new Dictionary<Type, object>();
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionDataHandler"/>.</summary>
   /// <param name="requester">The <see cref="IExceptionDataHandlerRequester"/> to use.</param>
   /// <param name="builder">The <see cref="IServiceBuilder"/> to use.</param>
   /// <param name="writeContext">The <see cref="ILogWriteContext"/> to use.</param>
   /// <param name="dataCollector">The <see cref="ILogDataCollector"/> to use.</param>
   public ExceptionDataHandler(
      IExceptionDataHandlerRequester requester,
      IServiceBuilder builder,
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector)
   {
      _builder = builder;
      _requester = requester;
      _writeContext = writeContext;
      _dataCollector = dataCollector;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IExceptionData Convert(Exception exception, out ulong exceptionDataTypeId, out Guid handlerId)
   {
      Type exceptionType = exception.GetType();

      GetDataHandler(exceptionType, out Type foundExceptionType, out IExceptionDataHandlerInfo info);
      handlerId = info.Id;

      if (exceptionType != foundExceptionType && _writeContext.ShouldLogUnknownException(exceptionType))
      {
         _internalLogger?
            .StartEntry(Severity.Substantial | Purpose.Diagnostics)
            .WithTag(CommonTags.UnknownExceptionType)
            .FinishEntry();
      }

      exceptionDataTypeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(_writeContext, _dataCollector, foundExceptionType);
      ConversionDelegate conversionDelegate = GetConversionDelegate(info.HandlerType, foundExceptionType);
      object handler = _handlerCache[info.HandlerType];

      return conversionDelegate.Invoke(handler, exception);
   }

   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IExceptionData data, Guid exceptionGroupId)
   {
      bool hasInfo = _requester.ById(exceptionGroupId, out IExceptionDataHandlerInfo? info);
      Debug.Assert(hasInfo && info is not null);

      CountDelegate countDelegate = GetCountDelegate(info.HandlerType, info.ExceptionDataType);
      object handler = _handlerCache[info.HandlerType];

      int count = countDelegate.Invoke(handler, data);
      writer.Write(count);

      SerialiseDelegate serialiseDelegate = GetSerialiseDelegate(info.HandlerType, info.ExceptionDataType);
      serialiseDelegate.Invoke(handler, writer, data);
   }

   /// <inheritdoc/>
   public int Count(IExceptionData data, Guid exceptionGroupId)
   {
      bool hasInfo = _requester.ById(exceptionGroupId, out IExceptionDataHandlerInfo? info);
      Debug.Assert(hasInfo && info is not null);

      CountDelegate countDelegate = GetCountDelegate(info.HandlerType, info.ExceptionDataType);
      object handler = _handlerCache[info.HandlerType];

      return countDelegate.Invoke(handler, data);
   }

   /// <summary>Sets the internal logger to use.</summary>
   /// <param name="internalLogger">The logger to use as the internal logger.</param>
   public void SetInternalLogger(ILogger internalLogger) => _internalLogger = internalLogger;
   #endregion

   #region Helpers
   private ConversionDelegate GetConversionDelegate(Type handlerType, Type dataExceptionType)
   {
      if (_conversionCache.TryGetValue(handlerType, out ConversionDelegate? conversionDelegate) == false)
      {
         CacheHandlerInstance(handlerType);

         conversionDelegate = GenerateConversionDelegate(handlerType, dataExceptionType);
      }

      return conversionDelegate;
   }
   private CountDelegate GetCountDelegate(Type handlerType, Type exceptionDataType)
   {
      if (_countCache.TryGetValue(handlerType, out CountDelegate? countDelegate) == false)
      {
         if (_handlerCache.ContainsKey(handlerType) == false)
            CacheHandlerInstance(handlerType);

         countDelegate = GenerateCountDelegate(handlerType, exceptionDataType);
      }

      return countDelegate;
   }
   private SerialiseDelegate GetSerialiseDelegate(Type handlerType, Type exceptionDataType)
   {
      if (_serialisationCache.TryGetValue(handlerType, out SerialiseDelegate? serialiseDelegate) == false)
      {
         if (_handlerCache.ContainsKey(handlerType) == false)
            CacheHandlerInstance(handlerType);

         serialiseDelegate = GenerateSerialiseDelegate(handlerType, exceptionDataType);
      }

      return serialiseDelegate;
   }
   private ConversionDelegate GenerateConversionDelegate(Type converterType, Type dataExceptionType)
   {
      ParameterExpression converterParam = Expression.Parameter(typeof(object), "handler");
      ParameterExpression exceptionParam = Expression.Parameter(typeof(Exception), "exception");

      Expression typedConverter = Expression.Convert(converterParam, converterType);
      Expression typedException = Expression.Convert(exceptionParam, dataExceptionType);

      Expression convertCall = Expression.Call(
         typedConverter,
         nameof(IExceptionDataHandler<Exception, IExceptionData>.Convert),
         null,
         typedException);

      Expression<ConversionDelegate> expression = Expression.Lambda<ConversionDelegate>(convertCall, converterParam, exceptionParam);
      return expression.Compile();
   }
   private CountDelegate GenerateCountDelegate(Type serialiserType, Type exceptionDataType)
   {
      ParameterExpression serialiserParam = Expression.Parameter(typeof(object), "handler");
      ParameterExpression exceptionDataParam = Expression.Parameter(typeof(IExceptionData), "exceptionData");

      Expression typedSerialiser = Expression.Convert(serialiserParam, serialiserType);
      Expression typedExceptionData = Expression.Convert(exceptionDataParam, exceptionDataType);

      Expression countCall = Expression.Call(
         typedSerialiser,
         nameof(ISerialiser<IExceptionData>.Count),
         null,
         typedExceptionData);

      Expression<CountDelegate> expression = Expression.Lambda<CountDelegate>(countCall, serialiserParam, exceptionDataParam);
      return expression.Compile();
   }
   private SerialiseDelegate GenerateSerialiseDelegate(Type handlerType, Type exceptionDataType)
   {
      ParameterExpression writerParam = Expression.Parameter(typeof(BinaryWriter), "writer");
      ParameterExpression handlerParam = Expression.Parameter(typeof(object), "handler");
      ParameterExpression exceptionDataParam = Expression.Parameter(typeof(IExceptionData), "exceptionData");

      Expression typedHandler = Expression.Convert(handlerParam, handlerType);
      Expression typedExceptionData = Expression.Convert(exceptionDataParam, exceptionDataType);

      Expression serialiseCall = Expression.Call(
         typedHandler,
         nameof(ISerialiser<IExceptionData>.Serialise),
         null,
         writerParam,
         typedExceptionData);

      Expression<SerialiseDelegate> expression = Expression.Lambda<SerialiseDelegate>(serialiseCall, handlerParam, writerParam, exceptionDataParam);
      return expression.Compile();
   }
   private void GetDataHandler(Type exceptionType, out Type foundExceptionType, out IExceptionDataHandlerInfo info)
   {
      Type originalExceptionType = exceptionType;
      while (true)
      {
         if (_requester.ByExceptionType(exceptionType, out IExceptionDataHandlerInfo? potentialInfo))
         {
            info = potentialInfo;
            foundExceptionType = exceptionType;
            return;
         }

         if (exceptionType.BaseType == typeof(object) || exceptionType.BaseType is null)
         {
            string msg = $"No data converter found for the given exception type ({originalExceptionType}). This should never happen.";
            Debug.Fail(msg);
            throw new Exception(msg);
         }

         exceptionType = exceptionType.BaseType;
      }
   }
   private void CacheHandlerInstance(Type handlerType)
   {
      object handler = _builder.Build(handlerType);
      _handlerCache.Add(handlerType, handler);
   }
   #endregion
}
