using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries;
using TNO.Logging.Writing.Entries.Components;
using TNO.Logging.Writing.Serialisers.LogData.Constructors;
using TNO.Logging.Writing.Serialisers.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.StackTraces;

namespace TNO.Writing.Tests.BinarySerialiserCountTests;

[TestClass]
public class EntrySerialiserCountTests : BinarySerialiserCountTestBase<EntrySerialiser, IEntry>
{
   #region Tests
   [TestMethod]
   public void Count()
   {
      // Arrange
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

      // Act + Assert
      CountTestBase(entry);
   }
   #endregion

   #region Methods
   protected override EntrySerialiser Setup()
   {
      ParameterInfoSerialiser parameterInfoSerialiser = new ParameterInfoSerialiser();

      ComponentSerialiserDispatcher componentSerialiser =
         new ComponentSerialiserDispatcher(
            new MessageComponentSerialiser(),
            new TagComponentSerialiser(),
            new ThreadComponentSerialiser(),
            new EntryLinkComponentSerialiser(),
            new TableComponentSerialiser(),
            new AssemblyComponentSerialiser(),
            new StackTraceComponentSerialiser(
               new StackTraceInfoSerialiser(
                  new StackFrameInfoSerialiser(
                     new MethodBaseInfoSerialiserDispatcher(
                        new MethodInfoSerialiser(parameterInfoSerialiser),
                        new ConstructorInfoSerialiser(parameterInfoSerialiser))))));

      return new EntrySerialiser(componentSerialiser);
   }
   #endregion
}
