using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Entries.Components.Assembly.Versions;

/// <summary>
/// A deserialiser for <see cref="IAssemblyComponent"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.Assembly)]
public sealed class AssemblyComponentDeserialiser0 : IDeserialiser<IAssemblyComponent>
{
   #region Methods
   /// <inheritdoc/>
   public IAssemblyComponent Deserialise(BinaryReader reader)
   {
      ulong assemblyId = reader.ReadUInt64();

      return AssemblyComponentFactory.Version0(assemblyId);
   }
   #endregion
}