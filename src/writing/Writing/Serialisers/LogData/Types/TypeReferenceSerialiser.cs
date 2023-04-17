using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Types;

namespace TNO.Logging.Writing.Serialisers.LogData.Types;

/// <summary>
/// A serialiser for <see cref="TypeReference"/>.
/// </summary>
[Version(0)]
public class TypeReferenceSerialiser : ITypeReferenceSerialiser
{
   #region Fields
   private readonly ITypeInfoSerialiser _typeInfoSerialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TypeReferenceSerialiser"/>.</summary>
   /// <param name="typeInfoSerialiser">The <see cref="ITypeInfoSerialiser"/> to use.</param>
   public TypeReferenceSerialiser(ITypeInfoSerialiser typeInfoSerialiser)
   {
      _typeInfoSerialiser = typeInfoSerialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, TypeReference data)
   {
      ulong id = data.Id;
      ITypeInfo typeInfo = data.TypeInfo;

      writer.Write(id);
      _typeInfoSerialiser.Serialise(writer, typeInfo);
   }

   /// <inheritdoc/>
   public ulong Count(TypeReference data)
   {
      ulong infoSize = _typeInfoSerialiser.Count(data.TypeInfo);

      return sizeof(ulong) + infoSize;
   }
   #endregion
}
