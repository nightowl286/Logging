namespace TNO.Logging.Reading.Abstractions.Deserialisers;

/// <summary>
/// Denotes a deserialiser for data of the type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the data that can be deserialised.</typeparam>
public interface IDeserialiser<out T> : IDeserialiser
{
   #region Methods
   /// <summary>Deserialise an instance of the type <typeparamref name="T"/>.</summary>
   /// <returns>The deserialised data, of the type <typeparamref name="T"/>.</returns>
   T Deserialise();
   #endregion
}