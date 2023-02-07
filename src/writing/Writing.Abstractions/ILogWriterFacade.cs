using System;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Abstractions;

/// <summary>
/// Denotes a facade for the log writing system.
/// </summary>
public interface ILogWriterFacade
{
   #region Methods
   /// <summary>Gets a serialiser of the type <typeparamref name="T"/>.</summary>
   /// <typeparam name="T">The type of the serialiser.</typeparam>
   /// <returns>A serialiser of the type <typeparamref name="T"/>.</returns>
   T GetSerialiser<T>() where T : notnull, ISerialiser;

   /// <summary>Generates a <see cref="DataVersionMap"/> of the available serialisers.</summary>
   /// <returns>The generated version map.</returns>
   DataVersionMap GetVersionMap();
   #endregion

   #region File System Writer
   /// <summary>Creates a new logger that will save to the file system.</summary>
   /// <param name="directory">The directory to save the log in.</param>
   /// <returns>
   /// A logger instance which can be disposed in order to close the log.
   /// </returns>
   IDisposableLogger CreateOnFileSystem(string directory);

   /// <summary>Creates a new logger that will save to the file system.</summary>
   /// <param name="directory">
   /// The directory to which the log should be saved. A new child directory
   /// (formatted based on <see cref="DateTime.Now"/>) will be created.</param>
   /// <returns>
   /// A logger instance which can be disposed in order to close the log.
   /// </returns>
   IDisposableLogger CreateDatedOnFileSystem(string directory);
   #endregion
}
