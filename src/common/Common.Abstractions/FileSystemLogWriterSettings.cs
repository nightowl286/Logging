namespace TNO.Logging.Common.Abstractions;

/// <summary>
/// Represents the settings that a file system log writer should use.
/// </summary>
public class FileSystemLogWriterSettings
{
   #region Consts
   /// <summary>The default threshold that is used for each setting.</summary>
   public const long DefaultThreshold = 500_000; // 500 KB
   #endregion

   #region Properties
   /// <summary>The path of where the log should be saved.</summary>
   public string LogPath { get; set; }

   /// <summary>The threshold that should be reached for saved entries before they get compressed. In bytes.</summary>
   public long EntryThreshold { get; set; } = DefaultThreshold;

   /// <summary>The threshold that should be reached for saved file references before they get compressed. In bytes.</summary>
   public long FileReferenceThreshold { get; set; } = DefaultThreshold;

   /// <summary>The threshold that should be reached for saved context infos before they get compressed. In bytes.</summary>
   public long ContextInfoThreshold { get; set; } = DefaultThreshold;

   /// <summary>The threshold that should be reached for saved tag references before they get compressed. In bytes.</summary>
   public long TagReferenceThreshold { get; set; } = DefaultThreshold;

   /// <summary>The threshold that should be reached for saved table key references before they get compressed. In bytes.</summary>
   public long TableKeyReferenceThreshold { get; set; } = DefaultThreshold;

   /// <summary>The threshold that should be reached for saved assembly references before they get compressed. In bytes.</summary>
   public long AssemblyReferenceThreshold { get; set; } = DefaultThreshold;

   /// <summary>The threshold that should be reached for saved type references before they get compressed. In bytes.</summary>
   public long TypeReferenceThreshold { get; set; } = DefaultThreshold;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="FileSystemLogWriterSettings"/>.</summary>
   /// <param name="logPath">The path of where the log should be saved.</param>
   public FileSystemLogWriterSettings(string logPath)
      => LogPath = logPath;
   #endregion
}
