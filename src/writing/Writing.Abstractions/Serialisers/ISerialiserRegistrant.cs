using TNO.DependencyInjection.Abstractions.Components;

namespace TNO.Logging.Writing.Abstractions.Serialisers;

/// <summary>
/// Denotes a registrant for different types that implement <see cref="ISerialiser{T}"/>.
/// </summary>
public interface ISerialiserRegistrant
{
   #region Methods
   /// <summary>Registers the serialisers to the given <paramref name="scope"/>.</summary>
   /// <param name="scope">The scope to register the serialisers to.</param>
   void Register(IServiceScope scope);
   #endregion
}
