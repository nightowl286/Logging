﻿namespace TNO.Logging.Writer.Abstractions;
public interface ILogWriter
{
   #region Methods
   void WriteTag(string tag, ulong id);
   void WriteLinks(ulong contextId, string file, int line, ulong[] idsToLink);
   void WriteContext(string name, ulong parent);
   void WriteFileReference(string file, ulong id);
   void WriteEntry(ILogEntry entry);
   void Close();
   #endregion
}
