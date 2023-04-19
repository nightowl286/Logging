using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.ContextInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="ContextInfo"/>, version #0.
/// </summary>
[Version(0)]
public sealed class ContextInfoDeserialiser0 : IDeserialiser<ContextInfo>
{
   #region Methods
   /// <inheritdoc/>
   public ContextInfo Deserialise(BinaryReader reader)
   {
      ulong id = reader.ReadUInt64();
      ulong parentId = reader.ReadUInt64();
      string name = reader.ReadString();
      ulong fileId = reader.ReadUInt64();
      uint line = reader.ReadUInt32();

      return ContextInfoFactory.Version0(name, id, parentId, fileId, line);
   }
   #endregion
}
