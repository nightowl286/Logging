using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IAssemblyComponent"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.Assembly)]
public sealed class AssemblyComponentSerialiser : ISerialiser<IAssemblyComponent>
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IAssemblyComponent data)
   {
      ulong id = data.AssemblyId;
      writer.Write(id);
   }

   /// <inheritdoc/>
   public int Count(IAssemblyComponent data) => sizeof(ulong);
   #endregion
}