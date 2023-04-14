using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Tables;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="ITableComponent"/>.
/// </summary>
public sealed class TableComponentSerialiser : ITableComponentSerialiser
{
   #region Fields
   private readonly ITableInfoSerialiser _tableInfoSerialiser;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TableComponentSerialiser"/>.</summary>
   /// <param name="tableInfoSerialiser">The <see cref="ITableInfoSerialiser"/> to use.</param>
   public TableComponentSerialiser(ITableInfoSerialiser tableInfoSerialiser)
   {
      _tableInfoSerialiser = tableInfoSerialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ITableComponent data) => _tableInfoSerialiser.Serialise(writer, data.Table);

   /// <inheritdoc/>
   public ulong Count(ITableComponent data) => _tableInfoSerialiser.Count(data.Table);
   #endregion
}