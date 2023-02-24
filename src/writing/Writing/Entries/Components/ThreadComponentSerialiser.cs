using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IThreadComponent"/>.
/// </summary>
public sealed class ThreadComponentSerialiser : IThreadComponentSerialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IThreadComponent data)
   {
      int id = data.ManagedId;
      string name = data.Name;
      bool isThreadPool = data.IsThreadPoolThread;

      ushort rawState = (ushort)data.State;
      byte rawPriority = (byte)data.Priority;
      byte rawApartmentState = (byte)data.ApartmentState;

      writer.Write(id);
      writer.Write(name);
      writer.Write(isThreadPool);
      writer.Write(rawState);
      writer.Write(rawPriority);
      writer.Write(rawApartmentState);
   }

   /// <inheritdoc/>
   public ulong Count(IThreadComponent data)
   {
      int nameSize = BinaryWriterSizeHelper.StringSize(data.Name);

      int size =
         nameSize +
         sizeof(int) +
         sizeof(ushort) +
         sizeof(bool) +
         (sizeof(byte) * 2);

      return (ulong)size;
   }
   #endregion
}
