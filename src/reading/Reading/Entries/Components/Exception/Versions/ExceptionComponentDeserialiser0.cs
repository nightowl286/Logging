using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Entries.Components.Exception.Versions;

/// <summary>
/// A deserialiser for <see cref="IExceptionComponent"/>, version #0.
/// </summary>
[Version(0)]
public sealed class ExceptionComponentDeserialiser0 : IDeserialiser<IExceptionComponent>
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionComponentDeserialiser0"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>
   public ExceptionComponentDeserialiser0(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IExceptionComponent Deserialise(BinaryReader reader)
   {
      _deserialiser.Deserialise(reader, out IExceptionInfo exceptionInfo);

      return ExceptionComponentFactory.Version0(exceptionInfo);
   }
   #endregion
}