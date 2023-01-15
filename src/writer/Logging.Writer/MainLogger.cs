using TNO.Logging.Writer.Entries;
using TNO.Logging.Writer.IdFactories;

namespace TNO.Logging.Writer;
internal class MainLogger
{
   #region Fields
   private readonly SafeIdFactory _entryIdFactory = new SafeIdFactory(1);
   private readonly SafeIdFactory _contextIdFactory = new SafeIdFactory(1);
   private readonly CachedIdFactory<string> _fileIdFactory = new CachedIdFactory<string>(0);
   private readonly CachedIdFactory<string> _tagIdFactory = new CachedIdFactory<string>(0);
   #endregion

   #region Methods
   public ulong RequestEntryId() => _entryIdFactory.GetId();
   public void AddEntry(LogEntry entry) => throw new NotImplementedException();
   public ulong GetFileRef(string file) => _fileIdFactory.GetId(file);
   public ContextLogger CreateContext(string name, ulong parentContext) => throw new NotImplementedException();
   public void AddLinks(ulong contextId, string file, int line, ulong[] idsToLink) => throw new NotImplementedException();
   public ulong GetTagId(string tag) => _tagIdFactory.GetId(tag);
   #endregion
}
