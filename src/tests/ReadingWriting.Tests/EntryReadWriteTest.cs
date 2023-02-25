using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Entries.Components;
using TNO.Logging.Writing.Entries;
using TNO.Logging.Writing.Entries.Components;
using TNO.Tests.Common;

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
            new TagComponentSerialiser(),
            new ThreadComponentSerialiser());

      writer = new EntrySerialiser(componentSerialiser);

      ComponentDeserialiserDispatcher componentDeserialiser =
         new ComponentDeserialiserDispatcher(
            new MessageComponentDeserialiserLatest(),
            new TagComponentDeserialiserLatest(),
            new ThreadComponentDeserialiserLatest());

      reader = new EntryDeserialiserLatest(componentDeserialiser);
   }

   protected override IEntry CreateData()
   {
      MessageComponent messageComponent = new MessageComponent("some message");
      TagComponent tagComponent = new TagComponent(7);
      ThreadComponent threadComponent = ThreadComponent.FromThread(Thread.CurrentThread);

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
         { ComponentKind.Tag, tagComponent },
         { ComponentKind.Thread, threadComponent }
      };

      Entry entry = new Entry(id, contextId, scope, Importance, timestamp, fileId, line, components);

      return entry;
   }
   protected override void Verify(IEntry expected, IEntry result)
   {
      void GetComponents<T>(ComponentKind kind, out T expectedComponent, out T resultComponent)
         where T : class, IComponent
      {
         Assert.That.IsInconclusiveIfNot(expected.Components.ContainsKey(kind),
            $"The expected data did not contain the {kind} component, is the test wrong or is the data?");

         expectedComponent = (T)expected.Components[kind];
         resultComponent = AssertGetComponent<T>(result, kind);
      }


      Assert.That.AreEqual(expected.Id, result.Id);
      Assert.That.AreEqual(expected.ContextId, result.ContextId);
      Assert.That.AreEqual(expected.Scope, result.Scope);
      Assert.That.AreEqual(expected.Importance, result.Importance);
      Assert.That.AreEqual(expected.Timestamp, result.Timestamp);
      Assert.That.AreEqual(expected.FileId, result.FileId);
      Assert.That.AreEqual(expected.LineInFile, result.LineInFile);
      Assert.That.AreEqual(expected.Components.Count, result.Components.Count);

      // Message component
      {
         GetComponents(
            ComponentKind.Message,
            out IMessageComponent expectedComponent,
            out IMessageComponent resultComponent);

         Assert.That.AreEqual(expectedComponent.Message, resultComponent.Message);
      }

      // Tag component
      {
         GetComponents(
            ComponentKind.Tag,
            out ITagComponent expectedComponent,
            out ITagComponent resultComponent);

         Assert.That.AreEqual(expectedComponent.TagId, resultComponent.TagId);
      }

      // Thread component
      {
         GetComponents(
            ComponentKind.Thread,
            out IThreadComponent expectedComponent,
            out IThreadComponent resultComponent);

         Assert.That.AreEqual(expectedComponent.ManagedId, resultComponent.ManagedId);
         Assert.That.AreEqual(expectedComponent.Name, resultComponent.Name);
         Assert.That.AreEqual(expectedComponent.IsThreadPoolThread, resultComponent.IsThreadPoolThread);
         Assert.That.AreEqual(expectedComponent.State, resultComponent.State);
         Assert.That.AreEqual(expectedComponent.Priority, resultComponent.Priority);
         Assert.That.AreEqual(expectedComponent.ApartmentState, resultComponent.ApartmentState);
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