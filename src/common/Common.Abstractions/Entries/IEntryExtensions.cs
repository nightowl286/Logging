using System.Diagnostics.CodeAnalysis;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// Contains useful extension methods related to the <see cref="IEntry"/>.
/// </summary>
public static class IEntryExtensions
{
   #region Methods
   /// <summary>Tries to get a component of the type <typeparamref name="T"/> with the given <paramref name="kind"/>.</summary>
   /// <typeparam name="T">The type of the <see cref="IComponent"/> to get.</typeparam>
   /// <param name="entry">The entry to try and get the component from.</param>
   /// <param name="kind">The kind of the component.</param>
   /// <param name="component">The obtained component, or <see langword="null"/>.</param>
   /// <returns>
   /// <see langword="true"/> if the <paramref name="component"/> could be obtained, <see langword="false"/> otherwise.
   /// </returns>
   public static bool TryGetComponent<T>(this IEntry entry, ComponentKind kind, [NotNullWhen(true)] out T? component) where T : notnull, IComponent
   {
      if (entry.Components.TryGetValue(kind, out IComponent? untypedComponent))
      {
         if (untypedComponent is T typedComponent)
         {
            component = typedComponent;
            return true;
         }
      }

      component = default;
      return false;
   }
   #endregion
}
