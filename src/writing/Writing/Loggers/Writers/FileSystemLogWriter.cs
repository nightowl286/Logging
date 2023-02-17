using System.IO.Compression;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions.Loggers;

namespace TNO.Logging.Writing.Loggers.Writers;

/// <summary>
/// Denotes a log writer that will save the log to the file system.
/// </summary>
public sealed class FileSystemLogWriter : ILogDataCollector, IDisposable
{
   #region Fields
   private readonly ILogWriterFacade _facade;
   private readonly string _directory;

   private readonly IEntrySerialiser _entrySerialiser;
   private readonly ThreadedQueue<IEntry> _entryQueue;
   private readonly BinaryWriter _entryWriter;

   private readonly IFileReferenceSerialiser _fileReferenceSerialiser;
   private readonly ThreadedQueue<FileReference> _fileReferenceQueue;
   private readonly BinaryWriter _fileReferenceWriter;
   #endregion

   #region Constructor
   /// <summary>Creates a new file system log writer.</summary>
   /// <param name="facade">The writer facade to use.</param>
   /// <param name="directory">The directory in which to save the log to</param>
   public FileSystemLogWriter(ILogWriterFacade facade, string directory)
   {
      _facade = facade;
      _directory = directory;

      // entries
      _entryQueue = new ThreadedQueue<IEntry>(nameof(_entryQueue), entryQueue_WriteRequested, ThreadPriority.Lowest);
      _entryWriter = OpenDirectoryWriter("entries");
      _entrySerialiser = facade.GetSerialiser<IEntrySerialiser>();

      // file references
      _fileReferenceQueue = new ThreadedQueue<FileReference>(nameof(_fileReferenceQueue), fileReferenceQueue_WriteRequested, ThreadPriority.Lowest);
      _fileReferenceWriter = OpenDirectoryWriter("files");
      _fileReferenceSerialiser = facade.GetSerialiser<IFileReferenceSerialiser>();

      WriteVersions();
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Deposit(IEntry entry) => _entryQueue.Enqueue(entry);

   /// <inheritdoc/>
   public void Deposit(FileReference fileReference) => _fileReferenceQueue.Enqueue(fileReference);

   /// <inheritdoc/>
   public void Dispose()
   {
      _entryQueue.Dispose();
      _entryWriter.Dispose();

      _fileReferenceQueue.Dispose();
      _fileReferenceWriter.Dispose();

      CreateArchive(_directory);
   }

   private void entryQueue_WriteRequested(IEntry data)
   {
      _entrySerialiser.Serialise(_entryWriter, data);

      _entryWriter.Flush();
   }
   private void fileReferenceQueue_WriteRequested(FileReference data)
   {
      _fileReferenceSerialiser.Serialise(_fileReferenceWriter, data);

      _fileReferenceWriter.Flush();
   }
   private void WriteVersions()
   {
      IDataVersionMapSerialiser serialiser = _facade.GetSerialiser<IDataVersionMapSerialiser>();
      DataVersionMap map = _facade.GetVersionMap();

      string path = Path.Combine(_directory, "versions");
      using (BinaryWriter writer = OpenWriter(path))
         serialiser.Serialise(writer, map);
   }
   #endregion

   #region Functions
   /// <summary>Creates a new logger that will save to the file system.</summary>
   /// <param name="facade">The facade for the log writing system.</param>
   /// <param name="directory">
   /// The directory to which the log should be saved. A new child directory
   /// (formatted based on <see cref="DateTime.Now"/>) will be created.
   /// </param>
   /// <returns>An instance of the created logger.</returns>
   public static IFileSystemLogger CreateDated(ILogWriterFacade facade, string directory)
   {
      DateTime date = DateTime.Now;
      string dateStr = date.ToString("yyyy-MM-dd_HH-mm-ss");
      directory = Path.Combine(directory, dateStr);

      return Create(facade, directory);
   }

   /// <summary>Creates a new logger that will save to the file system.</summary>
   /// <param name="facade">The facade for the log writing system.</param>
   /// <param name="directory">The directory to save the log in.</param>
   /// <returns>An instance of the created logger.</returns>
   public static IFileSystemLogger Create(ILogWriterFacade facade, string directory)
   {
      Directory.CreateDirectory(directory);

      LogWriterContext context = new LogWriterContext();

      FileSystemLogWriter logWriter = new FileSystemLogWriter(facade, directory);
      ScopedLogger logger = new ScopedLogger(logWriter, context);
      FileSystemLoggerWrapper fsLogger = new FileSystemLoggerWrapper(logger, logWriter, directory);

      return fsLogger;
   }
   #endregion

   #region Helpers
   private BinaryWriter OpenDirectoryWriter(string path)
      => OpenWriter(Path.Combine(_directory, path));
   private static BinaryWriter OpenWriter(string path)
   {
      FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
      BinaryWriter writer = new BinaryWriter(fs);

      return writer;
   }
   private static void CreateArchive(string logDirectory)
   {
      string name = Path.GetFileName(logDirectory);
      string directory = Path.GetDirectoryName(logDirectory)!;
      string path = Path.Combine(directory, name + ".zip");

      ZipFile.CreateFromDirectory(logDirectory, path, CompressionLevel.SmallestSize, false);
      Directory.Delete(logDirectory, true);
   }
   #endregion
}