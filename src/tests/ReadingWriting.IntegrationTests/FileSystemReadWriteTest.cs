﻿using System.Runtime.CompilerServices;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData;
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
   #region Fields
   private static TimeSpan ReadTimeout = TimeSpan.FromMilliseconds(500);
   #endregion

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
      ulong expectedContextParentId = 0;
      ulong expectedScope = 1;

      string expectedTag = "tag";
      ulong expectedTagId = 1;


      string logPath = GetSubFolder("Log");

      // Write
      {
         LogWriterFacade facade = new LogWriterFacade();

         ILogger logger = facade.CreateConfigurator()
            .WithFileSystem(logPath, out IFileSystemLogWriter writer)
            .Logger
            .CreateContext(expectedContext, expectedFile, expectedLine)
            .CreateScoped();

         using (writer)
         {
            logger
               .StartEntry(expectedImportance, expectedFile, expectedLine)
               .With(expectedMessage)
               .WithTag(expectedTag)
               .FinishEntry();
         }
      }

      FileReference fileReference;
      IEntry entry;
      ContextInfo contextInfo;
      TagReference tagReference;

      IMessageComponent message;
      ITagComponent tag;

      // Read
      {
         LogReaderFacade facade = new LogReaderFacade();
         using IFileSystemLogReader reader = facade.ReadFromFileSystem(logPath);

         fileReference = AssertReadSingle(reader.FileReferences);
         entry = AssertReadSingle(reader.Entries);
         contextInfo = AssertReadSingle(reader.ContextInfos);
         tagReference = AssertReadSingle(reader.TagReferences);

         message = AssertGetComponent<IMessageComponent>(entry, ComponentKind.Message);
         tag = AssertGetComponent<ITagComponent>(entry, ComponentKind.Tag);
      }

      // Assert
      {
         Assert.AreEqual(fileReference.File, expectedFile);
         Assert.AreEqual(fileReference.Id, expectedFileId);

         Assert.AreEqual(fileReference.Id, entry.FileId);
         Assert.AreEqual(expectedLine, entry.LineInFile);
         Assert.AreEqual(expectedImportance, entry.Importance);
         Assert.AreEqual(expectedContextId, entry.Id);
         Assert.AreEqual(expectedScope, entry.Scope);

         Assert.AreEqual(contextInfo.Name, expectedContext);
         Assert.AreEqual(contextInfo.Id, expectedContextId);
         Assert.AreEqual(contextInfo.ParentId, expectedContextParentId);
         Assert.AreEqual(contextInfo.FileId, expectedFileId);
         Assert.AreEqual(contextInfo.LineInFile, expectedLine);

         Assert.AreEqual(tagReference.Tag, expectedTag);
         Assert.AreEqual(tagReference.Id, expectedTagId);

         Assert.AreEqual(expectedMessage, message.Message);
         Assert.AreEqual(expectedTagId, tag.TagId);
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
            .WithFileSystem(writerSettings, out IFileSystemLogWriter writer)
            .Logger;

         using (writer)
         {
            logger
               .Log(Importance.Inherit, "test1")
               .Log(Importance.Inherit, "test2");
         }
      }

      IEntry entry1;
      IEntry entry2;

      // Read & Assert
      {
         LogReaderFacade facade = new LogReaderFacade();
         using IFileSystemLogReader reader = facade.ReadFromFileSystem(logPath);

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
         .WithFileSystem(writerSettings, out IFileSystemLogWriter writer)
         .Logger;

      IFileSystemLogReader reader = new LogReaderFacade()
         .ReadFromFileSystem(logPath);

      // Act & Assert
      using (writer)
      using (reader)
      {
         AssertCantRead(reader.Entries);
         {
            logger.Log(Importance.Inherit, expectedMessage1);
            IEntry entry1 = AssertRead(reader.Entries);

            IMessageComponent message1 = AssertGetComponent<IMessageComponent>(entry1, ComponentKind.Message);
            Assert.AreEqual(expectedMessage1, message1.Message);
         }

         AssertCantRead(reader.Entries);
         {
            logger.Log(Importance.Inherit, expectedMessage2);
            IEntry entry2 = AssertRead(reader.Entries);

            IMessageComponent message2 = AssertGetComponent<IMessageComponent>(entry2, ComponentKind.Message);
            Assert.AreEqual(expectedMessage2, message2.Message);
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
         Assert.Fail($"[{name}] More data could be read, even though none was expected.");
   }
   private static T AssertReadSingle<T>(IReader<T> reader, [CallerArgumentExpression(nameof(reader))] string name = "<Reader>")
   {
      T data = AssertRead(reader, name);
      AssertCantRead(reader, name);

      return data;
   }
   #endregion
}