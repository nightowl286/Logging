using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing.Serialisers;

namespace TNO.ReadingWriting.Tests;

[TestClass]
public class ContextInfoReadWriteTest : ReadWriteTestBase<ContextInfoSerialiser, ContextInfoDeserialiserLatest, ContextInfo>
{
   #region Methods
   protected override ContextInfo CreateData()
   {
      return new ContextInfo("context", 1, 2, 3);
   }

   protected override void Verify(ContextInfo expected, ContextInfo result)
   {
      Assert.AreEqual(expected.Name, result.Name);
      Assert.AreEqual(expected.Id, result.Id);
      Assert.AreEqual(expected.FileId, result.FileId);
      Assert.AreEqual(expected.LineInFile, result.LineInFile);
   }
   #endregion
}
