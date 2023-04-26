using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.General;

/// <summary>
/// A serialiser for <see cref="ICollectionInfo"/>.
/// </summary>
public sealed class CollectionInfoSerialiser : ISerialiser<ICollectionInfo>
{
   #region Fields
   private readonly IPrimitiveSerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="CollectionInfoSerialiser"/>.</summary>
   /// <param name="serialiser">The <see cref="IPrimitiveSerialiser"/> to use.</param>
   public CollectionInfoSerialiser(IPrimitiveSerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ICollectionInfo data)
   {
      IReadOnlyCollection<object?> collection = data.Collection;
      int count = collection.Count;

      writer.Write7BitEncodedInt(count);
      foreach (object? value in collection)
         _serialiser.Serialise(writer, value);
   }

   /// <inheritdoc/>
   public int Count(ICollectionInfo data)
   {
      int countSize = BinaryWriterSizeHelper.Encoded7BitIntSize(data.Collection.Count);
      int total = countSize;
      foreach (object? value in data.Collection)
         total += _serialiser.Count(value);

      return total;
   }
   #endregion
}
