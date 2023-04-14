using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Reading.Abstractions.Entries.Components.Table;
using TNO.Logging.Reading.Abstractions.LogData.Tables;

namespace TNO.Logging.Reading.Entries.Components.Table.Versions;

/// <summary>
/// A deserialiser for <see cref="ITableComponent"/>, version #0.
/// </summary>
public sealed class TableComponentDeserialiser0 : ITableComponentDeserialiser
{
   #region Fields
   private readonly ITableInfoDeserialiser _tableInfoDeserialiser;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TableComponentDeserialiser0"/>.</summary>
   /// <param name="tableInfoDeserialiser">The <see cref="ITableInfoDeserialiser"/> to use.</param>
   public TableComponentDeserialiser0(ITableInfoDeserialiser tableInfoDeserialiser)
   {
      _tableInfoDeserialiser = tableInfoDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ITableComponent Deserialise(BinaryReader reader)
   {
      ITableInfo tableInfo = _tableInfoDeserialiser.Deserialise(reader);

      return TableComponentFactory.Version0(tableInfo);
   }
   #endregion
}
