namespace TNO.Logging.Common.Shared;

/// <summary>
/// Stores the shared constants for the file system log.
/// </summary>
public static class FileSystemConstants
{
   /// <summary>The file name for an uncompressed piece of data.</summary>
   public const string UncompressedName = "data";

   /// <summary>The file name for a compressed piece of data.</summary>
   public const string CompressedName = "chunk";

   /// <summary>The folder path for saving entry information.</summary>
   public const string EntryPath = "entries";

   /// <summary>The folder path for saving file information.</summary>
   public const string FilePath = "files";

   /// <summary>The folder path for saving context information.</summary>
   public const string ContextInfoPath = "contexts";

   /// <summary>The folder path for saving tag information.</summary>
   public const string TagPath = "tags";

   /// <summary>The folder path for saving table key information.</summary>
   public const string TableKeyPath = "table-keys";

   /// <summary>The folder path for saving assembly information.</summary>
   public const string AssemblyPath = "assemblies";

   /// <summary>The folder path for saving type information.</summary>
   public const string TypePath = "types";

   /// <summary>The file name for saving the versioning information.</summary>
   public const string VersionPath = "versions";

   /// <summary>The file extension for a compressed file.</summary>
   public const string ArchiveExtension = "zip";

   /// <summary>The <see cref="ArchiveExtension"/> with a leading dot.</summary>
   public const string DotArchiveExtension = "." + ArchiveExtension;
}
