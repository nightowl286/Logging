﻿using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Reading.Abstractions;
using TNO.Logging.Reading.Abstractions.Entries;
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
   public IReader<IEntry> Entries { get; private set; }
   #endregion

   #region Methods
   /// <summary>Creates a new instance of the <see cref="FileSystemLogReader"/>.</summary>
   /// <param name="path">The path to the log file.</param>
   /// <param name="facade">The log reader facade.</param>
   public FileSystemLogReader(string path, ILogReaderFacade facade)
   {
      _facade = facade;

      if (TryGetZipPath(path, out string? zipPath))
         path = ExtractZip(zipPath);

      FromDirectory(path);
   }
   #endregion

   #region Methods
   [MemberNotNull(nameof(Entries))]
   private void FromDirectory(string directory)
   {
      DataVersionMap map = ReadVersionsMap(directory);
      IDeserialiserProvider deserialiserProvider = _facade.GenerateProvider(map);

      BinaryReader entryReader = OpenReader(Path.Combine(directory, "entries"));
      CreateEntryReader(deserialiserProvider, entryReader);
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