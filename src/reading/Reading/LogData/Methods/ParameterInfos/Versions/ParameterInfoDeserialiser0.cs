using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.Methods.ParameterInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IParameterInfo"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.ParameterInfo)]
public sealed class ParameterInfoDeserialiser0 : IDeserialiser<IParameterInfo>
{
   #region Methods
   /// <inheritdoc/>
   public IParameterInfo Deserialise(BinaryReader reader)
   {
      string name = reader.ReadString();
      byte rawModifier = reader.ReadByte();
      ParameterModifier modifier = (ParameterModifier)rawModifier;
      bool hasDefaultValue = reader.ReadBoolean();
      ulong typeId = reader.ReadUInt64();

      return ParameterInfoFactory.Version0(
         typeId,
         modifier,
         hasDefaultValue,
         name);
   }
   #endregion
}
