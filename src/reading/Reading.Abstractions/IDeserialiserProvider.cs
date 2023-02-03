using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Abstractions;

/// <summary>
/// Denotes a provider for instances of <see cref="IDeserialiser"/>.
/// </summary>
public interface IDeserialiserProvider
{
   #region Methods
   /// <summary>Gets a deserialiser of the type <typeparamref name="T"/>.</summary>
   /// <typeparam name="T">The type of the deserialiser.</typeparam>
   /// <returns>A deserialiser of the type <typeparamref name="T"/>.</returns>
   T GetDeserialiser<T>() where T : notnull, IDeserialiser;
   #endregion
}
