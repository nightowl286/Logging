using System.IO;

namespace TNO.Logging.Writing.Abstractions.Serialisers;

/// <summary>
/// Denotes a serialiser for data of the type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the data that can be serialised.</typeparam>
public interface ISerialiser<in T>
{
   #region Methods
   /// <summary>Serialises the given <paramref name="data"/> using the given <paramref name="writer"/>.</summary>
   /// <param name="writer">The writer to use.</param>
   /// <param name="data">The data to serialise.</param>
   void Serialise(BinaryWriter writer, T data);

   /// <summary>Calculates the amount of bytes the given <paramref name="data"/> requires.</summary>
   /// <param name="data">The data to calculate the serialised size for.</param>
   /// <returns>The amount of bytes the given <paramref name="data"/> requires.</returns>
   ulong Count(T data);
   #endregion
}