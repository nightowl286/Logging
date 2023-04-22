using System.Reflection;
using TNO.Common.Extensions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Writing.Abstractions.Exceptions;

namespace TNO.Logging.Writing.Serialisers.Registrants;

/// <summary>
/// Represents an <see cref="IExceptionDataHandlerRegistrant"/> that will register built-in exception data handlers.
/// </summary>
public sealed class BuiltinExceptionDataHandlerRegistrant : IExceptionDataHandlerRegistrant
{
   #region Methods
   /// <inheritdoc/>
   public void Register(IExceptionDataHandlerRegistrar registrar, IServiceScope scope) => RegisterFromAssembly(registrar);

   private static void RegisterFromAssembly(IExceptionDataHandlerRegistrar registrar)
   {
      Assembly assembly = Assembly.GetExecutingAssembly();

      Type[] allTypes = assembly.GetTypes();
      foreach (Type type in allTypes)
      {
         if (type.ImplementsOpenInterface(typeof(IExceptionDataHandler<,>)))
            registrar.Register(type);
      }
   }
   #endregion
}
