using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData;

namespace TNO.Logging.Writing.Serialisers.LogData;

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
      ulong id = data.Id;
      ulong assemblyId = data.AssemblyId;
      ulong declaringTypeId = data.DeclaringTypeId;
      ulong baseTypeId = data.BaseTypeId;

      string name = data.Name;
      string fullName = data.FullName;
      string @namespace = data.Namespace;

      writer.Write(id);
      writer.Write(assemblyId);
      writer.Write(declaringTypeId);
      writer.Write(baseTypeId);

      writer.Write(name);
      writer.Write(fullName);
      writer.Write(@namespace);
   }

   /// <inheritdoc/>
   public ulong Count(ITypeInfo data)
   {
      int size =
         sizeof(ulong) * 4;

      int nameSize = BinaryWriterSizeHelper.StringSize(data.Name);
      int fullNameSize = BinaryWriterSizeHelper.StringSize(data.FullName);
      int namespaceSize = BinaryWriterSizeHelper.StringSize(data.Namespace);

      return (ulong)(size + nameSize + fullNameSize + namespaceSize);
   }
   #endregion
}
