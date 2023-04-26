using System.IO.Compression;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Abstractions.Writers;

namespace TNO.Logging.Writing.Writers;

/// <summary>
/// Represents a log writer that will save the log to the file system.
/// </summary>
public sealed class FileSystemLogWriter : IFileSystemLogWriter
{
   #region Fields
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
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   /// <param name="directory">The directory in which to save the log to</param>
   public FileSystemLogWriter(ISerialiser serialiser, string directory) :
      this(serialiser, new FileSystemLogWriterSettings(directory))
   { }

   /// <summary>Creates a new file system log writer.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   /// <param name="settings">The settings to use when setting up this writer.</param>
   public FileSystemLogWriter(ISerialiser serialiser, FileSystemLogWriterSettings settings)
   {
      LogPath = settings.LogPath;

      WriteVersionMap(
         GetWriterPath(FileSystemConstants.VersionPath),
         serialiser.Get<DataVersionMap>());

      // entries
      _entryDataWriter = new SerialiserWriter<IEntry>(
         GetWriterPath(FileSystemConstants.EntryPath),
         serialiser.Get<IEntry>(),
         settings.EntryThreshold);

      // file references
      _fileReferenceDataWriter = new SerialiserWriter<FileReference>(
         GetWriterPath(FileSystemConstants.FilePath),
         serialiser.Get<FileReference>(),
         settings.FileReferenceThreshold);

      // context infos
      _contextInfoDataWriter = new SerialiserWriter<ContextInfo>(
         GetWriterPath(FileSystemConstants.ContextInfoPath),
         serialiser.Get<ContextInfo>(),
         settings.ContextInfoThreshold);

      // tag references
      _tagReferenceDataWriter = new SerialiserWriter<TagReference>(
         GetWriterPath(FileSystemConstants.TagPath),
         serialiser.Get<TagReference>(),
         settings.TagReferenceThreshold);

      // table key references
      _tableKeyReferenceDataWriter = new SerialiserWriter<TableKeyReference>(
         GetWriterPath(FileSystemConstants.TableKeyPath),
         serialiser.Get<TableKeyReference>(),
         settings.TableKeyReferenceThreshold);

      // assembly infos
      _assemblyReferenceDataWriter = new SerialiserWriter<AssemblyReference>(
         GetWriterPath(FileSystemConstants.AssemblyPath),
         serialiser.Get<AssemblyReference>(),
         settings.AssemblyReferenceThreshold);

      // type infos
      _typeReferenceDataWriter = new SerialiserWriter<TypeReference>(
         GetWriterPath(FileSystemConstants.TypePath),
         serialiser.Get<TypeReference>(),
         settings.TypeReferenceThreshold);
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
   #endregion

   #region Helpers
   private void WriteVersionMap(string path, ISerialiser<DataVersionMap> serialiser)
   {
      DataVersionMap map = VersionMapGenerator.GetForLatestSerialisers();
      using BinaryWriter writer = CreateWriter(path);

      serialiser.Serialise(writer, map);
   }

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

#if NET6_0_OR_GREATER
      CompressionLevel level = CompressionLevel.SmallestSize;
#else
      CompressionLevel level = CompressionLevel.Optimal;
#endif

      ZipFile.CreateFromDirectory(logDirectory, path, level, false);
      Directory.Delete(logDirectory, true);
   }
   #endregion
}