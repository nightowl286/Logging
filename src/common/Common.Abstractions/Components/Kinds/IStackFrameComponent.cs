namespace TNO.Common.Abstractions.Components.Kinds;
public interface IStackFrameComponent : IStackFrameInfo, IEntryComponent
{
   #region Properties
   int ThreadId { get; }
   #endregion
}
