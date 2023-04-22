using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.LogData.AssemblyReferences.Versions;

/// <summary>
/// A deserialiser for <see cref="AssemblyReference"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.AssemblyReference)]
public sealed class AssemblyReferenceDeserialiser0 : IDeserialiser<AssemblyReference>
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="AssemblyReferenceDeserialiser0"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>
   public AssemblyReferenceDeserialiser0(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public AssemblyReference Deserialise(BinaryReader reader)
   {
      ulong id = reader.ReadUInt64();
      _deserialiser.Deserialise(reader, out IAssemblyInfo assemblyInfo);

      return AssemblyReferenceFactory.Version0(id, assemblyInfo);
   }
   #endregion
}
