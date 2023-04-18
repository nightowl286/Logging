using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Entries.Components.Exception;
using TNO.Logging.Reading.Abstractions.Exceptions.ExceptionInfos;

namespace TNO.Logging.Reading.Entries.Components.Exception.Versions;

/// <summary>
/// A deserialiser for <see cref="IExceptionComponent"/>, version #0.
/// </summary>
[Version(0)]
public sealed class ExceptionComponentDeserialiser0 : IExceptionComponentDeserialiser
{
   #region Fields
   private readonly IExceptionInfoDeserialiser _exceptionInfoDeserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionComponentDeserialiser0"/>.</summary>
   /// <param name="exceptionInfoDeserialiser">The <see cref="IExceptionInfoDeserialiser"/> to use.</param>
   public ExceptionComponentDeserialiser0(IExceptionInfoDeserialiser exceptionInfoDeserialiser)
   {
      _exceptionInfoDeserialiser = exceptionInfoDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IExceptionComponent Deserialise(BinaryReader reader)
   {
      IExceptionInfo exceptionInfo = _exceptionInfoDeserialiser.Deserialise(reader);

      return ExceptionComponentFactory.Version0(exceptionInfo);
   }
   #endregion
}