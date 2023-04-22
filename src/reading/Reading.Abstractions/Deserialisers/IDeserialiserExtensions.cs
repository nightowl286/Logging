using System.IO;

namespace TNO.Logging.Reading.Abstractions.Deserialisers;

/// <summary>
/// Contains useful extension methods related to the <see cref="IDeserialiser"/>.
/// </summary>
public static class IDeserialiserExtensions
{
   #region Methods
   /// <summary>
   /// Deserialisers an instance of the type <typeparamref name="T"/>,
   /// using the given <paramref name="reader"/>.
   /// </summary>
   /// <typeparam name="T">The type of the data to deserialise.</typeparam>
   /// <param name="deserialiser">The deserialiser to use.</param>
   /// <param name="reader">The reader to use.</param>
   /// <param name="data">The deserialised data of the type <typeparamref name="T"/>.</param>
   public static void Deserialise<T>(this IDeserialiser deserialiser, BinaryReader reader, out T data) where T : notnull
   {
      data = deserialiser.Deserialise<T>(reader);
   }
   #endregion
}
