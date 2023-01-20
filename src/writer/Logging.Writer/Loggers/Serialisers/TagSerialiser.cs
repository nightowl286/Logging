namespace TNO.Logging.Writer.Loggers.Serialisers;
internal class TagSerialiser : ISerialiser<Tag>
{
   #region Methods
   public static void Serialise(BinaryWriter writer, Tag data)
   {
      writer.Write(data.Id);
      writer.Write(data.Name);
   }
   #endregion
}
