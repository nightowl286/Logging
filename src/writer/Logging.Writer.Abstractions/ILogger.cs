using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using TNO.Common.Abstractions;

namespace TNO.Logging.Writer.Abstractions;

/// <summary>
/// Denotes a logger.
/// </summary>
public interface ILogger : ILoggerContextFactory
{
   #region Methods
   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="message"/>.</summary>
   /// <param name="severity">The severity of the given <paramref name="message"/>.</param>
   /// <param name="message">The message to log.</param>
   /// <param name="entryId">The id of the logged entry.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   ILogger Log(Severity severity, string message, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0);

   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="stackFrame"/>.</summary>
   /// <param name="severity">The severity of the given <paramref name="stackFrame"/>.</param>
   /// <param name="stackFrame">The stack frame to log.</param>
   /// <param name="entryId">The id of the logged entry.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   ILogger Log(Severity severity, StackFrame stackFrame, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0);

   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="stackTrace"/>.</summary>
   /// <param name="severity">The severity of the given <paramref name="stackTrace"/>.</param>
   /// <param name="stackTrace">The stack trace to log.</param>
   /// <param name="entryId">The id of the logged entry.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   ILogger Log(Severity severity, StackTrace stackTrace, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0);

   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="exception"/>.</summary>
   /// <param name="severity">The severity of the given <paramref name="exception"/>.</param>
   /// <param name="exception">The exception to log.</param>
   /// <param name="entryId">The id of the logged entry.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   /// <remarks>
   /// This will also include a <see cref="StackTrace"/> generated from the 
   /// <paramref name="exception"/>, and also the <see cref="Thread.CurrentThread"/>.
   /// </remarks>
   ILogger Log(Severity severity, Exception exception, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0);

   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="thread"/>.</summary>
   /// <param name="severity">The severity of the given <paramref name="thread"/>.</param>
   /// <param name="thread">The thread to log.</param>
   /// <param name="entryId">The id of the logged entry.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   ILogger Log(Severity severity, Thread thread, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0);

   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="assembly"/>.</summary>
   /// <param name="severity">The severity of the given <paramref name="assembly"/>.</param>
   /// <param name="assembly">The assembly to log.</param>
   /// <param name="entryId">The id of the logged entry.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   ILogger Log(Severity severity, Assembly assembly, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0);

   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given additional <paramref name="filePath"/>.</summary>
   /// <param name="severity">The severity of the given <paramref name="filePath"/>.</param>
   /// <param name="filePath">The path of the file that should be included.</param>
   /// <param name="entryId">The id of the logged entry.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   ILogger LogAdditionalPath(Severity severity, string filePath, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0);

   /// <summary>Links the given <paramref name="entryIds"/> together.</summary>
   /// <param name="entryIds">The ids of the entries to link.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   ILogger CreateLinks(ulong[] entryIds,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0);

   /// <summary>Starts building an entry, provides an <see cref="ILogEntryBuilder"/> that can be used to customise the entry.</summary>
   /// <param name="severity">The severity of the entry that will be logged.</param>
   /// <param name="entryId">The id of the entry that will be logged.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>An instance of <see cref="ILogEntryBuilder"/> that can be used to customise the entry.</returns>
   /// <remarks><see cref="ILogEntryBuilder.FinishEntry"/> must be called to actually log the entry.</remarks>
   ILogEntryBuilder StartEntry(Severity severity, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0);
   #endregion
}
