namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// Denotes a basic log entry.
/// </summary>
public interface IBasicEntry
{
   #region Properties
   /// <summary>The id of this entry.</summary>
   ulong Id { get; }

   /// <summary>The kinds of the components that this entry contains.</summary>
   ComponentKind ComponentKinds { get; }
   #endregion
}
