using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TNO.Logging.Abstractions;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Entries.Importance;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.LogData;
using TNO.Logging.Reading;
using TNO.Logging.Reading.Abstractions.Readers;
using TNO.Logging.Reading.Builders;
using TNO.Logging.Writing;
using TNO.Logging.Writing.Abstractions.Writers;
using TNO.Logging.Writing.Builders;
using TNO.ReadingWriting.IntegrationTests.TestBases.FileSystem;

namespace TNO.ReadingWriting.IntegrationTests;

[TestClass]
[TestCategory(Category.Serialisation)]
public class FileSystemReadWriteTests : FileSystemIntegration
{
   #region Fields
   private static TimeSpan ReadTimeout = TimeSpan.FromSeconds(1);
   #endregion

   #region Methods
   [TestMethod]
   public void Write_Read_Successful()
   {
      // Arrange
      ImportanceCombination expectedImportance = Severity.Negligible | Purpose.Telemetry;
      string expectedMessage = "message";
      string expectedFile = "file";
      ulong expectedFileId = 1;
      uint expectedLine = 5;

      Thread expectedThread = new Thread(() => { });
      string expectedContext = "context";
      ulong expectedContextId = 1;
      ulong expectedContextParentId = 0;
      ulong expectedScope = 1;

      string expectedTag = "tag";
      ulong expectedTagId = 1;
      ulong entryId;

      string expectedTableKey = "key";
      uint expectedTableKeyId = 1;
      string expectedTableValue = "test";

      string expectedExceptionMessage = "Exception message";
      Exception exception = new Exception(expectedExceptionMessage);

      Assembly assembly = Assembly.GetExecutingAssembly();
      ulong expectedAssemblyId = 1;
      AssemblyInfo expectedAssemblyInfo = AssemblyInfo.FromAssembly(assembly);
      AssemblyReference expectedAssemblyReference = new AssemblyReference(expectedAssemblyInfo, expectedAssemblyId);

      string logPath = GetSubFolder("Log");


      // Write
      {
         LogWriterFacade facade = new LogWriterFacade();

         ILogger logger = facade.CreateConfigurator()
            .WithInternalLogger(false)
            .WithFileSystem(logPath, out IFileSystemLogWriter writer)
            .Create()
            .CreateContext(expectedContext, expectedFile, expectedLine)
            .CreateScoped();

         using (writer)
         {
            logger
               .StartEntry(expectedImportance, out entryId, expectedFile, expectedLine)
                  .With(expectedMessage)
                  .WithTag(expectedTag)
                  .With(expectedThread)
                  .With(entryId)
                  .WithTable()
                     .With(expectedTableKey, expectedTableValue)
                     .BuildTable()
                  .With(assembly)
                  .With(exception)
               .FinishEntry();
         }
      }

      FileReference fileReference;
      IEntry entry;
      ContextInfo contextInfo;
      TagReference tagReference;
      TableKeyReference tableKeyReference;
      AssemblyReference assemblyReference;

      IMessageComponent message;
      ITagComponent tag;
      IThreadComponent thread;
      IEntryLinkComponent entryLink;
      ITableComponent table;
      IAssemblyComponent assemblyComponent;
      IExceptionComponent exceptionComponent;

      // Read
      {
         using IFileSystemLogReader reader = new LogReaderFacade()
            .CreateConfigurator()
            .FromFileSystem(logPath);

         fileReference = AssertReadSingle(reader.FileReferences);
         entry = AssertReadSingle(reader.Entries);
         contextInfo = AssertReadSingle(reader.ContextInfos);
         tagReference = AssertReadSingle(reader.TagReferences);
         tableKeyReference = AssertReadSingle(reader.TableKeyReferences);
         assemblyReference = AssertRead(reader.AssemblyReferences);
         _ = AssertReadSingle(reader.AssemblyReferences); // exception type assembly

         message = AssertGetComponent<IMessageComponent>(entry, ComponentKind.Message);
         tag = AssertGetComponent<ITagComponent>(entry, ComponentKind.Tag);
         thread = AssertGetComponent<IThreadComponent>(entry, ComponentKind.Thread);
         entryLink = AssertGetComponent<IEntryLinkComponent>(entry, ComponentKind.EntryLink);
         table = AssertGetComponent<ITableComponent>(entry, ComponentKind.Table);
         assemblyComponent = AssertGetComponent<IAssemblyComponent>(entry, ComponentKind.Assembly);
         exceptionComponent = AssertGetComponent<IExceptionComponent>(entry, ComponentKind.Exception);
      }

      // Assert
      {
         Assert.That.AreEqual(fileReference.File, expectedFile);
         Assert.That.AreEqual(fileReference.Id, expectedFileId);

         Assert.That.AreEqual(fileReference.Id, entry.FileId);
         Assert.That.AreEqual(expectedLine, entry.LineInFile);
         Assert.That.AreEqual(expectedImportance, entry.Importance);
         Assert.That.AreEqual(entryId, entry.Id);
         Assert.That.AreEqual(expectedScope, entry.Scope);

         Assert.That.AreEqual(contextInfo.Name, expectedContext);
         Assert.That.AreEqual(contextInfo.Id, expectedContextId);
         Assert.That.AreEqual(contextInfo.ParentId, expectedContextParentId);
         Assert.That.AreEqual(contextInfo.FileId, expectedFileId);
         Assert.That.AreEqual(contextInfo.LineInFile, expectedLine);

         Assert.That.AreEqual(tagReference.Tag, expectedTag);
         Assert.That.AreEqual(tagReference.Id, expectedTagId);

         Assert.That.AreEqual(expectedMessage, message.Message);
         Assert.That.AreEqual(expectedTagId, tag.TagId);

         Assert.That.AreEqual(expectedThread.ManagedThreadId, thread.ManagedId);
         Assert.That.AreEqual(expectedThread.Name ?? string.Empty, thread.Name);
         Assert.That.AreEqual(expectedThread.IsThreadPoolThread, thread.IsThreadPoolThread);
         Assert.That.AreEqual(expectedThread.ThreadState, thread.State);
         Assert.That.AreEqual(expectedThread.Priority, thread.Priority);
         Assert.That.AreEqual(expectedThread.GetApartmentState(), thread.ApartmentState);

         Assert.That.AreEqual(entryId, entryLink.EntryId);

         Assert.That.AreEqual(expectedTableKeyId, tableKeyReference.Id);
         Assert.That.AreEqual(expectedTableKey, tableKeyReference.Key);

         Assert.That.AreEqual(1, table.Table.Table.Count);
         object? tableValue = table.Table.Table[expectedTableKeyId];
         Assert.That.AreEqual(expectedTableValue, tableValue);

         Assert.That.AreEqual(expectedAssemblyId, assemblyComponent.AssemblyId);
         Assert.That.AreEqual(expectedAssemblyId, assemblyReference.Id);
         IAssemblyInfo assemblyInfo = assemblyReference.AssemblyInfo;

         Assert.That.AreEqual(expectedAssemblyInfo.Name, assemblyInfo.Name);
         Assert.That.AreEqual(expectedAssemblyInfo.Version?.Major, assemblyInfo.Version?.Major);
         Assert.That.AreEqual(expectedAssemblyInfo.Version?.Minor, assemblyInfo.Version?.Minor);
         Assert.That.AreEqual(expectedAssemblyInfo.Version?.Build, assemblyInfo.Version?.Build);
         Assert.That.AreEqual(expectedAssemblyInfo.Version?.Revision, assemblyInfo.Version?.Revision);
         Assert.That.AreEqual(expectedAssemblyInfo.Culture?.Name, assemblyInfo.Culture?.Name);
         Assert.That.AreEqual(expectedAssemblyInfo.DebuggingFlags, assemblyInfo.DebuggingFlags);
         Assert.That.AreEqual(expectedAssemblyInfo.Configuration, assemblyInfo.Configuration);
         Assert.That.AreEqual(expectedAssemblyInfo.PeKinds, assemblyInfo.PeKinds);
         Assert.That.AreEqual(expectedAssemblyInfo.TargetPlatform, assemblyInfo.TargetPlatform);

         Assert.That.AreEqual(expectedExceptionMessage, exceptionComponent.ExceptionInfo.Message);
      }
   }

   [TestMethod]
   public void ChunkedWrite_Read_Successful()
   {
      // Arrange
      string logPath = GetSubFolder("Log");
      FileSystemLogWriterSettings writerSettings = new FileSystemLogWriterSettings(logPath)
      {
         EntryThreshold = 0
      };

      // Write
      {
         LogWriterFacade facade = new LogWriterFacade();
         ILogger logger = facade.CreateConfigurator()
            .WithInternalLogger(false)
            .WithFileSystem(writerSettings, out IFileSystemLogWriter writer)
            .Create();

         using (writer)
         {
            logger
               .Log(ImportanceCombination.Inherit, "test1")
               .Log(ImportanceCombination.Inherit, "test2");
         }
      }

      IEntry entry1;
      IEntry entry2;

      // Read & Assert
      {
         using IFileSystemLogReader reader = new LogReaderFacade()
            .CreateConfigurator()
            .FromFileSystem(logPath);

         entry1 = AssertRead(reader.Entries);
         entry2 = AssertReadSingle(reader.Entries);
      }
   }

   [TestMethod]
   public void ChunkedWrite_LiveRead_Successful()
   {
      // Arrange
      string expectedMessage1 = "test1";
      string expectedMessage2 = "test2";

      string logPath = GetSubFolder("Log");
      FileSystemLogWriterSettings writerSettings = new FileSystemLogWriterSettings(logPath)
      {
         EntryThreshold = 0
      };

      ILogger logger = new LogWriterFacade()
         .CreateConfigurator()
         .WithInternalLogger(false)
         .WithFileSystem(writerSettings, out IFileSystemLogWriter writer)
         .Create();

      IFileSystemLogReader reader = new LogReaderFacade()
         .CreateConfigurator()
         .FromFileSystem(logPath);

      // Act & Assert
      using (writer)
      using (reader)
      {
         AssertCantRead(reader.Entries);
         {
            logger.Log(ImportanceCombination.Inherit, expectedMessage1);
            IEntry entry1 = AssertRead(reader.Entries);

            IMessageComponent message1 = AssertGetComponent<IMessageComponent>(entry1, ComponentKind.Message);
            Assert.That.AreEqual(expectedMessage1, message1.Message);
         }

         AssertCantRead(reader.Entries);
         {
            logger.Log(ImportanceCombination.Inherit, expectedMessage2);
            IEntry entry2 = AssertRead(reader.Entries);

            IMessageComponent message2 = AssertGetComponent<IMessageComponent>(entry2, ComponentKind.Message);
            Assert.That.AreEqual(expectedMessage2, message2.Message);
         }

         AssertCantRead(reader.Entries);
      }
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
   private static void AssertCanRead<T>(IReader<T> reader, TimeSpan timeout, [CallerArgumentExpression(nameof(reader))] string name = "<Reader>")
   {
      bool canRead = SpinWait.SpinUntil(reader.CanRead, (int)timeout.TotalMilliseconds);
      if (canRead == false)
         Assert.Fail($"[{name}] No more data could be read within the given timeout.");
   }
   private static T AssertRead<T>(IReader<T> reader, [CallerArgumentExpression(nameof(reader))] string name = "<Reader>")
   {
      AssertCanRead(reader, ReadTimeout, name);

      T data = reader.Read();

      return data;
   }
   private static void AssertCantRead<T>(IReader<T> reader, [CallerArgumentExpression(nameof(reader))] string name = "<Reader>")
   {
      if (reader.CanRead())
      {
         StringBuilder loggedDataBuilder = new StringBuilder();
         while (reader.CanRead())
         {
            T data = reader.Read();
            loggedDataBuilder.AppendLine(data?.ToString());
         }

         Assert.Fail($"[{name}] More data could be read, even though none was expected.\nRemaining data:\n{loggedDataBuilder}");
      }
   }
   private static T AssertReadSingle<T>(IReader<T> reader, [CallerArgumentExpression(nameof(reader))] string name = "<Reader>")
   {
      T data = AssertRead(reader, name);
      AssertCantRead(reader, name);

      return data;
   }
   #endregion
}