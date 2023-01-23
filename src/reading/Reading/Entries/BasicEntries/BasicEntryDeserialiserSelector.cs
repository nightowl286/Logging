using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.BasicEntries;
using TNO.Logging.Reading.Deserialisers;

namespace TNO.Logging.Reading.Entries.BasicEntries;

/// <summary>
/// An <see cref="IDeserialiserSelector{T, U}"/> for versions of the <see cref="IBasicEntryDeserialiser"/>.
/// </summary>
internal class BasicEntryDeserialiserSelector : DeserialiserSelectorBase<IBasicEntryDeserialiser, IBasicEntry>
{
   #region Properties
   /// <inheritdoc/>
   protected override Dictionary<uint, Type> DeserialiserTypes { get; } = new Dictionary<uint, Type>()
   {
      { 0, typeof(BasicEntryDeserialiser0) }
   };
   #endregion
}
