using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers;

/// <summary>
/// Represents a general serialiser.
/// </summary>
public class Serialiser : ISerialiser
{
   #region Fields
   private readonly IServiceRequester _requester;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="Serialiser"/>.</summary>
   /// <param name="requester">The <see cref="IServiceRequester"/> to use to obtain different serialisers.</param>
   public Serialiser(IServiceRequester requester) => _requester = requester;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise<T>(BinaryWriter writer, T data)
   {
      ISerialiser<T> serialiser = _requester.Get<ISerialiser<T>>();

      serialiser.Serialise(writer, data);
   }

   /// <inheritdoc/>
   public ulong Count<T>(T data)
   {
      ISerialiser<T> serialiser = _requester.Get<ISerialiser<T>>();

      return serialiser.Count(data);
   }
   #endregion
}
