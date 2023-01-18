using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Entries.Components.Kinds;
internal abstract record StackFrameInfo(
   ulong FileReference,
   int Line,
   int Column) : IStackFrameInfo;
