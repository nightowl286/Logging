using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Entries.Components;

/// <summary>
/// Represents a <see cref="IDeserialiser{T}"/> dispatcher that 
/// will deserialise a <see cref="IComponent"/> based on a 
/// given <see cref="ComponentKind"/>.
/// </summary>
public class ComponentDeserialiserDispatcher
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ComponentDeserialiserDispatcher"/>.</summary>
   public ComponentDeserialiserDispatcher(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <summary>Deserialises an <see cref="IComponent"/> based on the given <paramref name="componentKind"/>.</summary>
   /// <param name="reader">The reader to use.</param>
   /// <param name="componentKind">The kind of the <see cref="IComponent"/> to deserialise.</param>
   /// <returns>The deserialised <see cref="IComponent"/>.</returns>
   /// <exception cref="ArgumentException">Throw if an unknown <paramref name="componentKind"/> is given.</exception>
   public IComponent Deserialise(BinaryReader reader, ComponentKind componentKind)
   {
      return componentKind switch
      {
         ComponentKind.Message => _deserialiser.Deserialise<IMessageComponent>(reader),
         ComponentKind.Tag => _deserialiser.Deserialise<ITagComponent>(reader),
         ComponentKind.Thread => _deserialiser.Deserialise<IThreadComponent>(reader),
         ComponentKind.EntryLink => _deserialiser.Deserialise<IEntryLinkComponent>(reader),
         ComponentKind.Table => _deserialiser.Deserialise<ITableComponent>(reader),
         ComponentKind.Assembly => _deserialiser.Deserialise<IAssemblyComponent>(reader),
         ComponentKind.StackTrace => _deserialiser.Deserialise<IStackTraceComponent>(reader),
         ComponentKind.Type => _deserialiser.Deserialise<ITypeComponent>(reader),
         ComponentKind.Exception => _deserialiser.Deserialise<IExceptionComponent>(reader),

         _ => throw new ArgumentException($"Unknown component kind ({componentKind}).")
      };
   }
   #endregion
}