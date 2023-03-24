using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="ISimpleStackTraceComponent"/>.
/// </summary>
public sealed class SimpleStackTraceComponentSerialiser : ISimpleStackTraceComponentSerialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, ISimpleStackTraceComponent data)
   {
      int threadId = data.ThreadId;
      string stackTrace = data.StackTrace;

      writer.Write(threadId);
      writer.Write(stackTrace);
   }

   /// <inheritdoc/>
   public ulong Count(ISimpleStackTraceComponent data)
   {
      int stackTraceSize = BinaryWriterSizeHelper.StringSize(data.StackTrace);

      return (ulong)(stackTraceSize + sizeof(int));
   }
   #endregion
}
