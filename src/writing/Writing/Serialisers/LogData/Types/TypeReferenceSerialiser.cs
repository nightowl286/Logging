using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.Types;

/// <summary>
/// A serialiser for <see cref="TypeReference"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.TypeReference)]
public class TypeReferenceSerialiser : ISerialiser<TypeReference>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="TypeReferenceSerialiser"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public TypeReferenceSerialiser(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, TypeReference data)
   {
      ulong id = data.Id;
      ITypeInfo typeInfo = data.TypeInfo;

      writer.Write(id);
      _serialiser.Serialise(writer, typeInfo);
   }

   /// <inheritdoc/>
   public ulong Count(TypeReference data)
   {
      ulong infoSize = _serialiser.Count(data.TypeInfo);

      return sizeof(ulong) + infoSize;
   }
   #endregion
}
