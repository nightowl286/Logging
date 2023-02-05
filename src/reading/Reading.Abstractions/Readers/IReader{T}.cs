using System;

namespace TNO.Logging.Reading.Abstractions.Readers;

/// <summary>
/// Denotes a reader for data of the type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the data this reader can read.</typeparam>
public interface IReader<T>
{
   #region Methods
   /// <summary>Checks whether another instance of the <typeparamref name="T"/> can be read.</summary>
   /// <returns><see langword="true"/> if another instance can be read, <see langword="false"/> otherwise.</returns>
   bool CanRead();

   /// <summary>Reads another instance of the type <typeparamref name="T"/>.</summary>
   /// <returns>The read instance of the type <typeparamref name="T"/>.</returns>
   /// <exception cref="InvalidOperationException">
   /// Thrown if another instance cannot be read at the present moment, <see cref="CanRead"/> can be used to check for this.
   /// </exception>
   T Read();
   #endregion
}
