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
            new MessageComponentSerialiser(),
            new TagComponentSerialiser());

      writer = new EntrySerialiser(componentSerialiser);

      ComponentDeserialiserDispatcher componentDeserialiser =
         new ComponentDeserialiserDispatcher(
            new MessageComponentDeserialiserLatest(),
            new TagComponentDeserialiserLatest());

      reader = new EntryDeserialiserLatest(componentDeserialiser);
   }

   protected override IEntry CreateData()
   {
      MessageComponent messageComponent = new MessageComponent("some message");
      TagComponent tagComponent = new TagComponent(7);

      ulong id = 1;
      ulong contextId = 2;
      ulong scope = 3;
      TimeSpan timestamp = new TimeSpan(4);
      ulong fileId = 5;
      uint line = 6;

      Importance Importance = Severity.Negligible | Purpose.Telemetry;
      Dictionary<ComponentKind, IComponent> components = new Dictionary<ComponentKind, IComponent>
      {
         { ComponentKind.Message, messageComponent },
         { ComponentKind.Tag, tagComponent }
      };

      Entry entry = new Entry(id, contextId, scope, Importance, timestamp, fileId, line, components);

      return entry;
   }
   protected override void Verify(IEntry expected, IEntry result)
   {
      // Todo(Nightowl): These should include an appropriate message;
      Assert.AreEqual(expected.Id, result.Id);
      Assert.AreEqual(expected.ContextId, result.ContextId);
      Assert.AreEqual(expected.Scope, result.Scope);
      Assert.AreEqual(expected.Importance, result.Importance);
      Assert.AreEqual(expected.Timestamp, result.Timestamp);
      Assert.AreEqual(expected.FileId, result.FileId);
      Assert.AreEqual(expected.LineInFile, result.LineInFile);
      Assert.AreEqual(expected.Components.Count, result.Components.Count);

      // Message component
      {
         IMessageComponent expectedComponent = (IMessageComponent)expected.Components[ComponentKind.Message];
         IMessageComponent resultComponent = AssertGetComponent<IMessageComponent>(result, ComponentKind.Message);

         Assert.AreEqual(expectedComponent.Message, resultComponent.Message);
      }

      // Tag component
      {
         ITagComponent expectedComponent = (ITagComponent)expected.Components[ComponentKind.Tag];
         ITagComponent resultComponent = AssertGetComponent<ITagComponent>(result, ComponentKind.Tag);

         Assert.AreEqual(expectedComponent.TagId, resultComponent.TagId);
      }
   }
   #endregion

   #region Helpers
   private T AssertGetComponent<T>(IEntry entry, ComponentKind kind) where T : class, IComponent
   {
      Assert.IsTrue(entry.Components.TryGetValue(kind, out IComponent? component));

      T? typedComponent = component as T;

      Assert.IsNotNull(typedComponent);

      return typedComponent;

   }
   #endregion
}