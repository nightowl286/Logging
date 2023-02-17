using System.Diagnostics;

namespace TNO.Logging.Writing.Loggers.Writers;

/// <summary>A delegate that represents a write request.</summary>
/// <typeparam name="T">The type of the data.</typeparam>
/// <param name="data">The data that was requested to be written.</param>
public delegate void WriteRequestDelegate<T>(T data);

/// <summary>
/// Represents that a queue that will request writes for the enqueued data, on a separate thread.
/// </summary>
/// <typeparam name="T">The type of the data in this queue.</typeparam>
public sealed class ThreadedQueue<T> : IDisposable where T : notnull
{
   #region Fields
   private static readonly TimeSpan ThreadSleepTimeout = TimeSpan.FromMilliseconds(50);
   private readonly SemaphoreSlim _queueLock = new SemaphoreSlim(1);
   private readonly Queue<T> _queue = new Queue<T>();
   private readonly Thread _thread;
   private bool _disposeRequested;
   #endregion

   #region Constructors
   /// <summary>Created a new instance of the <see cref="ThreadedQueue{T}"/>.</summary>
   /// <param name="threadName">The name to give to the newly created thread.</param>
   /// <param name="priority">The thread priority to give to the newly created thread.</param>
   public ThreadedQueue(string threadName, ThreadPriority priority)
   {
      _thread = new Thread(ThreadLoop)
      {
         Name = threadName,
         Priority = priority
      };
      _thread.Start();
   }
   #endregion

   #region Events
   /// <summary>An event that is raised when a write operation is requested.</summary>
   public event WriteRequestDelegate<T>? WriteRequested;
   #endregion

   #region Methods
   /// <summary>Adds the given <paramref name="data"/> to the end of the queue.</summary>
   /// <param name="data">The data to add.</param>
   public void Enqueue(T data)
   {
      if (_disposeRequested)
         return;

      _queueLock.Wait();
      try
      {
         _queue.Enqueue(data);
      }
      finally
      {
         _queueLock.Release();
      }
   }
   private void ThreadLoop()
   {
      while (_disposeRequested == false || _queue.Count > 0)
      {
         _queueLock.Wait();

         bool hasData;
         T? data;
         try
         {
            hasData = _queue.TryDequeue(out data);
         }
         finally
         {
            _queueLock.Release();
         }

         if (hasData)
         {
            Debug.Assert(data is not null);
            WriteRequested?.Invoke(data);
         }
         else if (Thread.Yield() == false)
            Thread.Sleep(ThreadSleepTimeout);
      }
   }

   /// <inheritdoc/>
   public void Dispose()
   {
      _disposeRequested = true;
      bool HasStopped() => _thread.IsAlive == false;

      SpinWait.SpinUntil(HasStopped);
   }
   #endregion
}