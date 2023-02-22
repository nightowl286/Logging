using System;

namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
/// Denotes a context that stores information about the current logging session.
/// </summary>
public interface ILogWriteContext
{
   #region Methods
   /// <summary>Gets a unique id for a new entry.</summary>
   /// <returns>The id that should be assigned to an entry.</returns>
   /// <remarks>The generated id is only unique within the current log.</remarks>
   ulong NewEntryId();

   /// <summary>Gets the current timestamp since the log was created.</summary>
   /// <returns>A <see cref="TimeSpan"/> that represents the time since this log was created.</returns>
   TimeSpan GetTimestamp();

   /// <summary>Gets or creates the <paramref name="fileId"/> for the given <paramref name="file"/>.</summary>
   /// <param name="file">The file to get or create the <paramref name="fileId"/> for.</param>
   /// <param name="fileId">The id of the given <paramref name="file"/>.</param>
   /// <returns>
   /// <see langword="true"/> if a new <paramref name="fileId"/> 
   /// had to be created, <see langword="false"/> otherwise.
   /// </returns>
   bool GetOrCreateFileId(string file, out ulong fileId);

   /// <summary>Gets or creates the <paramref name="tagId"/> for the given <paramref name="tag"/>.</summary>
   /// <param name="tag">The tag to get or create the <paramref name="tagId"/> for.</param>
   /// <param name="tagId">The id of the given <paramref name="tag"/>.</param>
   /// <returns>
   /// <see langword="true"/> if a new <paramref name="tagId"/> 
   /// had to be created, <see langword="false"/> otherwise.
   /// </returns>
   bool GetOrCreateTagId(string tag, out ulong tagId);

   /// <summary>Creates a new id for a context.</summary>
   /// <returns>The id of the newly created context.</returns>
   ulong CreateContextId();
   #endregion
}