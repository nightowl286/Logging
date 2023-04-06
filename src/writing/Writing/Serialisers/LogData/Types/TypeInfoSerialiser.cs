using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Types;

namespace TNO.Logging.Writing.Serialisers.LogData.Types;

/// <summary>
/// A serialiser for <see cref="ITypeInfo"/>.
/// </summary>
public class TypeInfoSerialiser : ITypeInfoSerialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ITypeInfo data)
   {
      ulong assemblyId = data.AssemblyId;
      ulong declaringTypeId = data.DeclaringTypeId;
      ulong baseTypeId = data.BaseTypeId;

      string name = data.Name;
      string fullName = data.FullName;
      string @namespace = data.Namespace;

      IReadOnlyList<ulong> genericTypeIds = data.GenericTypeIds;

      writer.Write(assemblyId);
      writer.Write(declaringTypeId);
      writer.Write(baseTypeId);

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
         sizeof(ulong) * 3;

      int nameSize = BinaryWriterSizeHelper.StringSize(data.Name);
      int fullNameSize = BinaryWriterSizeHelper.StringSize(data.FullName);
      int namespaceSize = BinaryWriterSizeHelper.StringSize(data.Namespace);
      int genericTypeIdsCountSize = BinaryWriterSizeHelper.Encoded7BitIntSize(data.GenericTypeIds.Count);
      int genericTypeIdsCount = sizeof(ulong) * data.GenericTypeIds.Count;

      return (ulong)(size + nameSize + fullNameSize + namespaceSize + genericTypeIdsCountSize + genericTypeIdsCount);
   }
   #endregion
}
