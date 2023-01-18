using System.Threading;

namespace TNO.Common.Abstractions.Components.Kinds;
public interface IThreadComponent : IEntryComponent
{
   #region Properties
   string? Name { get; }
   int Id { get; }
   ApartmentState ApartmentState { get; }
   ThreadPriority Priority { get; }
   bool IsBackgroundThread { get; }
   bool IsThreadPoolThread { get; }
   bool IsAlive { get; }
   ThreadState State { get; }
   #endregion
}
