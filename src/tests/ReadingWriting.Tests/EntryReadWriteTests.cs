using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Entries;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Common.LogData.StackTraces;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Writing.Entries;

namespace TNO.ReadingWriting.Tests;

[TestClass]
public class EntryReadWriteTests : BinaryReadWriteTestsBase<EntrySerialiser, EntryDeserialiserLatest, IEntry>
{
   #region Methods
   protected override void Setup(out EntrySerialiser writer, out EntryDeserialiserLatest reader)
   {
      writer = new EntrySerialiser(GeneralSerialiser.Instance);

      reader = new EntryDeserialiserLatest(GeneralDeserialiser.Instance);
   }

   protected override IEnumerable<IEntry> CreateData()
   {
      MessageComponent messageComponent = new MessageComponent("some message");
      TagComponent tagComponent = new TagComponent(7);
      ThreadComponent threadComponent = ThreadComponent.FromThread(Thread.CurrentThread);

      Dictionary<uint, object?> table = new Dictionary<uint, object?>() { { 1, 5 } };
      TableInfo tableInfo = new TableInfo(table);
      TableComponent tableComponent = new TableComponent(tableInfo);
      AssemblyComponent assemblyComponent = new AssemblyComponent(0);

      IStackTraceInfo stackTraceInfo;
      {
         // Arrange
         MethodInfo mainMethod = new MethodInfo(
           1,
           Array.Empty<IParameterInfo>(),
           "main method",
           1,
           Array.Empty<ulong>());

         StackFrameInfo stackFrameInfo = new StackFrameInfo(
            1,
            2,
            3,
            mainMethod,
            null);

         stackTraceInfo = new StackTraceInfo(
            6,
            new[] { stackFrameInfo });
      }
      StackTraceComponent stackTraceComponent = new StackTraceComponent(stackTraceInfo);

      ulong id = 1;
      ulong contextId = 2;
      ulong scope = 3;
      TimeSpan timestamp = new TimeSpan(4);
      ulong fileId = 5;
      uint line = 6;

      ImportanceCombination Importance = Severity.Negligible | Purpose.Telemetry;
      Dictionary<ComponentKind, IComponent> components = new Dictionary<ComponentKind, IComponent>
      {
         { ComponentKind.Message, messageComponent },
         { ComponentKind.Tag, tagComponent },
         { ComponentKind.Thread, threadComponent },
         { ComponentKind.Table, tableComponent },
         { ComponentKind.Assembly, assemblyComponent },
         { ComponentKind.StackTrace, stackTraceComponent }
      };

      Entry entry = new Entry(id, contextId, scope, Importance, timestamp, fileId, line, components);

      yield return entry;
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
         GetComponents(ComponentKind.Message,
            out IMessageComponent expectedComponent,
            out IMessageComponent resultComponent);

         Assert.That.AreEqual(expectedComponent.Message, resultComponent.Message);
      }

      // Tag component
      {
         GetComponents(ComponentKind.Tag,
            out ITagComponent expectedComponent,
            out ITagComponent resultComponent);

         Assert.That.AreEqual(expectedComponent.TagId, resultComponent.TagId);
      }

      // Thread component
      {
         GetComponents(ComponentKind.Thread,
            out IThreadComponent expectedComponent,
            out IThreadComponent resultComponent);

         Assert.That.AreEqual(expectedComponent.ManagedId, resultComponent.ManagedId);
         Assert.That.AreEqual(expectedComponent.Name, resultComponent.Name);
         Assert.That.AreEqual(expectedComponent.IsThreadPoolThread, resultComponent.IsThreadPoolThread);
         Assert.That.AreEqual(expectedComponent.State, resultComponent.State);
         Assert.That.AreEqual(expectedComponent.Priority, resultComponent.Priority);
         Assert.That.AreEqual(expectedComponent.ApartmentState, resultComponent.ApartmentState);
      }

      // Table component
      {
         GetComponents(ComponentKind.Table,
            out ITableComponent expectedComponent,
            out ITableComponent resultComponent);

         ITableInfo expectedTable = expectedComponent.Table;
         ITableInfo resultTable = resultComponent.Table;

         Assert.That.AreEqual(expectedTable.Table.Count, resultTable.Table.Count);
         foreach (KeyValuePair<uint, object?> expectedPair in expectedTable.Table)
         {
            object? expectedTableValue = expectedPair.Value;
            object? resultTableValue = resultTable.Table[expectedPair.Key];

            Assert.That.AreEqual(expectedTableValue, resultTableValue);
         }
      }

      // Assembly component
      {
         GetComponents(ComponentKind.Assembly,
            out IAssemblyComponent expectedComponent,
            out IAssemblyComponent resultComponent);

         Assert.That.AreEqual(expectedComponent.AssemblyId, resultComponent.AssemblyId);
      }

      // Stack trace component
      {
         GetComponents(ComponentKind.StackTrace,
            out IStackTraceComponent expectedComponent,
            out IStackTraceComponent resultComponent);

         IStackTraceInfo expectedInfo = expectedComponent.StackTrace;
         IStackTraceInfo resultInfo = resultComponent.StackTrace;

         Verify(expectedInfo, resultInfo);
      }
   }
   #endregion

   #region Helpers
   private static void Verify(IStackTraceInfo expected, IStackTraceInfo result)
   {
      Assert.That.AreEqual(expected.ThreadId, result.ThreadId);
      Assert.That.AreEqual(expected.Frames.Count, result.Frames.Count);

      for (int i = 0; i < expected.Frames.Count; i++)
         Verify(expected.Frames[i], result.Frames[i]);
   }

   private static void Verify(IStackFrameInfo expected, IStackFrameInfo result)
   {
      Assert.That.AreEqual(expected.FileId, result.FileId);
      Assert.That.AreEqual(expected.LineInFile, result.LineInFile);
      Assert.That.AreEqual(expected.ColumnInLine, result.ColumnInLine);
      Verify(expected.MainMethod, result.MainMethod);
      Verify(expected.SecondaryMethod, result.SecondaryMethod);
   }

   private static void Verify(IMethodBaseInfo? expected, IMethodBaseInfo? result)
   {
      bool isExpectedNull = expected is null;
      bool isResultNull = result is null;

      Assert.That.AreEqual(isExpectedNull, isResultNull);

      if (expected is not null && result is not null)
      {
         Assert.That.AreEqual(expected.Name, result.Name);
         Assert.That.AreEqual(expected.DeclaringTypeId, result.DeclaringTypeId);
         Assert.That.AreEqual(expected.ParameterInfos.Count, result.ParameterInfos.Count);
      }
   }

   private static T AssertGetComponent<T>(IEntry entry, ComponentKind kind) where T : class, IComponent
   {
      Assert.IsTrue(entry.Components.TryGetValue(kind, out IComponent? component));

      T? typedComponent = component as T;

      Assert.IsNotNull(typedComponent);

      return typedComponent;

   }
   #endregion
}