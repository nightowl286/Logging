using TNO.DependencyInjection.Abstractions.Components;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <summary>
/// Denotes a registrant for different types that implement <see cref="IExceptionDataHandler{TException, TExceptionData}"/>.
/// </summary>
public interface IExceptionDataHandlerRegistrant
{
   #region Methods
   /// <summary>Registers the exception data handlers to the given <paramref name="registrar"/>.</summary>
   /// <param name="registrar">The registrar to register the exception data handlers to.</param>
   /// <param name="scope">The scope that can be used to obtain/build dependencies.</param>
   void Register(IExceptionDataHandlerRegistrar registrar, IServiceScope scope);
   #endregion
}
