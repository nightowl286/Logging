using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="ITypeComponent"/>.
/// </summary>
public sealed class TypeComponentSerialiser : ITypeComponentSerialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

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