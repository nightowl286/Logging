using System.IO.Compression;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Abstractions.Writers;

namespace TNO.Logging.Writing.Writers;

/// <summary>
/// Represents a log writer that will save the log to the file system.
/// </summary>
public sealed class FileSystemLogWriter : IFileSystemLogWriter
{
   #region Fields
   private readonly ILogWriterFacade _facade;

   private readonly BinarySerialiserWriter<IEntry> _entryDataWriter;
   private readonly BinarySerialiserWriter<FileReference> _fileReferenceDataWriter;
   private readonly BinarySerialiserWriter<ContextInfo> _contextInfoDataWriter;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public string LogPath { get; }
   #endregion

   #region Constructor
   /// <summary>Creates a new file system log writer.</summary>
   /// <param name="facade">The writer facade to use.</param>
   /// <param name="directory">The directory in which to save the log to</param>
   public FileSystemLogWriter(ILogWriterFacade facade, string directory) :
      this(facade, new FileSystemLogWriterSettings(directory))
   { }

   /// <summary>Creates a new file system log writer.</summary>
   /// <param name="facade">The writer facade to use.</param>
   /// <param name="settings">The settings to use when setting up this writer.</param>
   public FileSystemLogWriter(ILogWriterFacade facade, FileSystemLogWriterSettings settings)
   {
      _facade = facade;
      LogPath = settings.LogPath;

      // entries
      _entryDataWriter = new BinarySerialiserWriter<IEntry>(
         GetWriterPath("entries"),
         facade.GetSerialiser<IEntrySerialiser>(),
         settings.EntryThreshold);

      // file references
      _fileReferenceDataWriter = new BinarySerialiserWriter<FileReference>(
         GetWriterPath("files"),
         facade.GetSerialiser<IFileReferenceSerialiser>(),
         settings.FileReferenceThreshold);

      // context infos
      _contextInfoDataWriter = new BinarySerialiserWriter<ContextInfo>(
         GetWriterPath("contexts"),
         facade.GetSerialiser<IContextInfoSerialiser>(),
         settings.ContextInfoThreshold);

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

      string path = GetWriterPath("versions");
      using (BinaryWriter writer = CreateWriter(path))
         serialiser.Serialise(writer, map);
   }
   #endregion

   #region Helpers
   private string GetWriterPath(string dataName)
      => Path.Combine(LogPath, dataName);
   internal static FileStream OpenStream(string path)
   {
      FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.ReadWrite, FileShare.Read);

      return fs;
   }
   internal static BinaryWriter CreateWriter(string path)
   {
      FileStream fs = OpenStream(path);
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