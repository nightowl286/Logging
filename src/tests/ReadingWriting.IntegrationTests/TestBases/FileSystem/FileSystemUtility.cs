using System.Diagnostics;
using System.Reflection;

namespace TNO.ReadingWriting.IntegrationTests.TestBases.FileSystem;
public static class FileSystemUtility
{
   #region Fields
   private static readonly string TestFolderRoot;
   #endregion
   static FileSystemUtility()
   {
      string assemblyPath = Assembly
         .GetExecutingAssembly()
         .Location;

      assemblyPath = Path.GetDirectoryName(assemblyPath)!;

      TestFolderRoot = Path.Combine(assemblyPath, "IntegrationTests");

      if (Directory.Exists(TestFolderRoot))
         Directory.Delete(TestFolderRoot, true);

      Directory.CreateDirectory(TestFolderRoot);
   }

   #region Functions
   public static string GetTestFolder()
   {
      Type? testClass = GetTestClass();
      string subFolder = testClass is null ? "Unknown" : GetSafeFolderName(testClass);

      string path = Path.Combine(TestFolderRoot, subFolder);
      Directory.CreateDirectory(path);

      return path;
   }

   public static void CleanupTestFolder(string folder)
   {
      Directory.Delete(folder, true);
   }
   #endregion

   #region Helpers
   private static string GetSafeFolderName(Type type)
   {
      char[] invalid = Path.GetInvalidPathChars();
      string name = type.Name;

      foreach (char ch in invalid)
         name = name.Replace(ch, '_');

      return name;
   }
   private static Type? GetTestClass()
   {
      StackTrace trace = new StackTrace(1, false);
      StackFrame[] frames = trace.GetFrames();
      foreach (StackFrame frame in frames)
      {
         MethodBase? method = frame.GetMethod();
         Type? declaringType = method?.DeclaringType;

         bool skipFrame =
            declaringType is null ||
            declaringType.IsAbstract ||
            declaringType.GetCustomAttribute<TestClassAttribute>() is null;

         if (skipFrame)
            continue;

         return declaringType;
      }

      return null;
   }
   #endregion
}