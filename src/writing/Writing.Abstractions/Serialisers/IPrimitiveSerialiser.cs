using System.IO;

namespace TNO.Logging.Writing.Abstractions.Serialisers;

/// <summary>
/// Denotes a serialiser for primitive values.
/// </summary>
public interface IPrimitiveSerialiser
{
   #region Methods
   /// <summary>Serialisers the given primitive <paramref name="data"/> using the given <paramref name="writer"/>.</summary>
   /// <param name="writer">The writer to use.</param>
   /// <param name="data">The primitive data to serialise.</param>
   void Serialise(BinaryWriter writer, object? data);

   /// <summary>Calculates the amount of bytes the given primitive <paramref name="data"/> requires.</summary>
   /// <param name="data">The primitive data to calculate the serialised size for.</param>
   /// <returns>The amount of bytes the given primitive <paramref name="data"/> requires.</returns>
   int Count(object? data);
   #endregion
}
