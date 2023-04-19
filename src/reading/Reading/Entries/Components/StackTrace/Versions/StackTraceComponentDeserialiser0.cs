using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Entries.Components.StackTrace.Versions;

/// <summary>
/// A deserialiser for <see cref="IStackTraceComponent"/>, version #0.
/// </summary>
[Version(0)]
public sealed class StackTraceComponentDeserialiser0 : IDeserialiser<IStackTraceComponent>
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackTraceComponentDeserialiser0"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>
   public StackTraceComponentDeserialiser0(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IStackTraceComponent Deserialise(BinaryReader reader)
   {
      _deserialiser.Deserialise(reader, out IStackTraceInfo stackTraceInfo);

      return StackTraceComponentFactory.Version0(stackTraceInfo);
   }
   #endregion
}
