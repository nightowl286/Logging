using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.LogData.Methods.ConstructorInfos;
using TNO.Logging.Reading.Abstractions.LogData.Methods.MethodInfos;

namespace TNO.Logging.Reading.LogData.Methods;

/// <summary>
/// Represents a <see cref="IBinaryDeserialiser{T}"/> dispatcher that will
/// deserialise a <see cref="IMethodBaseInfo"/> based on its <see cref="MethodKind"/>.
/// </summary>
public class MethodBaseInfoDeserialiserDispatcher : IMethodBaseInfoDeserialiserDispatcher
{
   #region Fields
   private readonly IMethodInfoDeserialiser _methodInfoDeserialiser;
   private readonly IConstructorInfoDeserialiser _constructorInfoDeserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="MethodBaseInfoDeserialiserDispatcher"/>.</summary>
   /// <param name="methodInfoDeserialiser">The method info deserialiser to use.</param>
   /// <param name="constructorInfoDeserialiser">The constructor info deserialiser to use.</param>
   public MethodBaseInfoDeserialiserDispatcher(
      IMethodInfoDeserialiser methodInfoDeserialiser,
      IConstructorInfoDeserialiser constructorInfoDeserialiser)
   {
      _methodInfoDeserialiser = methodInfoDeserialiser;
      _constructorInfoDeserialiser = constructorInfoDeserialiser;
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
         MethodKind.Method => _methodInfoDeserialiser.Deserialise(reader),
         MethodKind.Constructor => _constructorInfoDeserialiser.Deserialise(reader),

         _ => throw new InvalidDataException($"Unknown method kind ({methodKind}).")
      };
   }
   #endregion
}
