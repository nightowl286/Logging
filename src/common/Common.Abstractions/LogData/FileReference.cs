namespace TNO.Logging.Common.Abstractions.LogData;

/// <summary>
/// Represents a link between a <see cref="File"/> and it's <see cref="Id"/>.
/// </summary>
public readonly struct FileReference
{
   #region Fields
   /// <summary>The file that the <see cref="Id"/> is associated with.</summary>
   public readonly string File;

   /// <summary>The id that the <see cref="File"/> is associated with.</summary>
   public readonly ulong Id;
   #endregion

   #region Constructors
   /// <summary>Creates a new file reference.</summary>
   /// <param name="file">The file.</param>
   /// <param name="id">The id of the <paramref name="file"/>.</param>
   public FileReference(string file, ulong id)
   {
      File = file;
      Id = id;
   }
   #endregion
}