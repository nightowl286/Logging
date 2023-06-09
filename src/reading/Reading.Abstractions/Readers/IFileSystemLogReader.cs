﻿using System;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.Abstractions.LogData.Types;

namespace TNO.Logging.Reading.Abstractions.Readers;

/// <summary>
/// Denotes a reader for a log that was saved to the file system.
/// </summary>
public interface IFileSystemLogReader : IDisposable
{
   #region Properties
   /// <summary>The actual directory that the log is being read from.</summary>
   string ReadDirectory { get; }

   /// <summary>The path of the log that is being read.</summary>
   string LogPath { get; }

   /// <summary>An <see cref="IReader{T}"/> that can be used to read entries.</summary>
   IReader<IEntry> Entries { get; }

   /// <summary>An <see cref="IReader{T}"/> that can be used to read file references.</summary>
   IReader<FileReference> FileReferences { get; }

   /// <summary>An <see cref="IReader{T}"/> that can be used to read context infos.</summary>
   IReader<ContextInfo> ContextInfos { get; }

   /// <summary>An <see cref="IReader{T}"/> that can be used to read tag references.</summary>
   IReader<TagReference> TagReferences { get; }

   /// <summary>An <see cref="IReader{T}"/> that can be used to read table key references.</summary>
   IReader<TableKeyReference> TableKeyReferences { get; }

   /// <summary>An <see cref="IReader{T}"/> that can be used to read assembly references.</summary>
   IReader<AssemblyReference> AssemblyReferences { get; }

   /// <summary>An <see cref="IReader{T}"/> that can be used to read type references.</summary>
   IReader<TypeReference> TypeReferences { get; }
   #endregion
}