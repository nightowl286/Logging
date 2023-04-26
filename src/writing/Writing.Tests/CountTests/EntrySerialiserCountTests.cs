using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Entries;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries;

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

      ImportanceCombination Importance = Severity.Negligible | Purpose.Telemetry;
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
}
