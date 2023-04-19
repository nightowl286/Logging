using System.IO.Compression;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Abstractions.Writers;

namespace TNO.Logging.Writing.Writers;

/// <summary>
/// Represents a log writer that will save the log to the file system.
/// </summary>
public sealed class FileSystemLogWriter : IFileSystemLogWriter
{
   #region Fields
   private readonly ISerialiserProvider _provider;

   private readonly SerialiserWriter<IEntry> _entryDataWriter;
   private readonly SerialiserWriter<FileReference> _fileReferenceDataWriter;
   private readonly SerialiserWriter<ContextInfo> _contextInfoDataWriter;
   private readonly SerialiserWriter<TagReference> _tagReferenceDataWriter;
   private readonly SerialiserWriter<TableKeyReference> _tableKeyReferenceDataWriter;
   private readonly SerialiserWriter<AssemblyReference> _assemblyReferenceDataWriter;
   private readonly SerialiserWriter<TypeReference> _typeReferenceDataWriter;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public string LogPath { get; }
   #endregion

   #region Constructor
   /// <summary>Creates a new file system log writer.</summary>
   /// <param name="provider">The writer facade to use.</param>
   /// <param name="directory">The directory in which to save the log to</param>
   public FileSystemLogWriter(ISerialiserProvider provider, string directory) :
      this(provider, new FileSystemLogWriterSettings(directory))
   { }

   /// <summary>Creates a new file system log writer.</summary>
   /// <param name="provider">The writer facade to use.</param>
   /// <param name="settings">The settings to use when setting up this writer.</param>
   public FileSystemLogWriter(ISerialiserProvider provider, FileSystemLogWriterSettings settings)
   {
      _provider = provider;
      LogPath = settings.LogPath;

      // entries
      _entryDataWriter = new SerialiserWriter<IEntry>(
         GetWriterPath(FileSystemConstants.EntryPath),
         provider.GetSerialiser<IEntry>(),
         settings.EntryThreshold);

      // file references
      _fileReferenceDataWriter = new SerialiserWriter<FileReference>(
         GetWriterPath(FileSystemConstants.FilePath),
         provider.GetSerialiser<FileReference>(),
         settings.FileReferenceThreshold);

      // context infos
      _contextInfoDataWriter = new SerialiserWriter<ContextInfo>(
         GetWriterPath(FileSystemConstants.ContextInfoPath),
         provider.GetSerialiser<ContextInfo>(),
         settings.ContextInfoThreshold);

      // tag references
      _tagReferenceDataWriter = new SerialiserWriter<TagReference>(
         GetWriterPath(FileSystemConstants.TagPath),
         provider.GetSerialiser<TagReference>(),
         settings.TagReferenceThreshold);

      // table key references
      _tableKeyReferenceDataWriter = new SerialiserWriter<TableKeyReference>(
         GetWriterPath(FileSystemConstants.TableKeyPath),
         provider.GetSerialiser<TableKeyReference>(),
         settings.TableKeyReferenceThreshold);

      // assembly infos
      _assemblyReferenceDataWriter = new SerialiserWriter<AssemblyReference>(
         GetWriterPath(FileSystemConstants.AssemblyPath),
         provider.GetSerialiser<AssemblyReference>(),
         settings.AssemblyReferenceThreshold);

      // type infos
      _typeReferenceDataWriter = new SerialiserWriter<TypeReference>(
         GetWriterPath(FileSystemConstants.TypePath),
         provider.GetSerialiser<TypeReference>(),
         settings.TypeReferenceThreshold);

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
   public void Deposit(TagReference tagReference) => _tagReferenceDataWriter.Deposit(tagReference);

   /// <inheritdoc/>
   public void Deposit(TableKeyReference tableKeyReference) => _tableKeyReferenceDataWriter.Deposit(tableKeyReference);

   /// <inheritdoc/>
   public void Deposit(AssemblyReference assemblyReference) => _assemblyReferenceDataWriter.Deposit(assemblyReference);

   /// <inheritdoc/>
   public void Deposit(TypeReference typeReference) => _typeReferenceDataWriter.Deposit(typeReference);

   /// <inheritdoc/>
   public void Dispose()
   {
      _entryDataWriter.Dispose();
      _fileReferenceDataWriter.Dispose();
      _contextInfoDataWriter.Dispose();
      _tagReferenceDataWriter.Dispose();
      _tableKeyReferenceDataWriter.Dispose();
      _assemblyReferenceDataWriter.Dispose();
      _typeReferenceDataWriter.Dispose();

      CreateArchive(LogPath);
   }
   private void WriteVersions()
   {
      ISerialiser<DataVersionMap> serialiser = _provider.GetSerialiser<DataVersionMap>();
      DataVersionMap map = _provider.GetVersionMap();

      string path = GetWriterPath(FileSystemConstants.VersionPath);
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
      string path = Path.Combine(directory, name + FileSystemConstants.DotArchiveExtension);

      ZipFile.CreateFromDirectory(logDirectory, path, CompressionLevel.SmallestSize, false);
      Directory.Delete(logDirectory, true);
   }
   #endregion
}