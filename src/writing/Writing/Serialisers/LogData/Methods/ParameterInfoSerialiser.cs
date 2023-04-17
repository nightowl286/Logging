using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Parameters;

namespace TNO.Logging.Writing.Serialisers.LogData.Methods;

/// <summary>
/// A serialiser for <see cref="IParameterInfo"/>.
/// </summary>
[Version(0)]
public class ParameterInfoSerialiser : IParameterInfoSerialiser
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IParameterInfo data)
   {
      string name = data.Name;
      ParameterModifier modifier = data.Modifier;
      byte rawModifier = (byte)modifier;
      bool hasDefaultValue = data.HasDefaultValue;
      ulong typeId = data.TypeId;

      writer.Write(name);
      writer.Write(rawModifier);
      writer.Write(hasDefaultValue);
      writer.Write(typeId);
   }

   /// <inheritdoc/>
   public ulong Count(IParameterInfo data)
   {
      int size =
         sizeof(byte) +
         sizeof(ulong) +
         sizeof(bool);

      int nameSize = BinaryWriterSizeHelper.StringSize(data.Name);

      return (ulong)(size + nameSize);
   }
   #endregion
}
