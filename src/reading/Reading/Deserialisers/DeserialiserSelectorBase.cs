using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Deserialisers;

/// <summary>
/// A base class for deserialiser selectors.
/// </summary>
/// <typeparam name="T">The type of the <see cref="IDeserialiser{T}"/>.</typeparam>
/// <typeparam name="U">The type of the data that the deserialiser handles.</typeparam>
public abstract class DeserialiserSelectorBase<T, U> : IDeserialiserSelector<T, U>
   where T : IDeserialiser<U>
{
   #region Fields
   private readonly Dictionary<uint, T> _createdDeserialisers = new Dictionary<uint, T>();
   #endregion

   #region Properties
   /// <summary>A dictionary that associates a deserialiser version with its type.</summary>
   protected abstract Dictionary<uint, Type> DeserialiserTypes { get; }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public bool CanSelect(uint version) => DeserialiserTypes.ContainsKey(version);

   /// <inheritdoc/>
   public bool TrySelect(uint version, [NotNullWhen(true)] out T? deserialiser)
   {
      // Todo(Nightowl): This is not thread-safe, but it might not matter;

      if (_createdDeserialisers.TryGetValue(version, out deserialiser))
         return true;

      if (DeserialiserTypes.TryGetValue(version, out Type? deserialiserType))
      {
         object? createdInstance = Activator.CreateInstance(deserialiserType);
         Debug.Assert(createdInstance is not null);

         deserialiser = (T)createdInstance;
         _createdDeserialisers.Add(version, deserialiser);

         return true;
      }

      deserialiser = default;
      return false;
   }
   #endregion
}
