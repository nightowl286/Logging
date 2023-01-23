using TNO.Logging.Common.Abstractions.Entries;

namespace TNO.Logging.Common.Entries;

/// <summary>
/// Represents an <see cref="IBasicEntry"/>.
/// </summary>
/// <param name="Id">The id of the entry.</param>
/// <param name="ComponentKinds">The kinds of the components that this entry contains.</param>
public record class BasicEntry(ulong Id, ComponentKind ComponentKinds) : IBasicEntry;
