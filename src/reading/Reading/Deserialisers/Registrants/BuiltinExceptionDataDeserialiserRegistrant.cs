using System.Reflection;
using TNO.Common.Extensions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Exceptions;

namespace TNO.Logging.Reading.Deserialisers.Registrants;

/// <summary>
/// Represents an <see cref="IExceptionDataDeserialiserRegistrant"/> that will register built-in exception data deserialisers.
/// </summary>
public sealed class BuiltinExceptionDataDeserialiserRegistrant : IExceptionDataDeserialiserRegistrant
{
   #region Methods
   /// <inheritdoc/>
   public void Register(IExceptionDataDeserialiserRegistrar registrar, IServiceScope scope) => RegisterFromAssembly(registrar);

   private static void RegisterFromAssembly(IExceptionDataDeserialiserRegistrar registrar)
   {
      Assembly assembly = Assembly.GetExecutingAssembly();

      Type[] allTypes = assembly.GetTypes();
      foreach (Type type in allTypes)
      {
         if (type.ImplementsOpenInterface(typeof(IExceptionDataDeserialiser<>)))
            registrar.Register(type);
      }
   }
   #endregion
}
