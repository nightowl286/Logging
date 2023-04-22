using TNO.DependencyInjection.Abstractions.Components;

namespace TNO.Logging.Reading.Abstractions.Exceptions;

/// <summary>
/// Denotes a registrant for different types that implement <see cref="IExceptionDataDeserialiser{TExceptionData}"/>.
/// </summary>
public interface IExceptionDataDeserialiserRegistrant
{
   #region Methods
   /// <summary>Registers the exception data deserialisers to the given <paramref name="registrar"/>.</summary>
   /// <param name="registrar">The registrar to register the exception data deserialisers to.</param>
   /// <param name="scope">The scope that can be used to obtain/build dependencies.</param>
   void Register(IExceptionDataDeserialiserRegistrar registrar, IServiceScope scope);
   #endregion
}
