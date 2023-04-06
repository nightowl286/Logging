using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Reading.Abstractions.LogData.TypeInfos;
using TNO.Logging.Reading.Abstractions.LogData.TypeReferences;

namespace TNO.Logging.Reading.LogData.TypeReferences.Versions;

/// <summary>
/// A deserialiser for <see cref="TypeReference"/>, version #0.
/// </summary>
public sealed class TypeReferenceDeserialiser0 : ITypeReferenceDeserialiser
{
   #region Fields
   private readonly ITypeInfoDeserialiser _assemblyInfoDeserialiser;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Constructors
   public TypeReferenceDeserialiser0(ITypeInfoDeserialiser assemblyInfoDeserialiser) => _assemblyInfoDeserialiser = assemblyInfoDeserialiser;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public TypeReference Deserialise(BinaryReader reader)
   {
      ulong id = reader.ReadUInt64();
      ITypeInfo assemblyInfo = _assemblyInfoDeserialiser.Deserialise(reader);

      return TypeReferenceFactory.Version0(id, assemblyInfo);
   }
   #endregion
}
