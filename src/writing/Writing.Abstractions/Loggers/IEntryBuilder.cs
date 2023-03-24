﻿using System.Diagnostics;
using System.Reflection;
using System.Threading;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;

namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
/// Denotes a builder for log entries.
/// </summary>
public interface IEntryBuilder
{
   #region Methods
   /// <summary>Adds the given <paramref name="message"/> as an <see cref="IMessageComponent"/>.</summary>
   /// <param name="message">The message to add.</param>
   /// <returns>The builder that was used.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called in order to actually save the entry.</remarks>
   IEntryBuilder With(string message);

   /// <summary>Adds the given <paramref name="tag"/> as an <see cref="ITagComponent"/>.</summary>
   /// <param name="tag">The tag to add.</param>
   /// <returns>The builder that was used.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called in order to actually save the entry.</remarks>
   IEntryBuilder WithTag(string tag);

   /// <summary>Adds the given <paramref name="entryIdToLink"/> as an <see cref="IEntryLinkComponent"/>.</summary>
   /// <param name="entryIdToLink">The id of the entry to link to.</param>
   /// <returns>The builder that was used.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called in order to actually save the entry.</remarks>
   IEntryBuilder With(ulong entryIdToLink);

   /// <summary>Adds the given <paramref name="thread"/> as an <see cref="IThreadComponent"/>.</summary>
   /// <param name="thread">The thread to add.</param>
   /// <returns>The builder that was used.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called in order to actually save the entry.</remarks>
   IEntryBuilder With(Thread thread);

   /// <summary>
   /// Adds the given <paramref name="assembly"/> as an <see cref="IAssemblyComponent"/>.
   /// The assembly info will be saved as <see cref="IAssemblyInfo"/>.
   /// </summary>
   /// <param name="assembly">The assembly to add.</param>
   /// <returns>The builder that was used.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called in order to actually save the entry.</remarks>
   IEntryBuilder With(Assembly assembly);

   /// <summary>Starts creating a table that will be added as an <see cref="ITableComponent"/>.</summary>
   /// <returns>The table component builder that can be used to customise the table.</returns>
   ITableComponentBuilder<IEntryBuilder> WithTable();

   /// <summary>
   /// Adds the given <paramref name="stackTrace"/> and the <paramref name="threadId"/>
   /// as an <see cref="ISimpleStackTraceComponent"/>.
   /// </summary>
   /// <param name="stackTrace">The stack trace to add.</param>
   /// <param name="threadId">
   /// The <see cref="Thread.ManagedThreadId"/> of the thread 
   /// that the <paramref name="stackTrace"/> is from.
   /// 
   /// If <see langword="null"/> is used, then the id of the
   /// <see cref="Thread.CurrentThread"/> will be used instead.
   /// </param>
   /// <returns>The builder that was used.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called in order to actually save the entry.</remarks>
   IEntryBuilder WithSimple(StackTrace stackTrace, int? threadId = null);

   /// <summary>Builds the entry and logs it.</summary>
   /// <returns>The logger that was used.</returns>
   ILogger FinishEntry();
   #endregion
}
