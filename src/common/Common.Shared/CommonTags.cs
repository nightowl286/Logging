using System;
using System.Reflection;

namespace TNO.Logging.Common.Shared;

/// <summary>
/// Contains the names of commonly used logging tags.
/// </summary>
public static class CommonTags
{
   #region Consts
   private const string Internal = $"(Internal)_";
   private const string Common = $"(Common)_";

   /// <summary>Tag associated with information about the <see cref="Assembly.GetEntryAssembly"/>.</summary>
   public const string EntryAssembly = Internal + "Entry-Assembly";

   /// <summary>Tag associated with information about the <see cref="Assembly"/> of the current writer.</summary>
   public const string WriterAssembly = Internal + "Writer-Assembly";


   /// <summary>Tag associated with information about an unknown <see cref="Exception"/>.</summary>
   public const string UnknownExceptionType = Internal + "Unknown-Exception-Type";

   /// <summary>Tag associated with the time that the log was created at.</summary>
   public const string LogStartTime = Common + "Log-Start-Time";
   #endregion
}
