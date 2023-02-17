using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IMessageComponent"/>.
/// </summary>
public sealed class MessageComponentSerialiser : IMessageComponentSerialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;

   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IMessageComponent data)
   {
      string message = data.Message;
      writer.Write(message);
   }

   /// <inheritdoc/>
   public ulong Count(IMessageComponent data)
   {
      string message = data.Message;
      return (ulong)BinaryWriterSizeHelper.StringSize(message);
   }
   #endregion
}