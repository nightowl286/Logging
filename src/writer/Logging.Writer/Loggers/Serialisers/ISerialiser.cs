namespace TNO.Logging.Writer.Loggers.Serialisers;
internal interface ISerialiser<T> where T : notnull
{
   #region Functions
   public static abstract void Serialise(BinaryWriter writer, T data);
   #endregion
}
