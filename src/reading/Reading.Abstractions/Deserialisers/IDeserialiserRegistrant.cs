using TNO.DependencyInjection.Abstractions.Components;

namespace TNO.Logging.Reading.Abstractions.Deserialisers;

/// <summary>
/// Denotes a registrant for different types that implement <see cref="IDeserialiser{T}"/>.
/// </summary>
public interface IDeserialiserRegistrant
{
   #region Methods
   /// <summary>Registers the deserialisers to the given <paramref name="scope"/>.</summary>
   /// <param name="scope">The scope to register the deserialisers to.</param>
   void Register(IServiceScope scope);
   #endregion
}
