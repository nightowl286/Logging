using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.EntryLink;

namespace TNO.Logging.Reading.Entries.Components.EntryLink.Versions;

/// <summary>
/// A deserialiser for <see cref="IEntryLinkComponent"/>, version #0.
/// </summary>
public sealed class EntryLinkComponentDeserialiser0 : IEntryLinkComponentDeserialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IEntryLinkComponent Deserialise(BinaryReader reader)
   {
      ulong entryId = reader.ReadUInt64();

      return EntryLinkComponentFactory.Version0(entryId);
   }
   #endregion
}