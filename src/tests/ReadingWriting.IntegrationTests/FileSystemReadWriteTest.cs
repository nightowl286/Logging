using System.Runtime.CompilerServices;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading;
using TNO.Logging.Reading.Abstractions.Readers;
using TNO.Logging.Writing;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Abstractions.Writers;
using TNO.Logging.Writing.Builders;
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
      Importance expectedImportance = Severity.Negligible | Purpose.Telemetry;
      string expectedMessage = "message";
      string expectedFile = "file";
      ulong expectedFileId = 1;
      uint expectedLine = 5;

      string expectedContext = "context";
      ulong expectedContextId = 1;
      ulong expectedScope = 1;


      string logPath = GetSubFolder("Log");

      // Write
      {
         LogWriterFacade facade = new LogWriterFacade();

         ILogger logger = facade.CreateBuilder()
            .WithFileSystem(logPath, out IFileSystemLogWriter writer)
            .Logger
            .CreateContext(expectedContext, expectedFile, expectedLine)
            .CreateScoped();

         using (writer)
         {
            logger.Log(
               expectedImportance,
               expectedMessage,
               expectedFile,
               expectedLine);
         }
      }

      FileReference fileReference;
      IEntry entry;
      ContextInfo contextInfo;
      IMessageComponent message;

      // Read
      {
         LogReaderFacade facade = new LogReaderFacade();
         using IFileSystemLogReader reader = facade.ReadFromFileSystem(logPath);

         fileReference = AssertReadSingle(reader.FileReferences);
         entry = AssertReadSingle(reader.Entries);
         contextInfo = AssertReadSingle(reader.ContextInfos);
         message = AssertGetComponent<IMessageComponent>(entry, ComponentKind.Message);
      }

      // Assert
      Assert.AreEqual(fileReference.File, expectedFile);
      Assert.AreEqual(fileReference.Id, expectedFileId);

      Assert.AreEqual(fileReference.Id, entry.FileId);
      Assert.AreEqual(expectedLine, entry.LineInFile);
      Assert.AreEqual(expectedImportance, entry.Importance);
      Assert.AreEqual(expectedContextId, entry.Id);
      Assert.AreEqual(expectedScope, entry.Scope);

      Assert.AreEqual(contextInfo.Name, expectedContext);
      Assert.AreEqual(contextInfo.Id, expectedContextId);
      Assert.AreEqual(contextInfo.FileId, expectedFileId);
      Assert.AreEqual(contextInfo.LineInFile, expectedLine);

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