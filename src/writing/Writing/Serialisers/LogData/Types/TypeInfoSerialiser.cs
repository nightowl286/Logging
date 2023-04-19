using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.Types;

/// <summary>
/// A serialiser for <see cref="ITypeInfo"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.TypeInfo)]
public class TypeInfoSerialiser : ISerialiser<ITypeInfo>
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ITypeInfo data)
   {
      ulong assemblyId = data.AssemblyId;
      ulong declaringTypeId = data.DeclaringTypeId;
      ulong baseTypeId = data.BaseTypeId;
      ulong elementTypeId = data.ElementTypeId;
      ulong genericTypeDefinitionId = data.GenericTypeDefinitionId;

      string name = data.Name;
      string fullName = data.FullName;
      string @namespace = data.Namespace;

      IReadOnlyList<ulong> genericTypeIds = data.GenericTypeIds;

      writer.Write(assemblyId);
      writer.Write(declaringTypeId);
      writer.Write(baseTypeId);
      writer.Write(elementTypeId);
      writer.Write(genericTypeDefinitionId);

      writer.Write(name);
      writer.Write(fullName);
      writer.Write(@namespace);

      writer.Write7BitEncodedInt(genericTypeIds.Count);
      foreach (ulong genericTypeId in genericTypeIds)
         writer.Write(genericTypeId);
   }

   /// <inheritdoc/>
   public ulong Count(ITypeInfo data)
   {
      int size =
         sizeof(ulong) * 5;

      int nameSize = BinaryWriterSizeHelper.StringSize(data.Name);
      int fullNameSize = BinaryWriterSizeHelper.StringSize(data.FullName);
      int namespaceSize = BinaryWriterSizeHelper.StringSize(data.Namespace);
      int genericTypeIdsCountSize = BinaryWriterSizeHelper.Encoded7BitIntSize(data.GenericTypeIds.Count);
      int genericTypeIdsCount = sizeof(ulong) * data.GenericTypeIds.Count;

      return (ulong)(size + nameSize + fullNameSize + namespaceSize + genericTypeIdsCountSize + genericTypeIdsCount);
   }
   #endregion
}
