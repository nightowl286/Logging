using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.LogData.TypeInfos;
using TNO.Logging.Reading.Abstractions.LogData.TypeReferences;

namespace TNO.Logging.Reading.LogData.TypeReferences.Versions;

/// <summary>
/// A deserialiser for <see cref="TypeReference"/>, version #0.
/// </summary>
[Version(0)]
public sealed class TypeReferenceDeserialiser0 : ITypeReferenceDeserialiser
{
   #region Fields
   private readonly ITypeInfoDeserialiser _typeInfoDeserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TypeReferenceDeserialiser0"/>.</summary>
   /// <param name="typeInfoDeserialiser">The <see cref="ITypeInfoDeserialiser"/> to use.</param>
   public TypeReferenceDeserialiser0(ITypeInfoDeserialiser typeInfoDeserialiser) => _typeInfoDeserialiser = typeInfoDeserialiser;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public TypeReference Deserialise(BinaryReader reader)
   {
      ulong id = reader.ReadUInt64();
      ITypeInfo typeInfo = _typeInfoDeserialiser.Deserialise(reader);

      return TypeReferenceFactory.Version0(id, typeInfo);
   }
   #endregion
}
