using System.Diagnostics;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// Denotes a <see cref="ISerialiser{T}"/> dispatcher that
/// will serialise a given <see cref="IComponent"/> based
/// on its <see cref="IComponent.Kind"/>.
/// </summary>
public class ComponentSerialiserDispatcher : IComponentSerialiserDispatcher
{
   #region Fields
   private readonly IMessageComponentSerialiser _messageSerialiser;
   private readonly ITagComponentSerialiser _tagSerialiser;
   private readonly IThreadComponentSerialiser _threadSerialiser;
   private readonly IEntryLinkComponentSerialiser _entryLinkSerialiser;
   private readonly ITableComponentSerialiser _tableSerialiser;
   private readonly IAssemblyComponentSerialiser _assemblySerialiser;
   private readonly ISimpleStackTraceComponentSerialiser _simpleStackTraceSerialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ComponentSerialiserDispatcher"/>.</summary>
   /// <param name="messageSerialiser">The message serialiser to use.</param>
   /// <param name="tagSerialiser">The tag serialiser to use.</param>
   /// <param name="threadSerialiser">The thread serialiser to use.</param>
   /// <param name="entryLinkSerialiser">The entry link serialiser to use.</param>
   /// <param name="tableSerialiser">The table serialiser to use.</param>
   /// <param name="assemblySerialiser">The assembly serialiser to use.</param>
   /// <param name="simpleStackTraceSerialiser">The simple stack trace serialiser to use.</param>
   public ComponentSerialiserDispatcher(
      IMessageComponentSerialiser messageSerialiser,
      ITagComponentSerialiser tagSerialiser,
      IThreadComponentSerialiser threadSerialiser,
      IEntryLinkComponentSerialiser entryLinkSerialiser,
      ITableComponentSerialiser tableSerialiser,
      IAssemblyComponentSerialiser assemblySerialiser,
      ISimpleStackTraceComponentSerialiser simpleStackTraceSerialiser)
   {
      _messageSerialiser = messageSerialiser;
      _tagSerialiser = tagSerialiser;
      _threadSerialiser = threadSerialiser;
      _entryLinkSerialiser = entryLinkSerialiser;
      _tableSerialiser = tableSerialiser;
      _assemblySerialiser = assemblySerialiser;
      _simpleStackTraceSerialiser = simpleStackTraceSerialiser;
   }

   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IComponent data)
   {
      if (data is IMessageComponent message)
      {
         Debug.Assert(message.Kind is ComponentKind.Message);
         _messageSerialiser.Serialise(writer, message);
      }
      else if (data is ITagComponent tag)
      {
         Debug.Assert(tag.Kind is ComponentKind.Tag);
         _tagSerialiser.Serialise(writer, tag);
      }
      else if (data is IThreadComponent thread)
      {
         Debug.Assert(thread.Kind is ComponentKind.Thread);
         _threadSerialiser.Serialise(writer, thread);
      }
      else if (data is IEntryLinkComponent entryLink)
      {
         Debug.Assert(entryLink.Kind is ComponentKind.EntryLink);
         _entryLinkSerialiser.Serialise(writer, entryLink);
      }
      else if (data is ITableComponent table)
      {
         Debug.Assert(table.Kind is ComponentKind.Table);
         _tableSerialiser.Serialise(writer, table);
      }
      else if (data is IAssemblyComponent assembly)
      {
         Debug.Assert(assembly.Kind is ComponentKind.Assembly);
         _assemblySerialiser.Serialise(writer, assembly);
      }
      else if (data is ISimpleStackTraceComponent simpleStackTrace)
      {
         Debug.Assert(simpleStackTrace.Kind is ComponentKind.SimpleStackTrace);
         _simpleStackTraceSerialiser.Serialise(writer, simpleStackTrace);
      }
      else
         throw new ArgumentException($"Unknown component type ({data.GetType()}). Kind: {data.Kind}.", nameof(data));
   }

   /// <inheritdoc/>
   public ulong Count(IComponent data)
   {
      return data switch
      {
         IMessageComponent message => _messageSerialiser.Count(message),
         ITagComponent tag => _tagSerialiser.Count(tag),
         IThreadComponent thread => _threadSerialiser.Count(thread),
         IEntryLinkComponent entryLink => _entryLinkSerialiser.Count(entryLink),
         ITableComponent table => _tableSerialiser.Count(table),
         IAssemblyComponent assembly => _assemblySerialiser.Count(assembly),
         ISimpleStackTraceComponent simpleStackTrace => _simpleStackTraceSerialiser.Count(simpleStackTrace),

         _ => throw new ArgumentException($"Unknown component type ({data.GetType()}). Kind: {data.Kind}.", nameof(data))
      };
   }
   #endregion
}