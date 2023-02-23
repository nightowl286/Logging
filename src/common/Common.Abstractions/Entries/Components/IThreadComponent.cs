using System.Threading;

namespace TNO.Logging.Common.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ComponentKind.Thread"/> component.
/// </summary>
public interface IThreadComponent : IComponent
{
   #region Properties
   /// <summary>The managed id of the thread.</summary>
   /// <seealso cref="Thread.ManagedThreadId"/>
   int ManagedId { get; }

   /// <summary>The optional name of the thread.</summary>
   /// <seealso cref="Thread.Name"/>
   string? Name { get; }

   /// <summary>The current state of the thread.</summary>
   /// <seealso cref="Thread.ThreadState"/>
   ThreadState State { get; }

   /// <summary>Whether the thread belongs to a thread pool.</summary>
   /// <seealso cref="Thread.IsThreadPoolThread"/>
   bool IsThreadPoolThread { get; }

   /// <summary>The priority of the thread.</summary>
   /// <seealso cref="Thread.Priority"/>.
   ThreadPriority Priority { get; }

   /// <summary>The apartment state of the thread.</summary>
   /// <seealso cref="Thread.ApartmentState"/>
   ApartmentState ApartmentState { get; }
   #endregion
}
