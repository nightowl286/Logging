using System.IO.Compression;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;
using TNO.Logging.Writing.Abstractions.Writers;

namespace TNO.Logging.Writing.Writers;

/// <summary>
/// Represents a log writer that will save the log to the file system.
/// </summary>
public sealed class FileSystemLogWriter : IFileSystemLogWriter
{
   #region Subclasses
   private sealed class DataWriter<T> : IDisposable where T : notnull
   {
      #region Fields
      private readonly IBinarySerialiser<T> _serialiser;
      private readonly ThreadedQueue<T> _queue;
      private readonly BinaryWriter _writer;
      #endregion
      public DataWriter(BinaryWriter writer, IBinarySerialiser<T> serialiser)
      {
         _writer = writer;
         _serialiser = serialiser;

         _queue = new ThreadedQueue<T>(
            $"{nameof(FileSystemLogWriter)}.{nameof(T)}",
            WriteData,
            ThreadPriority.Lowest);
      }

      #region Methods
      private void WriteData(T data)
      {
         _serialiser.Serialise(_writer, data);

         _writer.Flush();
      }
      public void Dispose()
      {
         _queue.Dispose();
         _writer.Dispose();
      }
      public void Deposit(T data) => _queue.Enqueue(data);
      #endregion
   }
   #endregion

   #region Fields
   private readonly ILogWriterFacade _facade;

   private readonly DataWriter<IEntry> _entryDataWriter;
   private readonly DataWriter<FileReference> _fileReferenceDataWriter;
   private readonly DataWriter<ContextInfo> _contextInfoDataWriter;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public string LogPath { get; }
   #endregion

   #region Constructor
   /// <summary>Creates a new file system log writer.</summary>
   /// <param name="facade">The writer facade to use.</param>
   /// <param name="directory">The directory in which to save the log to</param>
   public FileSystemLogWriter(ILogWriterFacade facade, string directory)
   {
      _facade = facade;
      LogPath = directory;

      // entries
      _entryDataWriter = new DataWriter<IEntry>(
         OpenDirectoryWriter("entries"),
         facade.GetSerialiser<IEntrySerialiser>());

      // file references
      _fileReferenceDataWriter = new DataWriter<FileReference>(
         OpenDirectoryWriter("files"),
         facade.GetSerialiser<IFileReferenceSerialiser>());

      // context infos
      _contextInfoDataWriter = new DataWriter<ContextInfo>(
         OpenDirectoryWriter("contexts"),
         facade.GetSerialiser<IContextInfoSerialiser>());

      WriteVersions();
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Deposit(IEntry entry) => _entryDataWriter.Deposit(entry);

   /// <inheritdoc/>
   public void Deposit(FileReference fileReference) => _fileReferenceDataWriter.Deposit(fileReference);

   /// <inheritdoc/>
   public void Deposit(ContextInfo contextInfo) => _contextInfoDataWriter.Deposit(contextInfo);

   /// <inheritdoc/>
   public void Dispose()
   {
      _entryDataWriter.Dispose();
      _fileReferenceDataWriter.Dispose();
      _contextInfoDataWriter.Dispose();

      CreateArchive(LogPath);
   }
   private void WriteVersions()
   {
      IDataVersionMapSerialiser serialiser = _facade.GetSerialiser<IDataVersionMapSerialiser>();
      DataVersionMap map = _facade.GetVersionMap();

      string path = Path.Combine(LogPath, "versions");
      using (BinaryWriter writer = OpenWriter(path))
         serialiser.Serialise(writer, map);
   }
   #endregion

   #region Helpers
   private BinaryWriter OpenDirectoryWriter(string path)
      => OpenWriter(Path.Combine(LogPath, path));
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