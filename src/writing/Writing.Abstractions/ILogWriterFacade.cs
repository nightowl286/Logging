using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Abstractions;

/// <summary>
/// Denotes a facade for the log writing system.
/// </summary>
public interface ILogWriterFacade
{
   #region Methods
   /// <summary>Gets a serialiser of the type <typeparamref name="T"/>.</summary>
   /// <typeparam name="T">The type of the serialiser.</typeparam>
   /// <returns>A serialiser of the type <typeparamref name="T"/>.</returns>
   T GetSerialiser<T>() where T : notnull, ISerialiser;
   #endregion
}
