using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.LogData.Methods.MethodInfos;
using TNO.Logging.Reading.Abstractions.LogData.Methods.ParameterInfos;

namespace TNO.Logging.Reading.LogData.Methods.MethodInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IMethodInfo"/>, version #0.
/// </summary>
[Version(0)]
public sealed class MethodInfoDeserialiser0 : IMethodInfoDeserialiser
{
   #region Fields
   private readonly IParameterInfoDeserialiser _parameterInfoDeserialiser;
   #endregion

   #region Methods
   /// <summary>Creates a new instance of the <see cref="MethodInfoDeserialiser0"/>.</summary>
   /// <param name="parameterInfoDeserialiser">The parameter info deserialiser to use.</param>
   public MethodInfoDeserialiser0(IParameterInfoDeserialiser parameterInfoDeserialiser)
   {
      _parameterInfoDeserialiser = parameterInfoDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IMethodInfo Deserialise(BinaryReader reader)
   {
      ulong declaringTypeId = reader.ReadUInt64();
      ulong returnTypeId = reader.ReadUInt64();
      string name = reader.ReadString();

      int genericTypeIdsCount = reader.Read7BitEncodedInt();
      ulong[] genericTypeIds = new ulong[genericTypeIdsCount];
      for (int i = 0; i < genericTypeIdsCount; i++)
      {
         ulong genericTypeId = reader.ReadUInt64();
         genericTypeIds[i] = genericTypeId;
      }

      int parameterInfosCount = reader.Read7BitEncodedInt();
      IParameterInfo[] parameterInfos = new IParameterInfo[parameterInfosCount];
      for (int i = 0; i < parameterInfosCount; i++)
      {
         IParameterInfo parameterInfo = _parameterInfoDeserialiser.Deserialise(reader);
         parameterInfos[i] = parameterInfo;
      }

      return MethodInfoFactory.Version0(
         declaringTypeId,
         parameterInfos,
         name,
         returnTypeId,
         genericTypeIds);
   }
   #endregion
}
