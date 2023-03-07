using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IAssemblyComponent"/>.
/// </summary>
public sealed class AssemblyComponentSerialiser : IAssemblyComponentSerialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

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