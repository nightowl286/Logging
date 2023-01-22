namespace TNO.Common.Abstractions.Components.Kinds;

public interface ISimpleStackTraceComponent : IEntryComponent
{
   #region Properties
   string StackTrace { get; }
   #endregion
}
