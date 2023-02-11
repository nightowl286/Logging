using System;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;

namespace TNO.Logging.Reading.Abstractions.Readers;

/// <summary>
/// Denotes a reader for a log that was saved to the file system.
/// </summary>
public interface IFileSystemLogReader : IDisposable
{
   #region Properties
   /// <summary>An <see cref="IReader{T}"/> that can be used to read entries.</summary>
   IReader<IEntry> Entries { get; }

   /// <summary>An <see cref="IReader{T}"/> that can be used to read file references.</summary>
   IReader<FileReference> FileReferences { get; }
   #endregion
}
