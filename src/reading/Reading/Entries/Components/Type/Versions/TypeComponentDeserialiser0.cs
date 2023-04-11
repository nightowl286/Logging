using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Type;

namespace TNO.Logging.Reading.Entries.Components.Type.Versions;

/// <summary>
/// A deserialiser for <see cref="ITypeComponent"/>, version #0.
/// </summary>
public sealed class TypeComponentDeserialiser0 : ITypeComponentDeserialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ITypeComponent Deserialise(BinaryReader reader)
   {
      ulong assemblyId = reader.ReadUInt64();

      return TypeComponentFactory.Version0(assemblyId);
   }
   #endregion
}