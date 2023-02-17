using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Reading.Abstractions;
using TNO.Logging.Reading.Abstractions.ContextInfos;
using TNO.Logging.Reading.Abstractions.Entries;
using TNO.Logging.Reading.Abstractions.FileReferences;
using TNO.Logging.Reading.Abstractions.Readers;

namespace TNO.Logging.Reading.Readers;

/// <summary>
/// Represents a reader for a log that was saved to the file system.
/// </summary>
public sealed class FileSystemLogReader : IFileSystemLogReader
{
   #region Fields
   private readonly string? _tempPath;
   private readonly ILogReaderFacade _facade;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public string ReadDirectory { get; }

   /// <inheritdoc/>
   public string LogPath { get; }

   /// <inheritdoc/>
   public IReader<IEntry> Entries { get; private set; }

   /// <inheritdoc/>
   public IReader<FileReference> FileReferences { get; private set; }

   /// <inheritdoc/>
   public IReader<ContextInfo> ContextInfos { get; private set; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="FileSystemLogReader"/>.</summary>
   /// <param name="path">The path to the log file.</param>
   /// <param name="facade">The log reader facade.</param>
   public FileSystemLogReader(string path, ILogReaderFacade facade)
   {
      _facade = facade;
      LogPath = path;

      if (TryGetZipPath(path, out string? zipPath))
      {
         path = ExtractZip(zipPath);
         _tempPath = path;
      }

      FromDirectory(path);
      ReadDirectory = path;
   }
   #endregion

   #region Methods
   [MemberNotNull(nameof(Entries))]
   [MemberNotNull(nameof(FileReferences))]
   [MemberNotNull(nameof(ContextInfos))]
   private void FromDirectory(string directory)
   {
      DataVersionMap map = ReadVersionsMap(directory);
      IDeserialiserProvider deserialiserProvider = _facade.GenerateProvider(map);

      BinaryReader entryReader = OpenReader(Path.Combine(directory, "entries"));
      CreateEntryReader(deserialiserProvider, entryReader);

      BinaryReader fileReferenceReader = OpenReader(Path.Combine(directory, "files"));
      CreateFileReferenceReader(deserialiserProvider, fileReferenceReader);

      BinaryReader contextInfoReader = OpenReader(Path.Combine(directory, "contexts"));
      CreateContextInfoReader(deserialiserProvider, contextInfoReader);
   }
   private DataVersionMap ReadVersionsMap(string directory)
   {
      string path = Path.Combine(directory, "versions");
      using BinaryReader reader = OpenReader(path);
      IDataVersionMapDeserialiser deserialiser = _facade.GetDeserialiser<IDataVersionMapDeserialiser>();
      DataVersionMap map = deserialiser.Deserialise(reader);

      return map;
   }

   /// <inheritdoc/>
   public void Dispose()
   {
      Entries.TryDispose();
      FileReferences.TryDispose();
      ContextInfos.TryDispose();

      if (_tempPath is not null)
         Directory.Delete(_tempPath, true);
   }
   #endregion

   #region Create Readers
   [MemberNotNull(nameof(Entries))]
   private void CreateEntryReader(IDeserialiserProvider provider, BinaryReader reader)
   {
      IEntryDeserialiser deserialiser = provider.GetDeserialiser<IEntryDeserialiser>();
      BinaryDeserialiserReader<IEntryDeserialiser, IEntry> entryReader = new BinaryDeserialiserReader<IEntryDeserialiser, IEntry>(reader, deserialiser);

      Entries = entryReader;
   }

   [MemberNotNull(nameof(FileReferences))]
   private void CreateFileReferenceReader(IDeserialiserProvider provider, BinaryReader reader)
   {
      IFileReferenceDeserialiser deserialiser = provider.GetDeserialiser<IFileReferenceDeserialiser>();
      BinaryDeserialiserReader<IFileReferenceDeserialiser, FileReference> fileReferenceReader =
         new BinaryDeserialiserReader<IFileReferenceDeserialiser, FileReference>(reader, deserialiser);

      FileReferences = fileReferenceReader;
   }

   [MemberNotNull(nameof(ContextInfos))]
   private void CreateContextInfoReader(IDeserialiserProvider provider, BinaryReader reader)
   {
      IContextInfoDeserialiser deserialiser = provider.GetDeserialiser<IContextInfoDeserialiser>();
      BinaryDeserialiserReader<IContextInfoDeserialiser, ContextInfo> contextInfoReader =
         new BinaryDeserialiserReader<IContextInfoDeserialiser, ContextInfo>(reader, deserialiser);

      ContextInfos = contextInfoReader;
   }
   #endregion

   #region Helpers
   private static string ExtractZip(string path)
   {
      string tempPath = Path.GetTempPath();
      string folderPath;
      do
      {
         folderPath = Path.Combine(tempPath, Path.GetRandomFileName());
      }
      while (Directory.Exists(folderPath));

      using (FileStream fs = OpenStream(path))
      {
         ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Read);
         archive.ExtractToDirectory(folderPath, false);
      }

      return folderPath;
   }
   private static bool TryGetZipPath(string path, [NotNullWhen(true)] out string? zipPath)
   {
      if (Path.GetExtension(path) == ".zip")
      {
         zipPath = path;
         return true;
      }

      string possiblePath = path + ".zip";
      if (Path.Exists(possiblePath))
      {
         zipPath = possiblePath;
         return true;
      }

      string[] zipFiles = Directory.GetFiles(path, "*.zip", SearchOption.TopDirectoryOnly);
      if (zipFiles.Length == 1)
      {
         zipPath = zipFiles[0];
         return true;
      }

      zipPath = null;
      return false;
   }
   private static FileStream OpenStream(string path)
   {
      FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
      return fs;
   }
   private static BinaryReader OpenReader(string path)
   {
      FileStream fs = OpenStream(path);
      BinaryReader reader = new BinaryReader(fs);

      return reader;
   }
   #endregion
}