namespace TNO.Logging.Common.Abstractions;

/// <summary>
/// Denotes that the implementing type is versioned.
/// </summary>
public interface IVersioned
{
   #region Properties
   /// <summary>The version of this data.</summary>
   uint Version { get; }
   #endregion
}
