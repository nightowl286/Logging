using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Loggers.Scopes;
using TNO.Logging.Writing.Abstractions.Writers;
using TNO.Logging.Writing.Writers;

namespace TNO.Logging.Writing.Builders;

/// <summary>
/// Contains useful extension methods related to the <see cref="ILoggerBuilder"/>.
/// </summary>
public static class LoggerBuilderExtensions
{
   #region Methods
   /// <summary>Includes a file system log <paramref name="writer"/>.</summary>
   /// <param name="builder">The builder to include the <paramref name="writer"/> in.</param>
   /// <param name="directory">The directory in which the log should be saved.</param>
   /// <param name="writer">The created file system log writer.</param>
   /// <returns>The given <paramref name="builder"/> instance.</returns>
   /// <remarks>It is the caller's responsibility to ensure that the <paramref name="writer"/> is disposed correctly.</remarks>
   public static ILoggerBuilder WithFileSystem(this ILoggerBuilder builder, string directory, out IFileSystemLogWriter writer)
   {
      Directory.CreateDirectory(directory);

      writer = new FileSystemLogWriter(builder.Facade, directory);

      builder.With(writer);

      return builder;
   }

   /// <inheritdoc cref="ILoggerBuilder.Build(out ILogDataDistributor)"/>
   /// <param name="builder">The builder to use.</param>
   public static IContextLogger Build(this ILoggerBuilder builder)
      => builder.Build(out _);
   #endregion
}
