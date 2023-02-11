namespace TNO.ReadingWriting.IntegrationTests.TestBases;

public abstract class IntegrationTestBase
{
   #region Methods
   [TestCleanup]
   protected abstract void Cleanup();
   #endregion
}
