using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Exceptions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Exceptions;

namespace TNO.Logging.Reading.Exceptions;

/// <summary>
/// Represents a general deserialiser for <see cref="IExceptionData"/>.
/// </summary>
public class ExceptionDataDeserialiser : IExceptionDataDeserialiser
{
   #region Fields
   private readonly IReadOnlyDictionary<Guid, Type> _exceptionDataDeserialiserTypes;
   private readonly Dictionary<Type, IBinaryDeserialiser<IExceptionData>> _exceptionDataDeserialisersCache = new Dictionary<Type, IBinaryDeserialiser<IExceptionData>>();
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionDataDeserialiser"/>.</summary>
   /// <param name="exceptionDataDeserialiserTypes">The </param>
   public ExceptionDataDeserialiser(IReadOnlyDictionary<Guid, Type> exceptionDataDeserialiserTypes)
   {
      _exceptionDataDeserialiserTypes = exceptionDataDeserialiserTypes;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IExceptionData Deserialise(BinaryReader reader, Guid exceptionGroupId)
   {
      ulong dataCount = reader.ReadUInt64();
      if (_exceptionDataDeserialiserTypes.TryGetValue(exceptionGroupId, out Type? deserialiserType) == false)
      {
         // Note(Nightowl): Unsure if it is safe to trust whether seeking is allowed or not;
         if (reader.BaseStream.CanSeek)
            reader.BaseStream.Seek((long)dataCount, SeekOrigin.Current);
         else
            _ = reader.ReadBytes((int)dataCount);

         // Note(Nightowl): Could always cache this value but not sure if that is important enough;
         return new UnknownExceptionGroupData(exceptionGroupId);
      }

      if (_exceptionDataDeserialisersCache.TryGetValue(deserialiserType, out IBinaryDeserialiser<IExceptionData>? deserialiser) == false)
      {
         deserialiser = (IBinaryDeserialiser<IExceptionData>)Activator.CreateInstance(deserialiserType)!;
         _exceptionDataDeserialisersCache.Add(deserialiserType, deserialiser);
      }

      IExceptionData data = deserialiser.Deserialise(reader);
      return data;
   }
   #endregion
}
