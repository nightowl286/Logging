using System.Diagnostics.CodeAnalysis;
using TNO.DependencyInjection.Abstractions.Components;
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
   private readonly Dictionary<uint, Type> _deserialiserTypes = new Dictionary<uint, Type>();
   private readonly IServiceBuilder _serviceBuilder;
   #endregion

   #region Constructors
   /// <summary>Creates the base instance for the <see cref="DeserialiserSelectorBase{T, U}"/> class.</summary>
   /// <param name="serviceBuilder">The service builder to use when creating the deserialisers.</param>
   public DeserialiserSelectorBase(IServiceBuilder serviceBuilder) => _serviceBuilder = serviceBuilder;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public bool CanSelect(uint version) => _deserialiserTypes.ContainsKey(version);

   /// <inheritdoc/>
   public bool TrySelect(uint version, [NotNullWhen(true)] out T? deserialiser)
   {
      // Todo(Nightowl): This is not thread-safe, but it might not matter;

      if (_createdDeserialisers.TryGetValue(version, out deserialiser))
         return true;

      if (_deserialiserTypes.TryGetValue(version, out Type? deserialiserType))
      {
         object createdInstance = _serviceBuilder.Build(deserialiserType);

         deserialiser = (T)createdInstance;
         _createdDeserialisers.Add(version, deserialiser);

         return true;
      }

      deserialiser = default;
      return false;
   }

   /// <summary>Registers the deserialiser of the type <typeparamref name="V"/>, with the given <paramref name="version"/>.</summary>
   /// <typeparam name="V">The type of the deserialiser.</typeparam>
   /// <param name="version">The version to associate with the deserialiser of the given type <typeparamref name="V"/>.</param>
   protected void With<V>(uint version) where V : T => _deserialiserTypes.Add(version, typeof(U));
   #endregion
}
