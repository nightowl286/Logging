using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Deserialisers;

/// <summary>
/// Represents a general deserialiser.
/// </summary>
public class Deserialiser : IDeserialiser
{
   #region Fields
   private readonly IServiceRequester _requester;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="Deserialiser"/>.</summary>
   /// <param name="requester">The <see cref="IServiceRequester"/> to use to obtain different deserialisers.</param>
   public Deserialiser(IServiceRequester requester) => _requester = requester;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public T Deserialise<T>(BinaryReader reader) where T : notnull
   {
      IDeserialiser<T> deserialiser = Get<T>();

      return deserialiser.Deserialise(reader);
   }

   /// <inheritdoc/>
   public IDeserialiser<T> Get<T>() where T : notnull => _requester.Get<IDeserialiser<T>>();
   #endregion
}
