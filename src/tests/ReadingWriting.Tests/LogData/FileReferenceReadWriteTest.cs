using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.ReadingWriting.Tests.LogData;

[TestClass]
public class FileReferenceReadWriteTest : ReadWriteTestBase<FileReferenceSerialiser, FileReferenceDeserialiserLatest, FileReference>
{
   #region Methods
   protected override FileReference CreateData()
   {
      return new FileReference("file", 5);
   }

   protected override void Verify(FileReference expected, FileReference result)
   {
      Assert.AreEqual(expected.File, result.File);
      Assert.AreEqual(expected.Id, result.Id);
   }
   #endregion
}