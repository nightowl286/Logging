using System.Threading;

namespace TNO.Logging.Common.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ComponentKind.SimpleStackTrace"/> component.
/// </summary>
public interface ISimpleStackTraceComponent : IComponent
{
   #region Properties
   /// <summary>The stack trace information.</summary>
   string StackTrace { get; }

   /// <summary>
   /// The <see cref="Thread.ManagedThreadId"/> of the 
   /// thread that the <see cref="StackTrace"/> is from.
   /// </summary>
   int ThreadId { get; }
   #endregion
}
