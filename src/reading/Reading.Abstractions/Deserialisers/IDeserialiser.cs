using System.IO;

namespace TNO.Logging.Reading.Abstractions.Deserialisers;

/// <summary>
/// Denotes a deserialiser for data of the type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the data that can be deserialised.</typeparam>
public interface IDeserialiser<T>
{
   #region Methods
   /// <summary>Deserialises an instance of the type <typeparamref name="T"/> using the given <paramref name="reader"/>.</summary>
   /// <param name="reader">The reader to use.</param>
   /// <returns>The deserialised data of the type <typeparamref name="T"/>.</returns>
   T Deserialise(BinaryReader reader);
   #endregion
}
