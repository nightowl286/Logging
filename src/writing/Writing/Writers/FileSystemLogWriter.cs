﻿using System.IO.Compression;
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

   private readonly IEntrySerialiser _entrySerialiser;
   private readonly ThreadedQueue<IEntry> _entryQueue;
   private readonly BinaryWriter _entryWriter;

   private readonly IFileReferenceSerialiser _fileReferenceSerialiser;
   private readonly ThreadedQueue<FileReference> _fileReferenceQueue;
   private readonly BinaryWriter _fileReferenceWriter;
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

      CreateArchive(LogPath);
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