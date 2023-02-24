using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Thread;

/// <summary>
/// A factory class that should be used instances of the <see cref="IThreadComponentDeserialiser"/>.
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