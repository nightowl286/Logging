using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Writing.Serialisers.LogData;

namespace TNO.ReadingWriting.Tests.LogData;

[TestClass]
public class ContextInfoReadWriteTests : ReadWriteTestsBase<ContextInfoSerialiser, ContextInfoDeserialiserLatest, ContextInfo>
{
   #region Methods
   protected override ContextInfo CreateData()
   {
      return new ContextInfo("context", 1, 2, 3, 4);
   }

   protected override void Verify(ContextInfo expected, ContextInfo result)
   {
      Assert.That.AreEqual(expected.Name, result.Name);
      Assert.That.AreEqual(expected.Id, result.Id);
      Assert.That.AreEqual(expected.ParentId, result.ParentId);
      Assert.That.AreEqual(expected.FileId, result.FileId);
      Assert.That.AreEqual(expected.LineInFile, result.LineInFile);
   }
   #endregion
}
