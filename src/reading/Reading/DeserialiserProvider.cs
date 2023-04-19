using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading;

/// <summary>
/// Represents a provider for instances of <see cref="IDeserialiser{T}"/>.
/// </summary>
internal class DeserialiserProvider : IDeserialiserProvider
{
   #region Fields
   private readonly IServiceRequester _requester;
   #endregion
   public DeserialiserProvider(IServiceRequester requester) => _requester = requester;

   #region Methods
   /// <inheritdoc/>
   public IDeserialiser<T> GetDeserialiser<T>() where T : notnull => _requester.Get<IDeserialiser<T>>();
   #endregion
}