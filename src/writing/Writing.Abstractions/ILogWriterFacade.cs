﻿using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

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
}