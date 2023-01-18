using TNO.Common.Abstractions.Components;
using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Entries.Components.Kinds;

internal record ThreadComponent(
   string? Name,
   int Id,
   ApartmentState ApartmentState,
   ThreadPriority Priority,
   bool IsBackgroundThread,
   bool IsThreadPoolThread,
   bool IsAlive,
   ThreadState State) : IEntryComponent, IThreadComponent
{
   #region Properties
   public ComponentKind Kind => ComponentKind.Thread;
   #endregion

   #region Functions
   public static ThreadComponent FromThread(Thread thread)
   {
      return new ThreadComponent(
         thread.Name,
         thread.ManagedThreadId,
         thread.GetApartmentState(),
         thread.Priority,
         thread.IsBackground,
         thread.IsThreadPoolThread,
         thread.IsAlive,
         thread.ThreadState);
   }
   #endregion
}
