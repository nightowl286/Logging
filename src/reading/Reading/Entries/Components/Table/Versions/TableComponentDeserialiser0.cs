using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Entries.Components.Table.Versions;

/// <summary>
/// A deserialiser for <see cref="ITableComponent"/>, version #0.
/// </summary>
[Version(0)]
public sealed class TableComponentDeserialiser0 : IDeserialiser<ITableComponent>
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TableComponentDeserialiser0"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>
   public TableComponentDeserialiser0(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ITableComponent Deserialise(BinaryReader reader)
   {
      _deserialiser.Deserialise(reader, out ITableInfo tableInfo);

      return TableComponentFactory.Version0(tableInfo);
   }
   #endregion
}
