namespace TNO.Common.Abstractions.Components.Kinds;

public interface IStackFrameInfo
{
   #region Properties
   ulong FileReference { get; }
   int Line { get; }
   int Column { get; }
   #endregion
}
