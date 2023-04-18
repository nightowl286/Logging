using System.Diagnostics;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// Represents a <see cref="ISerialiser{T}"/> dispatcher that
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
   private readonly ITypeComponentSerialiser _typeSerialiser;
   private readonly IStackTraceComponentSerialiser _stackTraceSerialiser;
   private readonly IExceptionComponentSerialiser _exceptionSerialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ComponentSerialiserDispatcher"/>.</summary>
   /// <param name="messageSerialiser">The <see cref="IMessageComponentSerialiser"/> to use.</param>
   /// <param name="tagSerialiser">The <see cref="ITagComponentSerialiser"/> to use.</param>
   /// <param name="threadSerialiser">The <see cref="IThreadComponentSerialiser"/> to use.</param>
   /// <param name="entryLinkSerialiser">The <see cref="IEntryLinkComponentSerialiser"/> to use.</param>
   /// <param name="tableSerialiser">The <see cref="ITableComponentSerialiser"/> to use.</param>
   /// <param name="assemblySerialiser">The <see cref="IAssemblyComponentSerialiser"/> to use.</param>
   /// <param name="typeSerialiser">The <see cref="ITypeComponentSerialiser"/> to use.</param>
   /// <param name="stackTraceSerialiser">The <see cref="IStackTraceComponentSerialiser"/> to use.</param>
   /// <param name="exceptionSerialiser">The <see cref="IExceptionComponentSerialiser"/> to use.</param>
   public ComponentSerialiserDispatcher(
      IMessageComponentSerialiser messageSerialiser,
      ITagComponentSerialiser tagSerialiser,
      IThreadComponentSerialiser threadSerialiser,
      IEntryLinkComponentSerialiser entryLinkSerialiser,
      ITableComponentSerialiser tableSerialiser,
      IAssemblyComponentSerialiser assemblySerialiser,
      ITypeComponentSerialiser typeSerialiser,
      IStackTraceComponentSerialiser stackTraceSerialiser,
      IExceptionComponentSerialiser exceptionSerialiser)
   {
      _messageSerialiser = messageSerialiser;
      _tagSerialiser = tagSerialiser;
      _threadSerialiser = threadSerialiser;
      _entryLinkSerialiser = entryLinkSerialiser;
      _tableSerialiser = tableSerialiser;
      _assemblySerialiser = assemblySerialiser;
      _stackTraceSerialiser = stackTraceSerialiser;
      _typeSerialiser = typeSerialiser;
      _exceptionSerialiser = exceptionSerialiser;
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
      else if (data is IStackTraceComponent stackTrace)
      {
         Debug.Assert(stackTrace.Kind is ComponentKind.StackTrace);
         _stackTraceSerialiser.Serialise(writer, stackTrace);
      }
      else if (data is ITypeComponent type)
      {
         Debug.Assert(type.Kind is ComponentKind.Type);
         _typeSerialiser.Serialise(writer, type);
      }
      else if (data is IExceptionComponent exception)
      {
         Debug.Assert(exception.Kind is ComponentKind.Exception);
         _exceptionSerialiser.Serialise(writer, exception);
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
         IStackTraceComponent stackTrace => _stackTraceSerialiser.Count(stackTrace),
         ITypeComponent type => _typeSerialiser.Count(type),
         IExceptionComponent exception => _exceptionSerialiser.Count(exception),

         _ => throw new ArgumentException($"Unknown component type ({data.GetType()}). Kind: {data.Kind}.", nameof(data))
      };
   }
   #endregion
}