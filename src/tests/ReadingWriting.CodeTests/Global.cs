global using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

#if DEBUG
[assembly: Parallelize(Scope = ExecutionScope.ClassLevel, Workers = 1)]
#else
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel, Workers = 0)]
#endif

internal static class Category
{
   public const string Versioning = nameof(Versioning);
}

internal static class TestAssemblies
{
   public static Assembly[] GetAssemblies()
   {
      string[] names = new string[]
      {
         "TNO.Logging.Common",
         "TNO.Logging.Common.Abstractions",
         "TNO.Logging.Common.Shared",
         "TNO.Logging.Common.Exceptions",
         "TNO.Logging.Common.Exceptions.Abstractions",
         "TNO.Logging.Reading",
         "TNO.Logging.Reading.Abstractions",
         "TNO.Logging.Writing",
         "TNO.Logging.Writing.Abstractions",
      };

      return names.Select(Assembly.Load).ToArray();
   }
}