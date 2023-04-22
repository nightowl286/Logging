using System.Reflection;
using TNO.Common.Extensions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.Registrants;

/// <summary>
/// Represents an <see cref="ISerialiserRegistrant"/> that will register the built-in serialisers.
/// </summary>
public sealed class BuiltinSerialiserRegistrant : ISerialiserRegistrant
{
   #region Methods
   /// <inheritdoc/>
   public void Register(IServiceScope scope) => RegisterFromAssembly(scope);

   private static void RegisterFromAssembly(IServiceScope scope)
   {
      Assembly assembly = Assembly.GetExecutingAssembly();

      Type[] allTypes = assembly.GetTypes();
      foreach (Type type in allTypes)
      {
         IEnumerable<Type> implementations = type.GetOpenInterfaceImplementations(typeof(ISerialiser<>));
         foreach (Type implementation in implementations)
            scope.Registrar.Singleton(implementation, type);
      }
   }
   #endregion
}
