using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IMessageComponent"/>.
/// </summary>
[Version(0)]
public sealed class MessageComponentSerialiser : IMessageComponentSerialiser
{
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