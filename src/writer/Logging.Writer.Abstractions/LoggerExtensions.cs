using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace TNO.Logging.Writer.Abstractions;

/// <summary>
/// Contains useful extension methods for <see cref="ILogger"/> implementations.
/// </summary>
public static class LoggerExtensions
{
   #region Methods
   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="message"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severity">The severity of the given <paramref name="message"/>.</param>
   /// <param name="message">The message to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, Severity severity, string message,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severity, message, out _, file, line);

   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="stackFrame"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severity">The severity of the given <paramref name="stackFrame"/>.</param>
   /// <param name="stackFrame">The stack frame to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, Severity severity, StackFrame stackFrame,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severity, stackFrame, out _, file, line);

   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="stackTrace"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severity">The severity of the given <paramref name="stackTrace"/>.</param>
   /// <param name="stackTrace">The stack trace to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, Severity severity, StackTrace stackTrace,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severity, stackTrace, out _, file, line);

   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="exception"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severity">The severity of the given <paramref name="exception"/>.</param>
   /// <param name="exception">The exception to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, Severity severity, Exception exception,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severity, exception, out _, file, line);

   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="thread"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severity">The severity of the given <paramref name="thread"/>.</param>
   /// <param name="thread">The thread to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, Severity severity, Thread thread,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severity, thread, out _, file, line);

   /// <summary>Logs an entry with the given <paramref name="severity"/> and the given <paramref name="assembly"/>.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severity">The severity of the given <paramref name="assembly"/>.</param>
   /// <param name="assembly">The assembly to log.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>The logger instance.</returns>
   public static ILogger Log(this ILogger logger, Severity severity, Assembly assembly,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.Log(severity, assembly, out _, file, line);

   /// <summary>Starts building an entry, provides an <see cref="ILogEntryBuilder"/> that can be used to customise the entry.</summary>
   /// <param name="logger">The <see cref="ILogger"/> instance to use.</param>
   /// <param name="severity">The severity of the entry that will be logged.</param>
   /// <param name="file">The file from which this method was called. This should be provided by the compiler.</param>
   /// <param name="line">The line in the file from which this method was called. This should be provided by the compiler.</param>
   /// <returns>An instance of <see cref="ILogEntryBuilder"/> that can be used to customise the entry.</returns>
   /// <remarks><see cref="ILogEntryBuilder.FinishEntry"/> must be called to actually log the entry.</remarks>
   public static ILogEntryBuilder StartEntry(this ILogger logger, Severity severity,
      [CallerFilePath] string file = "", [CallerLineNumber] int line = 0) => logger.StartEntry(severity, out _, file, line);
   #endregion
}
