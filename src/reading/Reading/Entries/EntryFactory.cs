using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries;
using TNO.Logging.Reading.Abstractions.Entries;

namespace TNO.Logging.Reading.Entries;

/// <summary>
/// A factory that should be used in instances of the <see cref="IEntryDeserialiser"/>.
/// </summary>
internal static class EntryFactory
{
    #region Functions
    public static IEntry Version0(ulong id, Importance Importance, TimeSpan timestamp, ulong fileId, uint line, IReadOnlyDictionary<ComponentKind, IComponent> components)
       => new Entry(id, Importance, timestamp, fileId, line, components);
    #endregion
}
