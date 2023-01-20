namespace TNO.Logging.Writer.Loggers.Serialisers;
internal class FileReferenceSerialiser : ISerialiser<FileReference>
{
   public static void Serialise(BinaryWriter writer, FileReference data)
   {
      writer.Write(data.Id);
      writer.Write(data.FilePath);
   }
}
