using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.LogData.Methods.ParameterInfos;

namespace TNO.Logging.Reading.LogData.Methods.ParameterInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IParameterInfo"/>, version #0.
/// </summary>
public sealed class ParameterInfoDeserialiser0 : IParameterInfoDeserialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

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
