using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Entries;

/// <summary>
/// A serialiser for <see cref="IEntry"/>.
/// </summary>
public class EntrySerialiser : IEntrySerialiser
{
   #region Fields
   private readonly IComponentSerialiserDispatcher _componentSerialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="EntrySerialiser"/>.</summary>
   /// <param name="componentSerialiser">The component serialiser to use.</param>
   public EntrySerialiser(IComponentSerialiserDispatcher componentSerialiser)
   {
      _componentSerialiser = componentSerialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IEntry data)
   {
      ulong id = data.Id;
      ComponentKind kinds = data.Components.Keys.CombineFlags();
      ushort rawKinds = (ushort)kinds;

      writer.Write(id);
      writer.Write(rawKinds);
      foreach (ComponentKind possibleKind in EnumExtensions.GetValuesAscending<ComponentKind>())
      {
         if (data.Components.TryGetValue(possibleKind, out IComponent? component))
            _componentSerialiser.Serialise(writer, component);
      }
   }
   #endregion
}
