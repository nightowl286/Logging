using System.Reflection;

namespace TNO.Logging.Common.Abstractions.LogData.Assemblies;

/// <summary>
/// Represents the different kinds of locations an <see cref="Assembly"/> may be in.
/// </summary>
public enum AssemblyLocationKind : byte
{
   /// <summary>Assembly is located in one of the shared dotnet runtime directories.</summary>
   SharedRuntime,

   /// <summary>The assembly is located in the same folder as the application.</summary>
   Application,

   /// <summary>The assembly is stored in a directory that is not nested under the application directory.</summary>
   External,

   /// <summary>The location of the assembly is not known.</summary>
   /// <remarks>
   /// This can happen if the assembly not been loaded in from a file directly (e.g. <see cref="Assembly.Load(byte[])"/>),
   /// or if it is a dynamically generated assembly, e.t.c.
   /// </remarks>
   Unknown,
}