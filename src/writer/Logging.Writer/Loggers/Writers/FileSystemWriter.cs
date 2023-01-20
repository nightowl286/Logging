using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Text;
using TNO.Logging.Writer.Abstractions;
using TNO.Logging.Writer.Loggers.Serialisers;

namespace TNO.Logging.Writer.Loggers.Writers;
internal class FileSystemWriter : ILogWriter
{
   #region Fields
   private const int Thread_Wait_Sleep_Milliseconds = 25;
   private static readonly Encoding Encoding = Encoding.UTF8;
   private readonly string _directoryPath;
   private readonly SemaphoreSlim _requestSemaphore = new SemaphoreSlim(1);
   private readonly Queue _writeRequests = new Queue();
   private bool _writeThreadActive = false;

   private BinaryWriter _fileRefTable;
   private BinaryWriter _assemblyRefTable;
   private BinaryWriter _typeRefTable;
   private BinaryWriter _tagRefTable;
   private BinaryWriter _entryLinksTable;

   private BinaryWriter _entryOffetsTable;
   private BinaryWriter _contextHierarchyTable;

   #endregion
   public FileSystemWriter(string directoryPath)
   {
      _directoryPath = directoryPath;
      Directory.CreateDirectory(directoryPath);

      ThrowIfDirectoryIsInvalid(directoryPath);

      SetupTables();
   }

   #region Methods
   public void WriteContext(string name, ulong id, ulong parent)
   {
      Context context = new Context(name, id, parent);

      CheckWriteThread(context);
   }
   public void WriteEntry(ILogEntry entry) => CheckWriteThread(entry);
   public void WriteFileReference(string file, ulong id)
   {
      FileReference fileRef = new FileReference(file, id);

      CheckWriteThread(fileRef);
   }
   public void WriteLinks(ulong contextId, ulong fileRef, int line, ulong[] idsToLink)
   {
      Links links = new Links(contextId, fileRef, line, idsToLink);

      CheckWriteThread(links);
   }
   public void WriteTag(string name, ulong id)
   {
      Tag tag = new Tag(name, id);

      CheckWriteThread(tag);
   }
   private void CheckWriteThread(object request)
   {
      _requestSemaphore.Wait();
      try
      {
         _writeRequests.Enqueue(request);

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

            if (request is Context context) CoreWriteContext(context);
            else if (request is Tag tag) CoreWriteTag(tag);
            else if (request is Links links) CoreWriteLinks(links);
            else if (request is FileReference fileReference) CoreWriteFileReference(fileReference);
            else if (request is ILogEntry logEntry) CoreWriteEntry(logEntry);
         }
      }
      finally
      {
         _writeThreadActive = false;
      }
   }

   public void Close()
   {
      WaitUntilThreadIsDone();

      _fileRefTable.Dispose();
      _assemblyRefTable.Dispose();
      _typeRefTable.Dispose();
      _tagRefTable.Dispose();

      _entryOffetsTable.Dispose();
      _contextHierarchyTable.Dispose();
      _entryLinksTable.Dispose();

      ZipLogDirectory();
      Directory.Delete(_directoryPath, true);
   }
   private void WaitUntilThreadIsDone()
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

   #region Core Write Methods
   private void CoreWriteContext(Context context) => ContextSerialiser.Serialise(_contextHierarchyTable, context);
   private void CoreWriteTag(Tag tag) => TagSerialiser.Serialise(_tagRefTable, tag);
   private void CoreWriteLinks(Links links) => LinksSerialiser.Serialise(_entryLinksTable, links);
   private void CoreWriteFileReference(FileReference fileReference) => FileReferenceSerialiser.Serialise(_fileRefTable, fileReference);
   private void CoreWriteEntry(ILogEntry entry)
   {

   }
   #endregion

   #region Setup Methods
   private void ZipLogDirectory()
   {
      string name = Path.GetFileName(_directoryPath);
      string parentFolder = Path.GetDirectoryName(_directoryPath)!;
      string zipPath = Path.Combine(parentFolder, name + ".zip");

      using (FileStream fs = new FileStream(zipPath, FileMode.CreateNew, FileAccess.Write))
      {
         using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Create))
         {
            foreach (string file in Directory.EnumerateFiles(_directoryPath, "*", SearchOption.AllDirectories))
            {
               string entryName = file.Substring(_directoryPath.Length);
               archive.CreateEntryFromFile(file, entryName, CompressionLevel.SmallestSize);
            }
         }
      }
   }

   [MemberNotNull(nameof(_fileRefTable))]
   [MemberNotNull(nameof(_assemblyRefTable))]
   [MemberNotNull(nameof(_typeRefTable))]
   [MemberNotNull(nameof(_tagRefTable))]
   [MemberNotNull(nameof(_entryOffetsTable))]
   [MemberNotNull(nameof(_contextHierarchyTable))]
   [MemberNotNull(nameof(_entryLinksTable))]
   private void SetupTables()
   {
      string tablesPath = Path.Combine(_directoryPath, "tables");

      _fileRefTable = OpenForWrite(Path.Combine(tablesPath, "file_refs"));
      _assemblyRefTable = OpenForWrite(Path.Combine(tablesPath, "assembly_refs"));
      _typeRefTable = OpenForWrite(Path.Combine(tablesPath, "type_refs"));
      _tagRefTable = OpenForWrite(Path.Combine(tablesPath, "tag_refs"));

      _entryLinksTable = OpenForWrite(Path.Combine(tablesPath, "entry_links"));
      _entryOffetsTable = OpenForWrite(Path.Combine(tablesPath, "entry_offsets"));
      _contextHierarchyTable = OpenForWrite(Path.Combine(tablesPath, "context_hierarchy"));
   }
   private static BinaryWriter OpenForWriteCompressed(string path)
   {
      FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
      DeflateStream ds = new DeflateStream(fs, CompressionLevel.SmallestSize);
      BinaryWriter writer = new BinaryWriter(ds, Encoding);

      return writer;
   }
   private static BinaryWriter OpenForWrite(string path)
   {
      FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
      BinaryWriter writer = new BinaryWriter(fs, Encoding);

      return writer;
   }
   private static void ThrowIfDirectoryIsInvalid(string directoryPath)
   {
      bool notEmpty = Directory
         .EnumerateDirectories(directoryPath)
         .Concat(Directory.EnumerateFiles(directoryPath))
         .Any();

      if (notEmpty)
         throw new ArgumentException($"The given directory ({directoryPath}) was not empty.", nameof(directoryPath));

      bool isRoot = Path.GetDirectoryName(directoryPath) == null;

      if (isRoot)
         throw new ArgumentException($"The given directory ({directoryPath}) is a root directory.", nameof(directoryPath));
   }
   #endregion
}
