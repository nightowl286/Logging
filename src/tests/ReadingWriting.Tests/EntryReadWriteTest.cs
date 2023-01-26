using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Entries.Components;
using TNO.Logging.Writing.Entries;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests;

[TestClass]
public class EntryReadWriteTest : ReadWriteTestBase<EntrySerialiser, EntryDeserialiserLatest, IEntry>
{
   #region Methods
   protected override void Setup(out EntrySerialiser writer, out EntryDeserialiserLatest reader)
   {
      ComponentSerialiserDispatcher componentSerialiser =
         new ComponentSerialiserDispatcher(
            new MessageComponentSerialiser());

      writer = new EntrySerialiser(componentSerialiser);


      ComponentDeserialiserDispatcher componentDeserialiser =
         new ComponentDeserialiserDispatcher(
            new MessageComponentDeserialiserLatest());

      reader = new EntryDeserialiserLatest(componentDeserialiser);
   }

   protected override IEntry CreateData()
   {
      MessageComponent messageComponent = new MessageComponent("some message");

      ulong id = 5;
      Dictionary<ComponentKind, IComponent> components = new Dictionary<ComponentKind, IComponent>
      {
         { ComponentKind.Message, messageComponent }
      };

      Entry entry = new Entry(id, components);

      return entry;
   }
   protected override void Verify(IEntry expected, IEntry result)
   {
      Assert.AreEqual(expected.Id, result.Id);
      Assert.AreEqual(expected.Components.Count, result.Components.Count);

      // Message component
      {
         IMessageComponent expectedComponent = (IMessageComponent)expected.Components[ComponentKind.Message];

         Assert.IsTrue(result.Components.TryGetValue(ComponentKind.Message, out IComponent? resultComponent));
         IMessageComponent? typedResultComponent = resultComponent as IMessageComponent;

         Assert.IsNotNull(typedResultComponent);

         Assert.AreEqual(expectedComponent.Message, typedResultComponent.Message);
      }
   }
   #endregion
}
