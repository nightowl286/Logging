using TNO.Common.Abstractions.Components;
using TNO.Common.Abstractions.Components.Kinds;
using TNO.Common.Extensions;
using TNO.Logging.Writer.Abstractions;
using TNO.Logging.Writer.Loggers.Serialisers.Components;

namespace TNO.Logging.Writer.Loggers.Serialisers;
internal class EntrySerialiser : ISerialiser<ILogEntry>
{
   public static void Serialise(BinaryWriter writer, ILogEntry data)
   {
      writer.Write(data.Id);
      writer.Write(data.ContextId);
      writer.Write(data.FileId);
      writer.Write(data.Line);

      ComponentKind componentFlags = data.Components.Keys.CombineFlags();
      writer.Write((ushort)componentFlags);

      foreach (ComponentKind kind in EnumExtensions.GetValuesAscending<ComponentKind>())
      {
         if (data.Components.TryGetValue(kind, out IEntryComponent? component) == false)
            continue;

         SerialiseComponent(writer, component);
      }
   }

   private static void SerialiseComponent(BinaryWriter writer, IEntryComponent component)
   {
      if (component is IMessageComponent message) MessageComponentSerialiser.Serialise(writer, message);
      else if (component is IThreadComponent thread) ThreadComponentSerialiser.Serialise(writer, thread);
      else if (component is ILinkComponent link) TagAndLinkComponentSerialiser.Serialise(writer, link);
      else if (component is ITagComponent tag) TagAndLinkComponentSerialiser.Serialise(writer, tag);
      else
         throw new ArgumentException($"Unknown component kind ({component.Kind}) ({component.GetType()}).", nameof(component));
   }
}
