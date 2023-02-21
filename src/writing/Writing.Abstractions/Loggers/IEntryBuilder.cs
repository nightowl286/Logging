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

   /// <summary>Builds the entry and logs it.</summary>
   /// <returns>The logger that was used.</returns>
   ILogger FinishEntry();
   #endregion
}
