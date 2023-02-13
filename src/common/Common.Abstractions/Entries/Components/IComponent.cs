namespace TNO.Logging.Common.Abstractions.Entries.Components;

/// <summary>
/// Denotes a log entry component.
/// </summary>
public interface IComponent
{
   #region Properties
   /// <summary>The kind of this component.</summary>
   ComponentKind Kind { get; }
   #endregion
}