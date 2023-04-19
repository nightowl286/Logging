using System.IO;

namespace TNO.Logging.Reading.Abstractions.Deserialisers;

/// <summary>
/// Denotes a general deserialiser.
/// </summary>
public interface IDeserialiser
{
   #region Methods
   /// <summary>
   /// Deserialises an instance of the type <typeparamref name="T"/>
   /// using the given <paramref name="reader"/>.
   /// </summary>
   /// <typeparam name="T">The type of the data to deserialise.</typeparam>
   /// <param name="reader">The reader to use.</param>
   /// <returns>The deserialised data of the type <typeparamref name="T"/>.</returns>
   T Deserialise<T>(BinaryReader reader);
   #endregion
}
