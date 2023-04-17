using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Entries.Components.StackTrace;
using TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackTraceInfos;

namespace TNO.Logging.Reading.Entries.Components.StackTrace.Versions;

/// <summary>
/// A deserialiser for <see cref="IStackTraceComponent"/>, version #0.
/// </summary>
[Version(0)]
public sealed class StackTraceComponentDeserialiser0 : IStackTraceComponentDeserialiser
{
   #region Fields
   private readonly IStackTraceInfoDeserialiser _stackTraceInfoDeserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackTraceComponentDeserialiser0"/>.</summary>
   /// <param name="stackTraceInfoDeserialiser">The <see cref="IStackTraceInfoDeserialiser"/> to use.</param>
   public StackTraceComponentDeserialiser0(IStackTraceInfoDeserialiser stackTraceInfoDeserialiser)
   {
      _stackTraceInfoDeserialiser = stackTraceInfoDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IStackTraceComponent Deserialise(BinaryReader reader)
   {
      IStackTraceInfo stackTraceInfo = _stackTraceInfoDeserialiser.Deserialise(reader);

      return StackTraceComponentFactory.Version0(stackTraceInfo);
   }
   #endregion
}
