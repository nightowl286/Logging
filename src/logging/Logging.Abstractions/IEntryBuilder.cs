﻿using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Types;

namespace TNO.Logging.Abstractions;

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

   /// <summary>
   /// Adds the given <paramref name="type"/> as an <see cref="ITypeComponent"/>.
   /// The assembly info will be saved as <see cref="ITypeInfo"/>.
   /// </summary>
   /// <param name="type">The type to add.</param>
   /// <returns>The builder that was used.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called in order to actually save the entry.</remarks>
   IEntryBuilder With(Type type);

   /// <summary>
   /// Adds the given <paramref name="exception"/> and the <paramref name="threadId"/>
   /// as an <see cref="IExceptionComponent"/>.
   /// </summary>
   /// <param name="exception">The exception to add.</param>
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
   /// <returns>The builder that was used.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called in order to actually save the entry.</remarks>
   IEntryBuilder With(Exception exception, int? threadId = null);

   /// <summary>Starts creating a table that will be added as an <see cref="ITableComponent"/>.</summary>
   /// <returns>The table component builder that can be used to customise the table.</returns>
   ITableComponentBuilder<IEntryBuilder> WithTable();

   /// <summary>
   /// Adds the given <paramref name="stackTrace"/> and the <paramref name="threadId"/>
   /// as an <see cref="IStackTraceComponent"/>.
   /// </summary>
   /// <param name="stackTrace">The stack trace to add.</param>
   /// <param name="threadId">
   /// <para>
   ///   The <see cref="Thread.ManagedThreadId"/> of the thread
   ///   that the <paramref name="stackTrace"/> is from.
   /// </para>
   /// <para>
   ///   If <see langword="null"/> is used, then a negative
   ///   id will be used to indicate that it is unknown.
   /// </para>
   /// </param>
   /// <returns>The builder that was used.</returns>
   /// <remarks><see cref="FinishEntry"/> must be called in order to actually save the entry.</remarks>
   IEntryBuilder With(StackTrace stackTrace, int? threadId = null);

   /// <summary>Builds the entry and logs it.</summary>
   /// <returns>The logger that was used.</returns>
   ILogger FinishEntry();
   #endregion
}
