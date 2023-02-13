using System.Collections.Generic;

namespace TNO.Logging.Reading.Abstractions.Readers;

/// <summary>
/// Contains useful extension methods related to the <see cref="IReader{T}"/>.
/// </summary>
public static class ReaderExtensions
{
   #region Methods
   /// <summary>
   /// Enumerates over the data that the <paramref name="reader"/> can read,
   /// until <see cref="IReader{T}.CanRead"/> returns <see langword="false"/>.
   /// </summary>
   /// <typeparam name="T">The type of the data that the given <paramref name="reader"/> handles.</typeparam>
   /// <param name="reader">The reader to enumerate over.</param>
   /// <returns>An enumerable of the data that the reader provides.</returns>
   public static IEnumerable<T> Enumerate<T>(this IReader<T> reader)
   {
      while (reader.CanRead())
      {
         T data = reader.Read();
         yield return data;
      }
   }
   #endregion
}