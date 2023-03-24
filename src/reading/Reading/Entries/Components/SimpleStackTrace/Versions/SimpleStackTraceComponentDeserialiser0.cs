using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.SimpleStackTrace;

namespace TNO.Logging.Reading.Entries.Components.SimpleStackTrace.Versions;

/// <summary>
/// A deserialiser for <see cref="ISimpleStackTraceComponent"/>, version #0.
/// </summary>
public sealed class SimpleStackTraceComponentDeserialiser0 : ISimpleStackTraceComponentDeserialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ISimpleStackTraceComponent Deserialise(BinaryReader reader)
   {
      int threadId = reader.ReadInt32();
      string stackTrace = reader.ReadString();

      return SimpleStackTraceComponentFactory.Version0(stackTrace, threadId);
   }
   #endregion
}
