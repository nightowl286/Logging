using System.Diagnostics.CodeAnalysis;

namespace TNO.Logging.Reading.Abstractions.Deserialisers;

/// <summary>
/// Denotes a selector for deserialisers of the type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the <see cref="IDeserialiser{T}"/>.</typeparam>
/// <typeparam name="U">The type of the data that the deserialiser handles.</typeparam>
public interface IDeserialiserSelector<T, U> where T : notnull, IDeserialiser<U>
{
   #region Methods
   /// <summary>Checks if a deserialiser with the given <paramref name="version"/> can be selected.</summary>
   /// <param name="version">The version of the deserialiser to check for.</param>
   /// <returns><see langword="true"/> if there is a deserialiser for the given <paramref name="version"/>, <see langword="false"/> otherwise.</returns>
   /// <remarks>This will not create any instances of the associated deserialiser.</remarks>
   bool CanSelect(uint version);

   /// <summary>Tries to select a <see cref="IDeserialiser{T}"/> based on the given <paramref name="version"/>.</summary>
   /// <param name="version">The version of the deserialiser.</param>
   /// <param name="deserialiser">The deserialiser that was selected.</param>
   /// <returns><see langword="true"/> if a <paramref name="deserialiser"/> could be selected, <see langword="false"/> otherwise.</returns>
   bool TrySelect(uint version, [NotNullWhen(true)] out T? deserialiser);
   #endregion
}
