using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="ITypeComponent"/>.
/// </summary>
[Version(0)]
public sealed class TypeComponentSerialiser : ITypeComponentSerialiser
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