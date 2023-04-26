using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Thread"/> component.
/// </summary>
public class ThreadComponent : IThreadComponent
{
   #region Properties
   /// <inheritdoc/>
   public int ManagedId { get; }

   /// <inheritdoc/>
   public string Name { get; }

   /// <inheritdoc/>
   public ThreadState State { get; }

   /// <inheritdoc/>
   public bool IsThreadPoolThread { get; }

   /// <inheritdoc/>
   public ThreadPriority Priority { get; }

   /// <inheritdoc/>
   public ApartmentState ApartmentState { get; }

   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Thread;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ThreadComponent"/>.</summary>
   /// <param name="managedId">The managed id of the thread.</param>
   /// <param name="name">The optional name of the thread.</param>
   /// <param name="state">The current state of the thread.</param>
   /// <param name="isThreadPoolThread">Whether the thread belongs to a thread pool.</param>
   /// <param name="priority">The priority of the thread.</param>
   /// <param name="apartmentState">The apartment state of the thread.</param>
   public ThreadComponent(
      int managedId,
      string name,
      ThreadState state,
      bool isThreadPoolThread,
      ThreadPriority priority,
      ApartmentState apartmentState)
   {
      ManagedId = managedId;
      Name = name;
      State = state;
      IsThreadPoolThread = isThreadPoolThread;
      Priority = priority;
      ApartmentState = apartmentState;
   }
   #endregion

   #region Functions
   /// <summary>Creates a <see cref="ThreadComponent"/> from the given <paramref name="thread"/>.</summary>
   /// <param name="thread">The thread to extract information from.</param>
   /// <returns>The newly created <see cref="ThreadComponent"/>.</returns>
   public static ThreadComponent FromThread(Thread thread)
   {
      ThreadComponent component = new ThreadComponent(
         thread.ManagedThreadId,
         thread.Name ?? string.Empty,
         thread.ThreadState,
         thread.IsBackground,
         thread.Priority,
         thread.GetApartmentState());

      return component;
   }
   #endregion
}
