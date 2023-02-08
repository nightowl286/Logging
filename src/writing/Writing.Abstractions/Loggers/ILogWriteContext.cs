using System;

namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
/// Denotes a context that belongs to a <see cref="ILogWriter"/>.
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
   #endregion
}
