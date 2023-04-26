using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;
#if !NET6_0_OR_GREATER
using TNO.Logging.Reading.Deserialisers;
#endif

namespace TNO.Logging.Reading.LogData.Methods.ConstructorInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IConstructorInfo"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.ConstructorInfo)]
public sealed class ConstructorInfoDeserialiser0 : IDeserialiser<IConstructorInfo>
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ConstructorInfoDeserialiser0"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>

   public ConstructorInfoDeserialiser0(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
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
         _deserialiser.Deserialise(reader, out IParameterInfo parameterInfo);
         parameterInfos[i] = parameterInfo;
      }

      return ConstructorInfoFactory.Version0(
         declaringTypeId,
         parameterInfos,
         name);
   }
   #endregion
}
