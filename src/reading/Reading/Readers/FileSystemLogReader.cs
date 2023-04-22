using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using TNO.Common.Extensions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Readers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Deserialisers.Registrants;

namespace TNO.Logging.Reading.Readers;

/// <summary>
/// Represents a reader for a log that was saved to the file system.
/// </summary>
public sealed class FileSystemLogReader : IFileSystemLogReader
{
   #region Fields
   private readonly IServiceScope _scope;
   private readonly string? _tempPath;
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

   /// <inheritdoc/>
   public IReader<TagReference> TagReferences { get; private set; }

   /// <inheritdoc/>
   public IReader<TableKeyReference> TableKeyReferences { get; private set; }

   /// <inheritdoc/>
   public IReader<AssemblyReference> AssemblyReferences { get; private set; }

   /// <inheritdoc/>
   public IReader<TypeReference> TypeReferences { get; private set; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="FileSystemLogReader"/>.</summary>
   /// <param name="path">The path to the log file.</param>
   /// <param name="scope">The <see cref="IServiceScope"/> to use.</param>
   public FileSystemLogReader(string path, IServiceScope scope)
   {
      _scope = scope;
      LogPath = path;

      if (TryGetZipPath(path, out string? zipPath))
      {
         path = ExtractZip(zipPath);
         _tempPath = path;
      }

      ReadDirectory = path;
      FromDirectory(path);
   }
   #endregion

   #region Methods
   [MemberNotNull(nameof(Entries))]
   [MemberNotNull(nameof(FileReferences))]
   [MemberNotNull(nameof(ContextInfos))]
   [MemberNotNull(nameof(TagReferences))]
   [MemberNotNull(nameof(TableKeyReferences))]
   [MemberNotNull(nameof(AssemblyReferences))]
   [MemberNotNull(nameof(TypeReferences))]
   private void FromDirectory(string directory)
   {
      IDeserialiser deserialiser = new Deserialiser(_scope.Requester);
      _scope.Registrar.Instance(deserialiser);

      DataVersionMap map = ReadVersionsMap(directory);

      new BuiltInVersionMapDeserialiserRegistrant(map).Register(_scope);


      Entries = new DeserialiserReader<IEntry>(
         GetReaderPath(FileSystemConstants.EntryPath),
         deserialiser.Get<IEntry>());

      FileReferences = new DeserialiserReader<FileReference>(
         GetReaderPath(FileSystemConstants.FilePath),
         deserialiser.Get<FileReference>());

      ContextInfos = new DeserialiserReader<ContextInfo>(
         GetReaderPath(FileSystemConstants.ContextInfoPath),
         deserialiser.Get<ContextInfo>());

      TagReferences = new DeserialiserReader<TagReference>(
         GetReaderPath(FileSystemConstants.TagPath),
         deserialiser.Get<TagReference>());

      TableKeyReferences = new DeserialiserReader<TableKeyReference>(
         GetReaderPath(FileSystemConstants.TableKeyPath),
         deserialiser.Get<TableKeyReference>());

      AssemblyReferences = new DeserialiserReader<AssemblyReference>(
         GetReaderPath(FileSystemConstants.AssemblyPath),
         deserialiser.Get<AssemblyReference>());

      TypeReferences = new DeserialiserReader<TypeReference>(
         GetReaderPath(FileSystemConstants.TypePath),
         deserialiser.Get<TypeReference>());
   }
   private DataVersionMap ReadVersionsMap(string directory)
   {
      string path = Path.Combine(directory, FileSystemConstants.VersionPath);
      using BinaryReader reader = OpenReader(path);
      IDeserialiser<DataVersionMap> deserialiser = _scope.Requester.Get<IDeserialiser<DataVersionMap>>();
      DataVersionMap map = deserialiser.Deserialise(reader);

      return map;
   }

   /// <inheritdoc/>
   public void Dispose()
   {
      Entries.TryDispose();
      FileReferences.TryDispose();
      ContextInfos.TryDispose();
      TagReferences.TryDispose();
      TableKeyReferences.TryDispose();
      AssemblyReferences.TryDispose();
      TypeReferences.TryDispose();

      if (_tempPath is not null)
         Directory.Delete(_tempPath, true);
   }
   #endregion

   #region Helpers
   private string GetReaderPath(string dataName)
      => Path.Combine(ReadDirectory, dataName);
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
      if (Path.GetExtension(path) == FileSystemConstants.DotArchiveExtension)
      {
         zipPath = path;
         return true;
      }

      string possiblePath = path + FileSystemConstants.DotArchiveExtension;
      if (Path.Exists(possiblePath))
      {
         zipPath = possiblePath;
         return true;
      }

      string[] zipFiles = Directory.GetFiles(path, $"*{FileSystemConstants.DotArchiveExtension}", SearchOption.TopDirectoryOnly);
      if (zipFiles.Length == 1)
      {
         zipPath = zipFiles[0];
         return true;
      }

      zipPath = null;
      return false;
   }
   internal static FileStream OpenStream(string path)
   {
      FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete);
      return fs;
   }
   internal static BinaryReader OpenReader(string path)
   {
      FileStream fs = OpenStream(path);
      BinaryReader reader = new BinaryReader(fs);

      return reader;
   }
   #endregion
}