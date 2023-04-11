using System.Reflection;

namespace TNO.Logging.Common.Shared;

/// <summary>
/// Contains the names of commonly used logging tags.
/// </summary>
public static class CommonTags
{
   /// <summary>Tag associated with information about the <see cref="Assembly.GetEntryAssembly"/>.</summary>
   public const string EntryAssembly = "(Internal)_Entry-Assembly";

   /// <summary>Tag associated with information about the <see cref="Assembly"/> of the current writer.</summary>
   public const string WriterAssembly = "(Internal)_Writer-Assembly";

   /// <summary>Tag associated with the time that the log was created at.</summary>
   public const string LogStartTime = "(Common)_Log-Start-Time";
}
