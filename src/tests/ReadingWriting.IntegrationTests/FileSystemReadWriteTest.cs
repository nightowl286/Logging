using System.Runtime.CompilerServices;
using TNO.Common.Abstractions;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading;
using TNO.Logging.Reading.Abstractions.Readers;
using TNO.Logging.Writing;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.ReadingWriting.IntegrationTests.TestBases.FileSystem;

namespace TNO.ReadingWriting.IntegrationTests;

[TestClass]
[TestCategory(Category.Serialisation)]
public class FileSystemReadWriteTest : FileSystemIntegration
{
   #region Methods
   [TestMethod]
   public void Write_Read_Successful()
   {
      // Arrange
      SeverityAndPurpose expectedSeverityAndPurpose = Severity.Negligible | Purpose.Telemetry;
      string expectedMessage = "message";
      string expectedFile = "file";
      uint expectedLine = 5;

      string logPath = GetSubFolder("Log");

      // Write
      {
         LogWriterFacade facade = new LogWriterFacade();
         using IDisposableLogger logger = facade.CreateOnFileSystem(logPath);

         logger.Log(
            expectedSeverityAndPurpose,
            expectedMessage,
            expectedFile,
            expectedLine);
      }

      FileReference fileReference;
      IEntry entry;
      IMessageComponent message;

      // Read
      {
         LogReaderFacade facade = new LogReaderFacade();
         using IFileSystemLogReader reader = facade.ReadFromFileSystem(logPath);

         fileReference = AssertReadSingle(reader.FileReferences);
         entry = AssertReadSingle(reader.Entries);
         message = AssertGetComponent<IMessageComponent>(entry, ComponentKind.Message);
      }

      // Assert
      Assert.AreEqual(fileReference.File, expectedFile);

      Assert.AreEqual(fileReference.Id, entry.FileId);
      Assert.AreEqual(expectedLine, entry.LineInFile);
      Assert.AreEqual(expectedSeverityAndPurpose, entry.SeverityAndPurpose);

      Assert.AreEqual(expectedMessage, message.Message);
   }
   #endregion

   #region Helpers
   private static T AssertGetComponent<T>(IEntry entry, ComponentKind kind) where T : IComponent
   {
      if (entry.Components.TryGetValue(kind, out IComponent? component) == false)
         Assert.Fail($"The given entry did not have a {kind} component.");

      if (component is T typedComponent)
         return typedComponent;

      Assert.Inconclusive($"Could not cast the read component ({component.GetType()}) to the type {typeof(T)}. Something else is likely broken.");
      return default!; // won't ever happen because of the Assert.Inconclusive.
   }
   private static T AssertReadSingle<T>(IReader<T> reader, [CallerArgumentExpression(nameof(reader))] string name = "<Reader>")
   {
      if (reader.CanRead() == false)
         Assert.Fail($"[{name}] There was no data to read, expected exactly 1.");

      T data = reader.Read();

      if (reader.CanRead())
         Assert.Fail($"[{name}] More data could be read, even though only expected 1.");

      return data;
   }
   #endregion
}
