using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.ReadingWriting.Tests.LogData;

[TestClass]
public class FileReferenceReadWriteTests : ReadWriteTestsBase<FileReferenceSerialiser, FileReferenceDeserialiserLatest, FileReference>
{
   #region Methods
   protected override FileReference CreateData()
   {
      return new FileReference("file", 5);
   }

   protected override void Verify(FileReference expected, FileReference result)
   {
      Assert.That.AreEqual(expected.File, result.File);
      Assert.That.AreEqual(expected.Id, result.Id);
   }
   #endregion
}