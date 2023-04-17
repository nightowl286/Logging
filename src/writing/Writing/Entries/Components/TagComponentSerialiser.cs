using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="ITagComponent"/>.
/// </summary>
[Version(0)]
public sealed class TagComponentSerialiser : ITagComponentSerialiser
{
   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ITagComponent data)
   {
      ulong id = data.TagId;
      writer.Write(id);
   }

   /// <inheritdoc/>
   public ulong Count(ITagComponent data) => sizeof(ulong);
   #endregion
}