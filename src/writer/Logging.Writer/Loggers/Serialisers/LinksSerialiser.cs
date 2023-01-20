namespace TNO.Logging.Writer.Loggers.Serialisers;
internal class LinksSerialiser : ISerialiser<Links>
{
   #region Methods
   public static void Serialise(BinaryWriter writer, Links data)
   {
      writer.Write(data.ContextId);
      writer.Write(data.FileRef);
      writer.Write(data.Line);
      writer.Write(data.Ids.Length);

      foreach (ulong id in data.Ids)
         writer.Write(id);
   }
   #endregion
}
