using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries;
using TNO.Logging.Writing.Entries.Components;

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

      // Act + Assert
      CountTestBase(entry);
   }
   #endregion

   #region Methods
   protected override EntrySerialiser Setup()
   {
      ComponentSerialiserDispatcher componentSerialiser =
         new ComponentSerialiserDispatcher(
            new MessageComponentSerialiser());

      return new EntrySerialiser(componentSerialiser);
   }
   #endregion
}
