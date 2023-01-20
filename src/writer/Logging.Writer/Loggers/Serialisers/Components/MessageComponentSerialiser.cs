using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Loggers.Serialisers.Components;
internal class MessageComponentSerialiser : IComponentSerialiser<IMessageComponent>
{
   public static void Serialise(BinaryWriter writer, IMessageComponent data)
   {
      writer.Write(data.Message);
   }
}
