using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Readers;

namespace TNO.Logging.Reading.Readers;
internal class BinaryDeserialiserReader<TDeserialiser, TData> : IReader<TData>, IDisposable
   where TDeserialiser : IBinaryDeserialiser<TData>
{
   #region Fields
   private readonly TDeserialiser _deserialiser;
   private readonly BinaryReader _reader;
   #endregion
   public BinaryDeserialiserReader(BinaryReader reader, TDeserialiser deserialiser)
   {
      _reader = reader;
      _deserialiser = deserialiser;
   }

   #region Methods
   public bool CanRead() => _reader.BaseStream.Position < _reader.BaseStream.Length;
   public TData Read() => _deserialiser.Deserialise(_reader);
   public void Dispose() => _reader.Dispose();
   #endregion
}
