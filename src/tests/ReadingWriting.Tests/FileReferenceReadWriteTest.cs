using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing;

namespace TNO.ReadingWriting.Tests;

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