namespace TNO.Logging.Common.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ComponentKind.Message"/> component.
/// </summary>
public interface IMessageComponent : IComponent
{
   #region Properties
   /// <summary>The message of this component.</summary>
   string Message { get; }
   #endregion
}