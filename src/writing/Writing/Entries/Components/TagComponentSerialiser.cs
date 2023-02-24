using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="ITagComponent"/>.
/// </summary>
public sealed class TagComponentSerialiser : ITagComponentSerialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ITagComponent data)
   {
      ulong id = data.TagId;
      writer.Write(id);
   }

   /// <inheritdoc/>
   public ulong Count(ITagComponent data) => sizeof(ulong);
   #endregion
}