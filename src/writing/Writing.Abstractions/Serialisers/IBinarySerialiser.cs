using System.IO;

namespace TNO.Logging.Writing.Abstractions.Serialisers;

/// <summary>
/// Denotes a serialiser for data of the type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the data that can be serialised.</typeparam>
public interface IBinarySerialiser<in T> : ISerialiser
{
   #region Methods
   /// <summary>Serialises the given <paramref name="data"/> using the given <paramref name="writer"/>.</summary>
   /// <param name="writer">The writer to use.</param>
   /// <param name="data">The data to serialise.</param>
   void Serialise(BinaryWriter writer, T data);
   #endregion
}