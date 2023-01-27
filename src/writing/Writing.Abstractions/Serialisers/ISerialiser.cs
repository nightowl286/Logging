namespace TNO.Logging.Writing.Abstractions.Serialisers;

/// <summary>
/// Denotes a serialiser for data of the type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the data that can be serialised.</typeparam>
public interface ISerialiser<in T>
{
   #region Methods
   /// <summary>Serialises the given <paramref name="data"/>.</summary>
   /// <param name="data">The data to serialise.</param>
   void Serialise(T data);
   #endregion
}
