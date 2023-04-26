using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.General;

/// <summary>
/// A deserialiser for <see cref="ICollectionInfo"/>.
/// </summary>
public sealed class CollectionInfoDeserialiser : IDeserialiser<ICollectionInfo>
{
   #region Fields
   private readonly IPrimitiveDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="CollectionInfoDeserialiser"/>.</summary>
   /// <param name="deserialiser">The <see cref="IPrimitiveDeserialiser"/> to use.</param>
   public CollectionInfoDeserialiser(IPrimitiveDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ICollectionInfo Deserialise(BinaryReader reader)
   {
      int collectionSize = reader.Read7BitEncodedInt();
      List<object?> collection = new List<object?>(collectionSize);
      for (int i = 0; i < collectionSize; i++)
      {
         object? value = _deserialiser.Deserialise(reader);
         collection.Add(value);
      }

      ICollectionInfo collectionInfo = new CollectionInfo(collection);

      return collectionInfo;
   }
   #endregion
}
