﻿using TNO.Logging.Common.Abstractions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Readers;

namespace TNO.Logging.Reading.Abstractions;

/// <summary>
/// Denotes a facade for the log reading system.
/// </summary>
public interface ILogReaderFacade
{
   #region Methods
   /// <summary>Generates a <see cref="IDeserialiserProvider"/> based on the given <paramref name="map"/>.</summary>
   /// <param name="map">The version map to use.</param>
   /// <returns>The generated <see cref="IDeserialiserProvider"/>.</returns>
   IDeserialiserProvider GenerateProvider(DataVersionMap map);

   /// <summary>Gets a deserialiser of the type <typeparamref name="T"/>.</summary>
   /// <typeparam name="T">The type of the deserialiser.</typeparam>
   /// <returns>A deserialiser of the type <typeparamref name="T"/>.</returns>
   /// <remarks>This will only work for non-versioned deserialisers.</remarks>
   T GetDeserialiser<T>() where T : notnull, IDeserialiser;
   #endregion

   #region File System Reader
   /// <summary>Creates a log reader that can read a log that was saved to the file system.</summary>
   /// <param name="path">The path of the log.</param>
   /// <returns>A newly created file system log reader.</returns>
   IFileSystemLogReader ReadFromFileSystem(string path);
   #endregion
}