using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;

namespace TNO.Logging.Common.Abstractions.LogData.Assemblies;

/// <summary>
/// Denotes info about an <see cref="Assembly"/>.
/// </summary>
public interface IAssemblyInfo
{
   #region Properties
   /// <summary>The name of the assembly.</summary>
   string? Name { get; }

   /// <summary>The version of the assembly.</summary>
   Version? Version { get; }

   /// <summary>The culture supported by the assembly.</summary>
   CultureInfo? Culture { get; }

   /// <summary>The kind of the location where the assembly is located.</summary>
   AssemblyLocationKind LocationKind { get; }

   /// <summary>The location where the assembly is located.</summary>
   string Location { get; }

   /// <summary>The debugging flags associated with this assembly.</summary>
   DebuggableAttribute.DebuggingModes? DebuggingFlags { get; }

   /// <summary>The configuration in which the assembly was compiled.</summary>
   string Configuration { get; }

   /// <summary>The nature of the code in the <see cref="Assembly.ManifestModule"/>.</summary>
   PortableExecutableKinds PeKinds { get; }

   /// <summary>The platform targeted by the <see cref="Assembly.ManifestModule"/>.</summary>
   ImageFileMachine TargetPlatform { get; }
   #endregion
}

