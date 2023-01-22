using System.Collections;
using TNO.Logging.Writer.Abstractions;
using TNO.Logging.Writer.Loggers.Serialisers;

namespace TNO.Logging.Writer.Loggers.Writers;

internal abstract class BaseWriter : ILogWriter
{
   #region Consts
   private const int Thread_Wait_Sleep_Milliseconds = 25;
   #endregion

   #region Fields
   private readonly SemaphoreSlim _requestSemaphore = new SemaphoreSlim(1);
   private bool _writeThreadActive = false;
   private readonly Queue _writeRequests = new Queue();
   #endregion

   #region Methods
   public virtual void Close()
   {
      WaitUntilWriteThreadIsDone();
   }
   #region Write Requests
   public virtual void RequestWriteContext(string name, ulong id, ulong parent)
   {
      Context context = new Context(name, id, parent);

      AddWriteRequest(context);
   }
   public virtual void RequestWriteFileReference(string file, ulong id)
   {
      FileReference fileRef = new FileReference(file, id);

      AddWriteRequest(fileRef);
   }
   public virtual void RequestWriteLinks(ulong contextId, ulong fileRef, int line, ulong[] idsToLink)
   {
      Links links = new Links(contextId, fileRef, line, idsToLink);

      AddWriteRequest(links);
   }
   public virtual void RequestWriteTag(string name, ulong id)
   {
      Tag tag = new Tag(name, id);

      AddWriteRequest(tag);
   }
   public virtual void RequestWriteEntry(ILogEntry entry) => AddWriteRequest(entry);
   #endregion
   #region Writes
   protected abstract void Write(Context context);
   protected abstract void Write(Tag tag);
   protected abstract void Write(Links links);
   protected abstract void Write(FileReference fileReference);
   protected abstract void Write(ILogEntry entry);
   #endregion
   #region Serialisation
   protected static void Serialise(BinaryWriter writer, Context context) => ContextSerialiser.Serialise(writer, context);
   protected static void Serialise(BinaryWriter writer, Tag tag) => TagSerialiser.Serialise(writer, tag);
   protected static void Serialise(BinaryWriter writer, Links links) => LinksSerialiser.Serialise(writer, links);
   protected static void Serialise(BinaryWriter writer, FileReference fileReference) => FileReferenceSerialiser.Serialise(writer, fileReference);
   protected static void Serialise(BinaryWriter writer, ILogEntry entry) => EntrySerialiser.Serialise(writer, entry);
   #endregion
   #endregion

   #region Helpers
   private void WriteThreadOperation()
   {
      try
      {
         while (true)
         {
            _requestSemaphore.Wait();
            object? request = null;
            try
            {
               if (_writeRequests.Count > 0)
                  request = _writeRequests.Dequeue();

               if (request is null)
                  return; // outer finally will set the thread state
            }
            finally
            {
               _requestSemaphore.Release();
            }

            if (request is Context context) Write(context);
            else if (request is Tag tag) Write(tag);
            else if (request is Links links) Write(links);
            else if (request is FileReference fileReference) Write(fileReference);
            else if (request is ILogEntry logEntry) Write(logEntry);
         }
      }
      finally
      {
         _writeThreadActive = false;
      }
   }
   protected void AddWriteRequest(object data)
   {
      _requestSemaphore.Wait();
      try
      {
         _writeRequests.Enqueue(data);

         if (_writeThreadActive)
            return;

         _writeThreadActive = true;
         Thread thread = new Thread(WriteThreadOperation);
         thread.Start();
      }
      finally
      {
         _requestSemaphore.Release();
      }
   }
   protected void WaitUntilWriteThreadIsDone()
   {
      while (true)
      {
         _requestSemaphore.Wait();
         try
         {
            if (_writeThreadActive == false)
               return;

            Thread.Sleep(Thread_Wait_Sleep_Milliseconds);
         }
         finally
         {
            _requestSemaphore.Release();
         }
      }
   }
   #endregion
}
