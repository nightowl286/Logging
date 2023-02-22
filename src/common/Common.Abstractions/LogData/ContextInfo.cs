namespace TNO.Logging.Common.Abstractions.LogData;

/// <summary>
/// Represents the info about a given logging context.
/// </summary>
public readonly struct ContextInfo
{
   #region Fields
   /// <summary>The name that was given to this context.</summary>
   /// <remarks>The name is not guaranteed to be unique.</remarks>
   public readonly string Name;

   /// <summary>The id of this context.</summary>
   public readonly ulong Id;

   /// <summary>The id of this context's parent.</summary>
   public readonly ulong ParentId;

   /// <summary>The id of the file where this context has been created.</summary>
   public readonly ulong FileId;

   /// <summary>
   /// The line number in the file (specified by the <see cref="FileId"/>)
   /// where this context has been created.
   /// </summary>
   public readonly uint LineInFile;
   #endregion

   #region Constructors
   /// <summary>
   /// Creates a new context info struct.
   /// </summary>
   /// <param name="name">The name of the context.</param>
   /// <param name="id">The id of the context.</param>
   /// <param name="parentId">The id of context's parent.</param>
   /// <param name="fileId">The id of the file where this context has been created.</param>
   /// <param name="lineInFile">The line number in the file where this context has been created.</param>
   public ContextInfo(string name, ulong id, ulong parentId, ulong fileId, uint lineInFile)
   {
      Name = name;
      Id = id;
      ParentId = parentId;
      FileId = fileId;
      LineInFile = lineInFile;
   }
   #endregion
}
