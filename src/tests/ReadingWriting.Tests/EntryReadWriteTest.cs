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
      TimeSpan timestamp = new TimeSpan(5);
      ulong fileId = 5;
      uint line = 5;

      Importance Importance = Severity.Negligible | Purpose.Telemetry;
      Dictionary<ComponentKind, IComponent> components = new Dictionary<ComponentKind, IComponent>
      {
         { ComponentKind.Message, messageComponent }
      };

      Entry entry = new Entry(id, Importance, timestamp, fileId, line, components);

      return entry;
   }
   protected override void Verify(IEntry expected, IEntry result)
   {
      // Todo(Nightowl): These should include an appropriate message;
      Assert.AreEqual(expected.Id, result.Id);
      Assert.AreEqual(expected.Importance, result.Importance);
      Assert.AreEqual(expected.Timestamp, result.Timestamp);
      Assert.AreEqual(expected.FileId, result.FileId);
      Assert.AreEqual(expected.LineInFile, result.LineInFile);
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