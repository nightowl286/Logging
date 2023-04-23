using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.Assemblies;

/// <summary>
/// A serialiser for <see cref="AssemblyReference"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.AssemblyReference)]
public class AssemblyReferenceSerialiser : ISerialiser<AssemblyReference>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="AssemblyReferenceSerialiser"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public AssemblyReferenceSerialiser(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, AssemblyReference data)
   {
      ulong id = data.Id;
      IAssemblyInfo assemblyInfo = data.AssemblyInfo;

      writer.Write(id);
      _serialiser.Serialise(writer, assemblyInfo);
   }

   /// <inheritdoc/>
   public int Count(AssemblyReference data)
   {
      int infoSize = _serialiser.Count(data.AssemblyInfo);

      return sizeof(ulong) + infoSize;
   }
   #endregion
}
