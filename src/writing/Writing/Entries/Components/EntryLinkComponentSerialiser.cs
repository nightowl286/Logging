using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IEntryLinkComponent"/>.
/// </summary>
public sealed class EntryLinkComponentSerialiser : IEntryLinkComponentSerialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IEntryLinkComponent data)
   {
      ulong id = data.EntryId;

      writer.Write(id);
   }

   /// <inheritdoc/>
   public ulong Count(IEntryLinkComponent data) => sizeof(ulong);
   #endregion
}
