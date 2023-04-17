using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Assemblies;

namespace TNO.Logging.Writing.Serialisers.LogData.Assemblies;

/// <summary>
/// A serialiser for <see cref="AssemblyReference"/>.
/// </summary>
[Version(0)]
public class AssemblyReferenceSerialiser : IAssemblyReferenceSerialiser
{
   #region Fields
   private readonly IAssemblyInfoSerialiser _assemblyInfoSerialiser;
   #endregion
   #region Constructors
   /// <summary>Creates a new instance of the <see cref="AssemblyReferenceSerialiser"/>.</summary>
   /// <param name="assemblyInfoSerialiser">The <see cref="IAssemblyInfoSerialiser"/> to use.</param>
   public AssemblyReferenceSerialiser(IAssemblyInfoSerialiser assemblyInfoSerialiser)
   {
      _assemblyInfoSerialiser = assemblyInfoSerialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, AssemblyReference data)
   {
      ulong id = data.Id;
      IAssemblyInfo assemblyInfo = data.AssemblyInfo;

      writer.Write(id);
      _assemblyInfoSerialiser.Serialise(writer, assemblyInfo);
   }

   /// <inheritdoc/>
   public ulong Count(AssemblyReference data)
   {
      ulong infoSize = _assemblyInfoSerialiser.Count(data.AssemblyInfo);

      return sizeof(ulong) + infoSize;
   }
   #endregion
}
