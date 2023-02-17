using TNO.Logging.Common.Abstractions;
using TNO.Logging.Reading.Abstractions.ContextInfos;

namespace TNO.Logging.Reading.ContextInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="ContextInfo"/>, version #0.
/// </summary>
public sealed class ContextInfoDeserialiser0 : IContextInfoDeserialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ContextInfo Deserialise(BinaryReader reader)
   {
      ulong id = reader.ReadUInt64();
      string name = reader.ReadString();
      ulong fileId = reader.ReadUInt64();
      uint line = reader.ReadUInt32();

      return ContextInfoFactory.Version0(name, id, fileId, line);
   }
   #endregion
}
