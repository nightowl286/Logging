using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.Methods.MethodInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IMethodInfo"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.MethodInfo)]
public sealed class MethodInfoDeserialiser0 : IDeserialiser<IMethodInfo>
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Methods
   /// <summary>Creates a new instance of the <see cref="MethodInfoDeserialiser0"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>

   public MethodInfoDeserialiser0(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
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
         _deserialiser.Deserialise(reader, out IParameterInfo parameterInfo);
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
