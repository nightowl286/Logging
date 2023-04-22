using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.TypeReferences.Versions;

/// <summary>
/// A deserialiser for <see cref="TypeReference"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.TypeReference)]
public sealed class TypeReferenceDeserialiser0 : IDeserialiser<TypeReference>
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TypeReferenceDeserialiser0"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>

   public TypeReferenceDeserialiser0(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public TypeReference Deserialise(BinaryReader reader)
   {
      ulong id = reader.ReadUInt64();
      _deserialiser.Deserialise(reader, out ITypeInfo typeInfo);

      return TypeReferenceFactory.Version0(id, typeInfo);
   }
   #endregion
}
