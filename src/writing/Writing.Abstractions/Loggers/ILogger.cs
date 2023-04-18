using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Entries.Importance;

namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
/// Denotes a logger.
/// </summary>
public interface ILogger
{
   #region Methods
   /// <summary>Writes the given <paramref name="message"/> to the log.</summary>
   /// <param name="importance">
   /// The severity and purpose of the entry that will be created.
   /// This value should be normalised.
   /// </param>
   /// <param name="message">The message to write.</param>
   /// <param name="entryId">The id of the entry that was created.</param>
   /// <param name="file">
   /// The file from which this method was called. 
   /// This should be provided by the compiler.
   /// </param>
   /// <param name="line">
   /// The line number in the <paramref name="file"/> where this method was called from.
   /// This should be provided by the compiler.
   /// </param>
   /// <returns>The logger that was used.</returns>
   ILogger Log(ImportanceCombination importance, string message, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0);

   /// <summary>Writes the given <paramref name="tag"/> to the log.</summary>
   /// <param name="importance">
   /// The severity and purpose of the entry that will be created.
   /// This value should be normalised.
   /// </param>
   /// <param name="tag">The tag to write.</param>
   /// <param name="entryId">The id of the entry that was created.</param>
   /// <param name="file">
   /// The file from which this method was called. 
   /// This should be provided by the compiler.
   /// </param>
   /// <param name="line">
   /// The line number in the <paramref name="file"/> where this method was called from.
   /// This should be provided by the compiler.
   /// </param>
   /// <returns>The logger that was used.</returns>
   ILogger LogTag(ImportanceCombination importance, string tag, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0);

   /// <summary>Writes the given <paramref name="thread"/> to the log.</summary>
   /// <param name="importance">
   /// The severity and purpose of the entry that will be created.
   /// This value should be normalised.
   /// </param>
   /// <param name="thread">The thread to write.</param>
   /// <param name="entryId">The id of the entry that was created.</param>
   /// <param name="file">
   /// The file from which this method was called. 
   /// This should be provided by the compiler.
   /// </param>
   /// <param name="line">
   /// The line number in the <paramref name="file"/> where this method was called from.
   /// This should be provided by the compiler.
   /// </param>
   /// <returns>The logger that was used.</returns>
   ILogger Log(ImportanceCombination importance, Thread thread, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0);

   /// <summary>Writes the given <paramref name="assembly"/> to the log.</summary>
   /// <param name="importance">
   /// The severity and purpose of the entry that will be created.
   /// This value should be normalised.
   /// </param>
   /// <param name="assembly">The assembly to write.</param>
   /// <param name="entryId">The id of the entry that was created.</param>
   /// <param name="file">
   /// The file from which this method was called. 
   /// This should be provided by the compiler.
   /// </param>
   /// <param name="line">
   /// The line number in the <paramref name="file"/> where this method was called from.
   /// This should be provided by the compiler.
   /// </param>
   /// <returns>The logger that was used.</returns>
   ILogger Log(ImportanceCombination importance, Assembly assembly, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0);

   /// <summary>Writes the given <paramref name="stackTrace"/> and the <paramref name="threadId"/> to the log.</summary>
   /// <param name="importance">
   /// The severity and purpose of the entry that will be created.
   /// This value should be normalised.
   /// </param>
   /// <param name="stackTrace">The <see cref="StackTrace"/> to write.</param>
   /// <param name="threadId">
   ///<para>
   ///   The <see cref="Thread.ManagedThreadId"/> of the thread
   ///   that the <paramref name="stackTrace"/> is from.
   /// </para>
   /// <para>
   ///   If <see langword="null"/> is used, then a negative
   ///   id will be used to indicate that it is unknown.
   /// </para>
   /// </param>
   /// <param name="entryId">The id of the entry that was created.</param>
   /// <param name="file">
   /// The file from which this method was called. 
   /// This should be provided by the compiler.
   /// </param>
   /// <param name="line">
   /// The line number in the <paramref name="file"/> where this method was called from.
   /// This should be provided by the compiler.
   /// </param>
   /// <returns>The logger that was used.</returns>
   ILogger Log(ImportanceCombination importance, StackTrace stackTrace, int? threadId, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0);

   /// <summary>Writes the given <paramref name="type"/> to the log.</summary>
   /// <param name = "importance" >
   /// The severity and purpose of the entry that will be created.
   /// This value should be normalised.
   /// </param>
   /// <param name="type">The type to write.</param>
   /// <param name="entryId">The id of the entry that was created.</param>
   /// <param name="file">
   /// The file from which this method was called. 
   /// This should be provided by the compiler.
   /// </param>
   /// <param name="line">
   /// The line number in the <paramref name="file"/> where this method was called from.
   /// This should be provided by the compiler.
   /// </param>
   /// <returns>The logger that was used.</returns>
   ILogger Log(ImportanceCombination importance, Type type, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0);

   /// <summary>Writes the given <paramref name="exception"/> and the <paramref name="threadId"/> to the log.</summary>
   /// <param name="importance">
   /// The severity and purpose of the entry that will be created.
   /// This value should be normalised.
   /// </param>
   /// <param name="exception">The <see cref="Exception"/> to write.</param>
   /// <param name="threadId">
   /// <para>
   ///   The <see cref="Thread.ManagedThreadId"/> of the thread
   ///   that the <paramref name="exception"/> is from.
   /// </para>
   /// <para>
   ///   If <see langword="null"/> is used, then a negative
   ///   id will be used to indicate that it is unknown.
   /// </para>
   /// </param>
   /// <param name="entryId">The id of the entry that was created.</param>
   /// <param name="file">
   /// The file from which this method was called. 
   /// This should be provided by the compiler.
   /// </param>
   /// <param name="line">
   /// The line number in the <paramref name="file"/> where this method was called from.
   /// This should be provided by the compiler.
   /// </param>
   /// <returns>The logger that was used.</returns>
   ILogger Log(ImportanceCombination importance, Exception exception, int? threadId, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0);

   /// <summary>Starts building an entry that will have multiple components.</summary>
   /// <param name="importance">
   /// The severity and purpose of the entry that will be created.
   /// This value should be normalised.
   /// </param>
   /// <param name="entryId">The id of the entry that was created.</param>
   /// <param name="file">
   /// The file from which this method was called. 
   /// This should be provided by the compiler.
   /// </param>
   /// <param name="line">
   /// The line number in the <paramref name="file"/> where this method was called from.
   /// This should be provided by the compiler.
   /// </param>
   /// <returns>The logger that was used.</returns>
   IEntryBuilder StartEntry(ImportanceCombination importance, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0);

   /// <summary>Starts creating a table that will be added as an <see cref="ITableComponent"/>.</summary>
   /// <param name="importance">
   /// The severity and purpose of the entry that will be created.
   /// This value should be normalised.
   /// </param>
   /// <param name="entryId">The id of the entry that was created.</param>
   /// <param name="file">
   /// The file from which this method was called. 
   /// This should be provided by the compiler.
   /// </param>
   /// <param name="line">
   /// The line number in the <paramref name="file"/> where this method was called from.
   /// This should be provided by the compiler.
   /// </param>
   /// <returns>The table component builder that can be used to customise the table.</returns>
   ITableComponentBuilder<ILogger> StartTable(ImportanceCombination importance, out ulong entryId,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0);
   #endregion
}