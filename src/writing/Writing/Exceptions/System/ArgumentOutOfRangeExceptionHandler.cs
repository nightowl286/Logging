using System.Runtime.InteropServices;
using TNO.Logging.Common.Exceptions.Abstractions;
using TNO.Logging.Common.Exceptions.Abstractions.System;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Logging.Helpers.General;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Serialisers;

namespace TNO.Logging.Writing.Exceptions.System;

/// <summary>
/// Converts and serialises exceptions of the <see cref="ArgumentOutOfRangeException"/> type.
/// </summary>
[Guid(ExceptionGroups.System.ArgumentOutOfRangeException)]
public sealed class ArgumentOutOfRangeExceptionHandler : IExceptionDataHandler<ArgumentOutOfRangeException, IArgumentOutOfRangeExceptionData>
{
   #region Fields
   private readonly ILogWriteContext _writeContext;
   private readonly ILogDataCollector _dataCollector;
   private readonly IPrimitiveSerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ArgumentOutOfRangeExceptionHandler"/>.</summary>
   /// <param name="writeContext">The <see cref="ILogWriteContext"/> to use.</param>
   /// <param name="dataCollector">The <see cref="ILogDataCollector"/> to use.</param>
   /// <param name="serialiser">The <see cref="IPrimitiveSerialiser"/> to use.</param>
   public ArgumentOutOfRangeExceptionHandler(
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      IPrimitiveSerialiser serialiser)
   {
      _writeContext = writeContext;
      _dataCollector = dataCollector;
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IArgumentOutOfRangeExceptionData Convert(ArgumentOutOfRangeException exception)
   {
      object? convertedValue = PrimitiveValueHelper.Convert(_writeContext, _dataCollector, exception.ActualValue);

      return new ArgumentOutOfRangeExceptionData(exception.ParamName, convertedValue);
   }

   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IArgumentOutOfRangeExceptionData data)
   {
      string? param = data.ParameterName;
      object? value = data.ActualValue;

      if (writer.TryWriteNullable(param))
         writer.Write(param);

      _serialiser.Serialise(writer, value);
   }

   /// <inheritdoc/>
   public int Count(IArgumentOutOfRangeExceptionData data)
   {
      int paramNameSize = BinaryWriterSizeHelper.StringSize(data.ParameterName);
      int valueSize = _serialiser.Count(data.ActualValue);

      return paramNameSize + valueSize + sizeof(bool);
   }
   #endregion
}
