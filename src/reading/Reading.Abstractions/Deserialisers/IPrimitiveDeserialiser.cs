using System.IO;

namespace TNO.Logging.Reading.Abstractions.Deserialisers;

/// <summary>
/// Denotes a deserialiser for primitive values.
/// </summary>
public interface IPrimitiveDeserialiser
{
   #region Methods
   /// <summary>Deserialises a primitive value using the given <paramref name="reader"/>.</summary>
   /// <param name="reader">The reader to use.</param>
   /// <returns>The deserialised primitive value.</returns>
   object? Deserialise(BinaryReader reader);
   #endregion
}
