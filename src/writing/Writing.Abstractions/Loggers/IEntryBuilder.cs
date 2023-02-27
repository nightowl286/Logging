using System.Threading;
using TNO.Logging.Common.Abstractions.Entries.Components;

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

   /// <summary>Starts creating a table that will be added as an <see cref="ITableComponent"/>.</summary>
   /// <returns>The table component builder that can be used to customise the table.</returns>
   ITableComponentBuilder<IEntryBuilder> WithTable();

   /// <summary>Builds the entry and logs it.</summary>
   /// <returns>The logger that was used.</returns>
   ILogger FinishEntry();
   #endregion
}
