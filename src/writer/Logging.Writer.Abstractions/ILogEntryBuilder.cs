using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace TNO.Logging.Writer.Abstractions;

/// <summary>
/// Denotes a builder for custom log entries.
/// </summary>
public interface ILogEntryBuilder
{
   #region Methods
   /// <summary>Adds a message component to the entry.</summary>
   /// <param name="message">The message to include.</param>
   /// <returns>The builder instance.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called to actually log the entry.</remarks>
   ILogEntryBuilder With(string message);

   /// <summary>Adds a <see cref="StackFrame"/> component to the entry.</summary>
   /// <param name="stackFrame">The stack frame to include.</param>
   /// <returns>The builder instance.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called to actually log the entry.</remarks>
   ILogEntryBuilder With(StackFrame stackFrame);

   /// <summary>Adds a <see cref="StackTrace"/> component to the entry.</summary>
   /// <param name="stackTrace">The stack trace to include.</param>
   /// <returns>The builder instance.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called to actually log the entry.</remarks>
   ILogEntryBuilder With(StackTrace stackTrace);

   /// <summary>Adds an <see cref="Exception"/> component to the entry.</summary>
   /// <param name="exception">The exception to include.</param>
   /// <returns>The builder instance.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called to actually log the entry.</remarks>
   ILogEntryBuilder With(Exception exception);

   /// <summary>Adds a <see cref="Thread"/> component to the entry.</summary>
   /// <param name="thread">The thread to include.</param>
   /// <returns>The builder instance.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called to actually log the entry.</remarks>
   ILogEntryBuilder With(Thread thread);

   /// <summary>Adds an <see cref="Assembly"/> component to the entry.</summary>
   /// <param name="assembly">The assembly to include.</param>
   /// <returns>The builder instance.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called to actually log the entry.</remarks>
   ILogEntryBuilder With(Assembly assembly);

   /// <summary>Adds a link to the given <paramref name="entryIdToLink"/>.</summary>
   /// <param name="entryIdToLink">The id of the entry to link.</param>
   /// <returns>The builder instance.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called to actually log the entry.</remarks>
   ILogEntryBuilder With(ulong entryIdToLink);

   /// <summary>Adds a <paramref name="tag"/> component to the entry.</summary>
   /// <param name="tag">The tag to attach. This can be used to easily search for entries afterwards.</param>
   /// <returns>The builder instance.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called to actually log the entry.</remarks>
   ILogEntryBuilder WithTag(string tag);

   /// <summary>Logs the customised entry.</summary>
   /// <returns>The logger instance that created this builder.</returns>
   ILogger FinishEntry();
   #endregion
}
