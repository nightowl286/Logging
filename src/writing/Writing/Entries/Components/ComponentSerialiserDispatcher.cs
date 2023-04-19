using System.Diagnostics;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// Represents a <see cref="ISerialiser{T}"/> dispatcher that
/// will serialise a given <see cref="IComponent"/> based
/// on its <see cref="IComponent.Kind"/>.
/// </summary>
public class ComponentSerialiserDispatcher : ISerialiser<IComponent>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ComponentSerialiserDispatcher"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public ComponentSerialiserDispatcher(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }

   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IComponent data)
   {
      if (data is IMessageComponent message)
      {
         Debug.Assert(message.Kind is ComponentKind.Message);
         _serialiser.Serialise(writer, message);
      }
      else if (data is ITagComponent tag)
      {
         Debug.Assert(tag.Kind is ComponentKind.Tag);
         _serialiser.Serialise(writer, tag);
      }
      else if (data is IThreadComponent thread)
      {
         Debug.Assert(thread.Kind is ComponentKind.Thread);
         _serialiser.Serialise(writer, thread);
      }
      else if (data is IEntryLinkComponent entryLink)
      {
         Debug.Assert(entryLink.Kind is ComponentKind.EntryLink);
         _serialiser.Serialise(writer, entryLink);
      }
      else if (data is ITableComponent table)
      {
         Debug.Assert(table.Kind is ComponentKind.Table);
         _serialiser.Serialise(writer, table);
      }
      else if (data is IAssemblyComponent assembly)
      {
         Debug.Assert(assembly.Kind is ComponentKind.Assembly);
         _serialiser.Serialise(writer, assembly);
      }
      else if (data is IStackTraceComponent stackTrace)
      {
         Debug.Assert(stackTrace.Kind is ComponentKind.StackTrace);
         _serialiser.Serialise(writer, stackTrace);
      }
      else if (data is ITypeComponent type)
      {
         Debug.Assert(type.Kind is ComponentKind.Type);
         _serialiser.Serialise(writer, type);
      }
      else if (data is IExceptionComponent exception)
      {
         Debug.Assert(exception.Kind is ComponentKind.Exception);
         _serialiser.Serialise(writer, exception);
      }
      else
         throw new ArgumentException($"Unknown component type ({data.GetType()}). Kind: {data.Kind}.", nameof(data));
   }

   /// <inheritdoc/>
   public ulong Count(IComponent data)
   {
      return data switch
      {
         IMessageComponent message => _serialiser.Count(message),
         ITagComponent tag => _serialiser.Count(tag),
         IThreadComponent thread => _serialiser.Count(thread),
         IEntryLinkComponent entryLink => _serialiser.Count(entryLink),
         ITableComponent table => _serialiser.Count(table),
         IAssemblyComponent assembly => _serialiser.Count(assembly),
         IStackTraceComponent stackTrace => _serialiser.Count(stackTrace),
         ITypeComponent type => _serialiser.Count(type),
         IExceptionComponent exception => _serialiser.Count(exception),

         _ => throw new ArgumentException($"Unknown component type ({data.GetType()}). Kind: {data.Kind}.", nameof(data))
      };
   }
   #endregion
}