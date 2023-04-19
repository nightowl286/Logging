using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.StackTraces;

/// <summary>
/// A serialiser for <see cref="IStackTraceInfo"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.StackTraceInfo)]
public class StackTraceInfoSerialiser : ISerialiser<IStackTraceInfo>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackTraceInfoSerialiser"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public StackTraceInfoSerialiser(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IStackTraceInfo data)
   {
      int threadId = data.ThreadId;
      IReadOnlyList<IStackFrameInfo> frames = data.Frames;

      writer.Write(threadId);

      writer.Write7BitEncodedInt(frames.Count);
      foreach (IStackFrameInfo frame in frames)
         _serialiser.Serialise(writer, frame);
   }

   /// <inheritdoc/>
   public ulong Count(IStackTraceInfo data)
   {
      int size = sizeof(int);

      int framesCountSize = BinaryWriterSizeHelper.Encoded7BitIntSize(data.Frames.Count);
      ulong framesSize = 0;
      foreach (IStackFrameInfo frame in data.Frames)
         framesSize += _serialiser.Count(frame);

      return (ulong)(size + framesCountSize) + framesSize;
   }
   #endregion
}
