using System;

namespace TNO.Logging.Writing.Abstractions.Exceptions;

/// <summary>
/// Denotes a registrar for types that implement the <see cref="IExceptionDataHandler{TException, TExceptionData}"/>.
/// </summary>
public interface IExceptionDataHandlerRegistrar
{
   #region Methods
   /// <summary>Registers the given <paramref name="handlerType"/>.</summary>
   /// <param name="handlerType">The type that implements the <see cref="IExceptionDataHandler{TException, TExceptionData}"/>.</param>
   void Register(Type handlerType);

   /// <summary>Registers the given <paramref name="handlerType"/> if a handler for the same exception type hasn't been registered yet.</summary>
   /// <param name="handlerType">The type that implements the <see cref="IExceptionDataHandler{TException, TExceptionData}"/>.</param>
   void RegisterIfMissing(Type handlerType);
   #endregion
}
