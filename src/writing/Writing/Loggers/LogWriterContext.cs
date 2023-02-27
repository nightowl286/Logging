using System.Diagnostics;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.IdFactories;

namespace TNO.Logging.Writing.Loggers;

/// <summary>
/// Represents a context that handles information about the current logging session.
/// </summary>
public class LogWriterContext : ILogWriteContext
{
   #region Fields
   private readonly SafeIdFactory _entryIdFactory = new SafeIdFactory(1);
   private readonly SafeIdFactory<string> _fileIdFactory = new SafeIdFactory<string>(1);
   private readonly SafeIdFactory _contextIdFactory = new SafeIdFactory(1);
   private readonly SafeIdFactory<string> _tagIdFactory = new SafeIdFactory<string>(1);
   private readonly SafeIdFactory<string> _tableKeyIdFactory = new SafeIdFactory<string>(1);
   private readonly Stopwatch _timestampWatch = Stopwatch.StartNew();
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ulong NewEntryId() => _entryIdFactory.GetNext();

   /// <inheritdoc/>
   public TimeSpan GetTimestamp() => _timestampWatch.Elapsed;

   /// <inheritdoc/>
   public bool GetOrCreateFileId(string file, out ulong fileId) => _fileIdFactory.GetOrCreate(file, out fileId);

   /// <inheritdoc/>
   public ulong CreateContextId() => _contextIdFactory.GetNext();

   /// <inheritdoc/>
   public bool GetOrCreateTagId(string tag, out ulong tagId) => _tagIdFactory.GetOrCreate(tag, out tagId);

   /// <inheritdoc/>
   public bool GetOrCreateTableKeyId(string key, out uint tableKeyId)
   {
      bool isNewId = _tableKeyIdFactory.GetOrCreate(key, out ulong keyId);
      tableKeyId = (uint)keyId;

      return isNewId;
   }
   #endregion
}