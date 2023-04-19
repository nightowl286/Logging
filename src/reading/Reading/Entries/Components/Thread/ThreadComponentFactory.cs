using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IThreadComponent"/>.
/// </summary>
internal static class ThreadComponentFactory
{
   #region Functions
   public static IThreadComponent Version0(
      int managedId,
      string name,
      ThreadState state,
      bool isThreadPoolThread,
      ThreadPriority priority,
      ApartmentState apartmentState)
   {
      return new ThreadComponent(
         managedId,
         name,
         state,
         isThreadPoolThread,
         priority,
         apartmentState);
   }
   #endregion
}