using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.StackTraces;

namespace TNO.Logging.Writing.Serialisers.LogData.StackTraces;

/// <summary>
/// A serialiser for <see cref="IStackTraceInfo"/>.
/// </summary>
[Version(0)]
public class StackTraceInfoSerialiser : IStackTraceInfoSerialiser
{
   #region Fields
   private readonly IStackFrameInfoSerialiser _stackFrameInfoSerialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackTraceInfoSerialiser"/>.</summary>
   /// <param name="stackFrameInfoSerialiser">The <see cref="IStackFrameInfoSerialiser"/> to use.</param>
   public StackTraceInfoSerialiser(IStackFrameInfoSerialiser stackFrameInfoSerialiser)
   {
      _stackFrameInfoSerialiser = stackFrameInfoSerialiser;
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
         _stackFrameInfoSerialiser.Serialise(writer, frame);
   }

   /// <inheritdoc/>
   public ulong Count(IStackTraceInfo data)
   {
      int size = sizeof(int);

      int framesCountSize = BinaryWriterSizeHelper.Encoded7BitIntSize(data.Frames.Count);
      ulong framesSize = 0;
      foreach (IStackFrameInfo frame in data.Frames)
         framesSize += _stackFrameInfoSerialiser.Count(frame);

      return (ulong)(size + framesCountSize) + framesSize;
   }
   #endregion
}
