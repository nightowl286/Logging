using System.Collections.Generic;
using TNO.Common.Abstractions;
using TNO.Common.Abstractions.Components;

namespace TNO.Logging.Writer.Abstractions;
public interface ILogEntry
{
   #region Properties
   ulong Id { get; }
   ulong ContextId { get; }
   ulong FileId { get; }
   int Line { get; }
   Severity Severity { get; }
   IReadOnlyDictionary<ComponentKind, IEntryComponent> Components { get; }
   #endregion
}
