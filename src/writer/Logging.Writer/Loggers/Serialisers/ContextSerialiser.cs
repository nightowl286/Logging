namespace TNO.Logging.Writer.Loggers.Serialisers;
internal class ContextSerialiser : ISerialiser<Context>
{
   public static void Serialise(BinaryWriter writer, Context data)
   {
      writer.Write(data.Id);
      writer.Write(data.Parent);
      writer.Write(data.Name);
   }
}
