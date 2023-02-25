﻿global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using TNO.Tests.Common;

#if DEBUG
[assembly: Parallelize(Scope = ExecutionScope.ClassLevel, Workers = 1)]
#else
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel, Workers = 0)]
#endif

internal static class Category
{
   public const string Serialisation = nameof(Serialisation);
   public const string Components = nameof(Components);
   public const string Counting = nameof(Counting);
}