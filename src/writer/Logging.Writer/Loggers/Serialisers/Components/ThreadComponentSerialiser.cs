using TNO.Common.Abstractions.Components.Kinds;

namespace TNO.Logging.Writer.Loggers.Serialisers.Components;
internal class ThreadComponentSerialiser : IComponentSerialiser<IThreadComponent>
{
   public static void Serialise(BinaryWriter writer, IThreadComponent data)
   {
      writer.Write(data.Name is not null);
      if (data.Name is not null)
         writer.Write(data.Name);

      writer.Write(data.Id);
      writer.Write((int)data.ApartmentState);
      writer.Write((int)data.Priority);
      writer.Write(data.IsBackgroundThread);
      writer.Write(data.IsThreadPoolThread);
      writer.Write(data.IsAlive);
      writer.Write((int)data.State);
   }
}
