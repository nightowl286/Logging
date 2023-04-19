using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="ITypeComponent"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.Type)]
public sealed class TypeComponentSerialiser : ISerialiser<ITypeComponent>
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ITypeComponent data)
   {
      ulong id = data.TypeId;
      writer.Write(id);
   }

   /// <inheritdoc/>
   public ulong Count(ITypeComponent data) => sizeof(ulong);
   #endregion
}