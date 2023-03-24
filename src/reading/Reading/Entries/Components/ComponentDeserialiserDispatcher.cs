using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Assembly;
using TNO.Logging.Reading.Abstractions.Entries.Components.EntryLink;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;
using TNO.Logging.Reading.Abstractions.Entries.Components.SimpleStackTrace;
using TNO.Logging.Reading.Abstractions.Entries.Components.Table;
using TNO.Logging.Reading.Abstractions.Entries.Components.Tag;
using TNO.Logging.Reading.Abstractions.Entries.Components.Thread;

namespace TNO.Logging.Reading.Entries.Components;

/// <summary>
/// Denotes a <see cref="IDeserialiser{T}"/> dispatcher that 
/// will deserialise a <see cref="IComponent"/> based on a 
/// given <see cref="ComponentKind"/>.
/// </summary>
public class ComponentDeserialiserDispatcher : IComponentDeserialiserDispatcher
{
   #region Fields
   private readonly IMessageComponentDeserialiser _messageDeserialiser;
   private readonly ITagComponentDeserialiser _tagDeserialiser;
   private readonly IThreadComponentDeserialiser _threadDeserialiser;
   private readonly IEntryLinkComponentDeserialiser _entryLinkDeserialiser;
   private readonly ITableComponentDeserialiser _tableDeserialiser;
   private readonly IAssemblyComponentDeserialiser _assemblyDeserialiser;
   private readonly ISimpleStackTraceComponentDeserialiser _simpleStackTraceDeserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ComponentDeserialiserDispatcher"/>.</summary>
   /// <param name="messageDeserialiser">The message deserialiser to use.</param>
   /// <param name="tagDeserialiser">The tag deserialiser to use.</param>
   /// <param name="threadDeserialiser">The thread deserialiser to use.</param>
   /// <param name="entryLinkDeserialiser">The entry link deserialiser to use.</param>
   /// <param name="tableDeserialiser">The table deserialiser to use.</param>
   /// <param name="assemblyDeserialiser">The assembly deserialiser to use.</param>
   /// <param name="simpleStackTraceDeserialiser">The simple stack trace deserialiser to use.</param>
   public ComponentDeserialiserDispatcher(
      IMessageComponentDeserialiser messageDeserialiser,
      ITagComponentDeserialiser tagDeserialiser,
      IThreadComponentDeserialiser threadDeserialiser,
      IEntryLinkComponentDeserialiser entryLinkDeserialiser,
      ITableComponentDeserialiser tableDeserialiser,
      IAssemblyComponentDeserialiser assemblyDeserialiser,
      ISimpleStackTraceComponentDeserialiser simpleStackTraceDeserialiser)
   {
      _messageDeserialiser = messageDeserialiser;
      _tagDeserialiser = tagDeserialiser;
      _threadDeserialiser = threadDeserialiser;
      _entryLinkDeserialiser = entryLinkDeserialiser;
      _tableDeserialiser = tableDeserialiser;
      _assemblyDeserialiser = assemblyDeserialiser;
      _simpleStackTraceDeserialiser = simpleStackTraceDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IComponent Deserialise(BinaryReader reader, ComponentKind componentKind)
   {
      return componentKind switch
      {
         ComponentKind.Message => _messageDeserialiser.Deserialise(reader),
         ComponentKind.Tag => _tagDeserialiser.Deserialise(reader),
         ComponentKind.Thread => _threadDeserialiser.Deserialise(reader),
         ComponentKind.EntryLink => _entryLinkDeserialiser.Deserialise(reader),
         ComponentKind.Table => _tableDeserialiser.Deserialise(reader),
         ComponentKind.Assembly => _assemblyDeserialiser.Deserialise(reader),
         ComponentKind.SimpleStackTrace => _simpleStackTraceDeserialiser.Deserialise(reader),

         _ => throw new ArgumentException($"Unknown component kind ({componentKind}).")
      };
   }
   #endregion
}