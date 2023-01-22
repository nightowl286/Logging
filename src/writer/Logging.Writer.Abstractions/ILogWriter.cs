namespace TNO.Logging.Writer.Abstractions;

public interface ILogWriter
{
   #region Methods
   void RequestWriteTag(string name, ulong id);
   void RequestWriteLinks(ulong contextId, ulong fileRef, int line, ulong[] idsToLink);
   void RequestWriteContext(string name, ulong id, ulong parent);
   void RequestWriteFileReference(string file, ulong id);
   void RequestWriteEntry(ILogEntry entry);
   void Close();
   #endregion
}
