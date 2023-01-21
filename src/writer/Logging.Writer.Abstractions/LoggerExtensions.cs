using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using TNO.Common.Abstractions;

namespace TNO.Logging.Writer.Abstractions;

/// <summary>
/// Contains useful extension methods for <see cref="ILogger"/> implementations.
/// </summary>
public static class LoggerExtensions
{
   #region Methods
   /// <summary>Logs an entry with the given <paramref name="severityAndPurpose"/> and the given <paramref name="message"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severityAndPurpose">The severity and purpose of the given <paramref name="message"/>.</param>
   /// <param name="message">The message to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, SeverityAndPurpose severityAndPurpose, string message,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severityAndPurpose, message, out _, file, line);

   /// <summary>Logs an entry with the given <paramref name="severityAndPurpose"/> and the given <paramref name="stackFrame"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severityAndPurpose">The severity and purpose of the given <paramref name="stackFrame"/>.</param>
   /// <param name="stackFrame">The stack frame to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, SeverityAndPurpose severityAndPurpose, StackFrame stackFrame,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severityAndPurpose, stackFrame, out _, file, line);

   /// <summary>Logs an entry with the given <paramref name="severityAndPurpose"/> and the given <paramref name="stackTrace"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severityAndPurpose">The severity and purpose of the given <paramref name="stackTrace"/>.</param>
   /// <param name="stackTrace">The stack trace to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, SeverityAndPurpose severityAndPurpose, StackTrace stackTrace,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severityAndPurpose, stackTrace, out _, file, line);

   /// <summary>Logs an entry with the given <paramref name="severityAndPurpose"/> and the given <paramref name="exception"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severityAndPurpose">The severity and purpose of the given <paramref name="exception"/>.</param>
   /// <param name="exception">The exception to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, SeverityAndPurpose severityAndPurpose, Exception exception,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severityAndPurpose, exception, out _, file, line);

   /// <summary>Logs an entry with the given <paramref name="severityAndPurpose"/> and the given <paramref name="thread"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severityAndPurpose">The severity and purpose of the given <paramref name="thread"/>.</param>
   /// <param name="thread">The thread to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, SeverityAndPurpose severityAndPurpose, Thread thread,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severityAndPurpose, thread, out _, file, line);

   /// <summary>Logs an entry with the given <paramref name="severityAndPurpose"/> and the given <paramref name="assembly"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severityAndPurpose">The severity and purpose of the given <paramref name="assembly"/>.</param>
   /// <param name="assembly">The assembly to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, SeverityAndPurpose severityAndPurpose, Assembly assembly,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severityAndPurpose, assembly, out _, file, line);

   /// <summary>Logs an entry with the given <paramref name="severityAndPurpose"/> and the given additional <paramref name="filePath"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severityAndPurpose">The severity and purpose of the given <paramref name="filePath"/>.</param>
   /// <param name="filePath">The path of the file that should be included.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger LogAdditionalPath(this ILogger logger, SeverityAndPurpose severityAndPurpose, string filePath,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.LogAdditionalPath(severityAndPurpose, filePath, out _, file, line);

   /// <summary>Starts building an entry, provides an <see cref="ILogEntryBuilder"/> that can be used to customise the entry.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severityAndPurpose">The severity and purpose of the entry that will be logged.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>An instance of <see cref="ILogEntryBuilder"/> that can be used to customise the entry.</returns>
   /// <remarks><see cref="ILogEntryBuilder.FinishEntry"/> must be called to actually log the entry.</remarks>
   public static ILogEntryBuilder StartEntry(this ILogger logger, SeverityAndPurpose severityAndPurpose,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.StartEntry(severityAndPurpose, out _, file, line);
   #endregion
}
