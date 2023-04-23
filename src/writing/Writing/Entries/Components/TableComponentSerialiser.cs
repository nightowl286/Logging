using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="ITableComponent"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.Table)]
public sealed class TableComponentSerialiser : ISerialiser<ITableComponent>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TableComponentSerialiser"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public TableComponentSerialiser(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ITableComponent data) => _serialiser.Serialise(writer, data.Table);

   /// <inheritdoc/>
   public int Count(ITableComponent data) => _serialiser.Count(data.Table);
   #endregion
}