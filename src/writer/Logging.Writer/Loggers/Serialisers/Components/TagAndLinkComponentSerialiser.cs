using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Loggers.Serialisers.Components;
internal class TagAndLinkComponentSerialiser : IComponentSerialiser<ITagComponent>, IComponentSerialiser<ILinkComponent>
{
   #region Methods
   public static void Serialise(BinaryWriter writer, ILinkComponent data)
   {
      writer.Write(data.IdOfLinkedEntry);
   }
   public static void Serialise(BinaryWriter writer, ITagComponent data)
   {
      writer.Write(data.TagId);
   }
   #endregion
}
