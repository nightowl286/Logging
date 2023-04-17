using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.LogData.TypeInfos;

namespace TNO.Logging.Reading.LogData.TypeInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="ITypeInfo"/>, version #0.
/// </summary>
[Version(0)]
public sealed class TypeInfoDeserialiser0 : ITypeInfoDeserialiser
{
   #region Methods
   /// <inheritdoc/>
   public ITypeInfo Deserialise(BinaryReader reader)
   {
      ulong assemblyId = reader.ReadUInt64();
      ulong declaringTypeId = reader.ReadUInt64();
      ulong baseTypeId = reader.ReadUInt64();
      ulong elementTypeId = reader.ReadUInt64();
      ulong genericTypeDefinitionId = reader.ReadUInt64();

      string name = reader.ReadString();
      string fullName = reader.ReadString();
      string @namespace = reader.ReadString();

      int genericTypeIdsCount = reader.Read7BitEncodedInt();
      List<ulong> genericTypeIds = new List<ulong>(genericTypeIdsCount);
      for (int i = 0; i < genericTypeIdsCount; i++)
      {
         ulong genericTypeId = reader.ReadUInt64();
         genericTypeIds.Add(genericTypeId);
      }

      return TypeInfoFactory.Version0(
         assemblyId,
         baseTypeId,
         declaringTypeId,
         elementTypeId,
         genericTypeDefinitionId,
         name,
         fullName,
         @namespace,
         genericTypeIds);
   }
   #endregion
}
