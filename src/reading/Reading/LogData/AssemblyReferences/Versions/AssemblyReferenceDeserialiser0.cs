using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Reading.Abstractions.LogData.AssemblyInfos;
using TNO.Logging.Reading.Abstractions.LogData.AssemblyReferences;

namespace TNO.Logging.Reading.LogData.AssemblyReferences.Versions;

/// <summary>
/// A deserialiser for <see cref="AssemblyReference"/>, version #0.
/// </summary>
public sealed class AssemblyReferenceDeserialiser0 : IAssemblyReferenceDeserialiser
{
   #region Fields
   private readonly IAssemblyInfoDeserialiser _assemblyInfoDeserialiser;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Constructors
   public AssemblyReferenceDeserialiser0(IAssemblyInfoDeserialiser assemblyInfoDeserialiser) => _assemblyInfoDeserialiser = assemblyInfoDeserialiser;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public AssemblyReference Deserialise(BinaryReader reader)
   {
      ulong id = reader.ReadUInt64();
      IAssemblyInfo assemblyInfo = _assemblyInfoDeserialiser.Deserialise(reader);

      return AssemblyReferenceFactory.Version0(id, assemblyInfo);
   }
   #endregion
}
