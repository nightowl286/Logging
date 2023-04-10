using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackFrameInfos;
using TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackTraceInfos;
using TNO.Logging.Reading.LogData.Methods.StackTraceInfos;

namespace TNO.Logging.Reading.LogData.StackTraces.StackTraceInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IStackTraceInfo"/>, version #0.
/// </summary>
public sealed class StackTraceInfoDeserialiser0 : IStackTraceInfoDeserialiser
{
   #region Fields
   private readonly IStackFrameInfoDeserialiser _stackFrameInfoDeserialiser;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackTraceInfoDeserialiser0"/>.</summary>
   /// <param name="stackFrameInfoDeserialiser">The <see cref="IStackFrameInfoDeserialiser"/> to use.</param>
   public StackTraceInfoDeserialiser0(IStackFrameInfoDeserialiser stackFrameInfoDeserialiser)
   {
      _stackFrameInfoDeserialiser = stackFrameInfoDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IStackTraceInfo Deserialise(BinaryReader reader)
   {
      int threadId = reader.ReadInt32();
      int frameCount = reader.Read7BitEncodedInt();

      IStackFrameInfo[] frames = new IStackFrameInfo[frameCount];
      for (int i = 0; i < frameCount; i++)
      {
         IStackFrameInfo frame = _stackFrameInfoDeserialiser.Deserialise(reader);
         frames[i] = frame;
      }

      return StackTraceInfoFactory.Version0(threadId, frames);
   }
   #endregion
}
