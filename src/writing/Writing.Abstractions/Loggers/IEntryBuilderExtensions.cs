using System.Threading;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
/// Contains useful extension methods related to the <see cref="IEntryBuilder"/>.
/// </summary>
public static class IEntryBuilderExtensions
{
   #region Methods
   /// <summary>Adds the <see cref="Thread.CurrentThread"/> as an <see cref="IThreadComponent"/>.</summary>
   /// <inheritdoc cref="IEntryBuilder.With(Thread)"/>
   public static IEntryBuilder WithCurrentThread(this IEntryBuilder builder)
   {
      builder.With(Thread.CurrentThread);
      return builder;
   }
   #endregion
}
