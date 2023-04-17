using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IAssemblyComponent"/>.
/// </summary>
[Version(0)]
public sealed class AssemblyComponentSerialiser : IAssemblyComponentSerialiser
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IAssemblyComponent data)
   {
      ulong id = data.AssemblyId;
      writer.Write(id);
   }

   /// <inheritdoc/>
   public ulong Count(IAssemblyComponent data) => sizeof(ulong);
   #endregion
}