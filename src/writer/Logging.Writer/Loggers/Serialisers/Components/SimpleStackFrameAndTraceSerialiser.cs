using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Loggers.Serialisers.Components;
internal class SimpleStackFrameAndTraceSerialiser : IComponentSerialiser<ISimpleStackFrameComponent>, IComponentSerialiser<ISimpleStackTraceComponent>
{
   #region Methods
   public static void Serialise(BinaryWriter writer, ISimpleStackFrameComponent data)
   {
      writer.Write(data.StackFrame);
   }
   public static void Serialise(BinaryWriter writer, ISimpleStackTraceComponent data)
   {
      writer.Write(data.StackTrace);
   }
   #endregion
}
