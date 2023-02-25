global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using TNO.Tests.Common;

#if DEBUG
[assembly: Parallelize(Scope = ExecutionScope.ClassLevel, Workers = 1)]
#else
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel, Workers = 0)]
#endif

internal static class Category
{
   public const string Severity = nameof(Severity);
   public const string Purpose = nameof(Purpose);
   public const string Importance = nameof(Importance);
   public const string Extensions = nameof(Extensions);
}