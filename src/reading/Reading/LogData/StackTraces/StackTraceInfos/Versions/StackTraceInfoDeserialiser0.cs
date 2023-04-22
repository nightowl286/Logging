using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.LogData.Methods.StackTraceInfos;

namespace TNO.Logging.Reading.LogData.StackTraces.StackTraceInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IStackTraceInfo"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.StackTraceInfo)]
public sealed class StackTraceInfoDeserialiser0 : IDeserialiser<IStackTraceInfo>
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackTraceInfoDeserialiser0"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>

   public StackTraceInfoDeserialiser0(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
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
         _deserialiser.Deserialise(reader, out IStackFrameInfo frame);
         frames[i] = frame;
      }

      return StackTraceInfoFactory.Version0(threadId, frames);
   }
   #endregion
}
