using System.Diagnostics;

namespace TNO.ReadingWriting.IntegrationTests.TestBases.FileSystem;
public abstract class FileSystemIntegration : IntegrationTestBase
{
   #region Properties
   protected string Path { get; }
   #endregion
   public FileSystemIntegration()
   {
      Path = FileSystemUtility.GetTestClassFolder();
      Debug.WriteLine($"Test Folder: {Path}");
   }

   #region Methods
   protected override void Cleanup() => FileSystemUtility.CleanupTestFolder(Path);
   #endregion

   #region Helpers
   protected string GetSubFolder(string path)
   {
      string testPath = FileSystemUtility.GetTestFolder(Path);
      string newPath = System.IO.Path.Combine(testPath, path);

      if (Directory.Exists(newPath))
      {
         Type testType = GetType();
         Assert.Inconclusive($"[{testType.Name}] tried to create a sub folder ({path}) but it already existed.");
      }

      Directory.CreateDirectory(newPath);

      return newPath;
   }
   #endregion
}