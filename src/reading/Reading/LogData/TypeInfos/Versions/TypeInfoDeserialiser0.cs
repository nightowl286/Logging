using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Reading.Abstractions.LogData.TypeInfos;

namespace TNO.Logging.Reading.LogData.TypeInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="ITypeInfo"/>, version #0.
/// </summary>
public sealed class TypeInfoDeserialiser0 : ITypeInfoDeserialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ITypeInfo Deserialise(BinaryReader reader)
   {
      ulong id = reader.ReadUInt64();
      ulong assemblyId = reader.ReadUInt64();
      ulong declaringTypeId = reader.ReadUInt64();
      ulong baseTypeId = reader.ReadUInt64();

      string name = reader.ReadString();
      string fullName = reader.ReadString();
      string @namespace = reader.ReadString();

      return TypeInfoFactory.Version0(
         id,
         assemblyId,
         baseTypeId,
         declaringTypeId,
         name,
         fullName,
         @namespace);
   }
   #endregion
}
