using System.IO;

namespace TNO.Logging.Writing.Abstractions.Serialisers;

/// <summary>
/// Denotes a general serialiser.
/// </summary>
public interface ISerialiser
{
   #region Methods
   /// <summary>Serialises the given <paramref name="data"/> using the given <paramref name="writer"/>.</summary>
   /// <typeparam name="T">The type of the <paramref name="data"/> to write.</typeparam>
   /// <param name="writer">The writer to use.</param>
   /// <param name="data">The data to serialise.</param>
   void Serialise<T>(BinaryWriter writer, T data) where T : notnull;

   /// <summary>Calculates the amount of bytes the given <paramref name="data"/> requires.</summary>
   /// <typeparam name="T">The type of the <paramref name="data"/> to count the size of.</typeparam>
   /// <param name="data">The data to calculate the serialised size for.</param>
   /// <returns>The amount of bytes the given <paramref name="data"/> requires.</returns>
   ulong Count<T>(T data) where T : notnull;

   /// <summary>Gets an <see cref="ISerialiser{T}"/> for the given type <typeparamref name="T"/>.</summary>
   /// <typeparam name="T">The type to get the <see cref="ISerialiser{T}"/> for.</typeparam>
   /// <returns>The obtained serialiser.</returns>
   ISerialiser<T> Get<T>() where T : notnull;
   #endregion
}
