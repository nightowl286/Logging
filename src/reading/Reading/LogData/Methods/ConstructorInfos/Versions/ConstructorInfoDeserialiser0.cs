using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.LogData.Methods.ConstructorInfos;
using TNO.Logging.Reading.Abstractions.LogData.Methods.ParameterInfos;

namespace TNO.Logging.Reading.LogData.Methods.ConstructorInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IConstructorInfo"/>, version #0.
/// </summary>
[Version(0)]
public sealed class ConstructorInfoDeserialiser0 : IConstructorInfoDeserialiser
{
   #region Fields
   private readonly IParameterInfoDeserialiser _parameterInfoDeserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ConstructorInfoDeserialiser0"/>.</summary>
   /// <param name="parameterInfoDeserialiser">The parameter info deserialiser to use.</param>
   public ConstructorInfoDeserialiser0(IParameterInfoDeserialiser parameterInfoDeserialiser)
   {
      _parameterInfoDeserialiser = parameterInfoDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IConstructorInfo Deserialise(BinaryReader reader)
   {
      ulong declaringTypeId = reader.ReadUInt64();
      string name = reader.ReadString();

      int parameterInfosCount = reader.Read7BitEncodedInt();
      IParameterInfo[] parameterInfos = new IParameterInfo[parameterInfosCount];
      for (int i = 0; i < parameterInfosCount; i++)
      {
         IParameterInfo parameterInfo = _parameterInfoDeserialiser.Deserialise(reader);
         parameterInfos[i] = parameterInfo;
      }

      return ConstructorInfoFactory.Version0(
         declaringTypeId,
         parameterInfos,
         name);
   }
   #endregion
}
