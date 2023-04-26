using TNO.DependencyInjection.Abstractions.Components;
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
   private readonly IExceptionDataDeserialiserRequester _requester;
   private readonly IServiceBuilder _builder;

   private readonly Dictionary<Type, IDeserialiser<IExceptionData>> _deserialiserCache = new Dictionary<Type, IDeserialiser<IExceptionData>>();
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionDataDeserialiser"/>.</summary>
   /// <param name="requester">The <see cref="IExceptionDataDeserialiserRequester"/> to use.</param>
   /// <param name="builder">The <see cref="IServiceBuilder"/> to use.</param>
   public ExceptionDataDeserialiser(IExceptionDataDeserialiserRequester requester, IServiceBuilder builder)
   {
      _requester = requester;
      _builder = builder;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IExceptionData Deserialise(BinaryReader reader, Guid id)
   {
      ulong dataCount = reader.ReadUInt32();
      if (_requester.ById(id, out IExceptionDataDeserialiserInfo? info) == false)
      {
         // Note(Nightowl): Unsure if it is safe to trust whether seeking is allowed or not;
         if (reader.BaseStream.CanSeek)
            reader.BaseStream.Seek((long)dataCount, SeekOrigin.Current);
         else
            _ = reader.ReadBytes((int)dataCount);

         // Note(Nightowl): Could always cache this value but not sure if that is important enough;
         return new UnknownExceptionGroupData(id);
      }

      if (_deserialiserCache.TryGetValue(info.DeserialiserType, out IDeserialiser<IExceptionData>? deserialiser) == false)
      {
         deserialiser = (IDeserialiser<IExceptionData>)_builder.Build(info.DeserialiserType);
         _deserialiserCache.Add(info.DeserialiserType, deserialiser);
      }

      IExceptionData data = deserialiser.Deserialise(reader);
      return data;
   }
   #endregion
}
