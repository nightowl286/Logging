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
public sealed class FileSystemLogWriter : ILogWriter, IDisposable
{
   #region Fields
   private readonly ILogWriterFacade _facade;
   private readonly ThreadedQueue<IEntry> _entryQueue;
   private readonly LogWriterContext _context;
   private readonly string _directory;
   private readonly BinaryWriter _entryWriter;

   private readonly IEntrySerialiser _entrySerialiser;
   #endregion
   private FileSystemLogWriter(ILogWriterFacade facade, string directory)
   {
      _facade = facade;
      _entryQueue = new ThreadedQueue<IEntry>(nameof(_entryQueue));
      _entryQueue.WriteRequested += entryQueue_WriteRequested;

      _context = new LogWriterContext();
      _directory = directory;

      // writers
      _entryWriter = OpenWriter(Path.Combine(_directory, "entries"));

      // serialisers
      _entrySerialiser = facade.GetSerialiser<IEntrySerialiser>();

      WriteVersions();
   }

   #region Methods
   /// <inheritdoc/>
   public void RequestWrite(IEntry entry) => _entryQueue.Enqueue(entry);

   /// <inheritdoc/>
   public void Dispose()
   {
      _entryQueue.Dispose();
      _entryWriter.Dispose();

      CreateArchive(_directory);
   }

   private void entryQueue_WriteRequested(IEntry data)
   {
      _entrySerialiser.Serialise(_entryWriter, data);

      _entryWriter.Flush();
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
   /// <param name="logWriter">The created log writer.</param>
   /// <returns>An instance of the created logger.</returns>
   public static ILogger CreateDated(ILogWriterFacade facade, string directory, out FileSystemLogWriter logWriter)
   {
      DateTime date = DateTime.Now;
      string dateStr = date.ToString("yyyy-MM-dd_HH-mm-ss");
      directory = Path.Combine(directory, dateStr);

      return Create(facade, directory, out logWriter);
   }

   /// <summary>Creates a new logger that will save to the file system.</summary>
   /// <param name="facade">The facade for the log writing system.</param>
   /// <param name="directory">The directory to save the log in.</param>
   /// <param name="logWriter">The created log writer.</param>
   /// <returns>An instance of the created logger.</returns>
   public static ILogger Create(ILogWriterFacade facade, string directory, out FileSystemLogWriter logWriter)
   {
      Directory.CreateDirectory(directory);

      logWriter = new FileSystemLogWriter(facade, directory);
      ScopedLogger logger = new ScopedLogger(logWriter, logWriter._context);

      return logger;
   }
   #endregion

   #region Helpers
   private static BinaryWriter OpenWriter(string path)
   {
      FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
      BinaryWriter writer = new BinaryWriter(fs);

      return writer;
   }
   private static void CreateArchive(string logDirectory)
   {
      string name = Path.GetFileNameWithoutExtension(logDirectory);
      string directory = Path.GetDirectoryName(logDirectory)!;
      string path = Path.Combine(directory, name + ".zip");

      ZipFile.CreateFromDirectory(logDirectory, path, CompressionLevel.SmallestSize, false);
      Directory.Delete(logDirectory, true);
   }
   #endregion
}
