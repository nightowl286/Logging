using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.Methods;

/// <summary>
/// Represents a <see cref="IDeserialiser{T}"/> dispatcher that will
/// deserialise a <see cref="IMethodBaseInfo"/> based on its <see cref="MethodKind"/>.
/// </summary>
public class MethodBaseInfoDeserialiserDispatcher : IDeserialiser<IMethodBaseInfo>
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="MethodBaseInfoDeserialiserDispatcher"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>
   public MethodBaseInfoDeserialiserDispatcher(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IMethodBaseInfo Deserialise(BinaryReader reader)
   {
      byte rawMethodKind = reader.ReadByte();
      MethodKind methodKind = (MethodKind)rawMethodKind;

      return methodKind switch
      {
         MethodKind.Method => _deserialiser.Deserialise<IMethodInfo>(reader),
         MethodKind.Constructor => _deserialiser.Deserialise<IConstructorInfo>(reader),

         _ => throw new InvalidDataException($"Unknown method kind ({methodKind}).")
      };
   }
   #endregion
}
