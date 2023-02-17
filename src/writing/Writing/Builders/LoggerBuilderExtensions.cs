using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Loggers;
using TNO.Logging.Writing.Loggers.Writers;

namespace TNO.Logging.Writing.Builders;

/// <summary>
/// Contains useful extension methods related to the <see cref="ILoggerBuilder"/>.
/// </summary>
public static class LoggerBuilderExtensions
{
   #region Methods
   /// <summary>Includes a file system log <paramref name="logger"/>.</summary>
   /// <param name="builder">The builder to include the <paramref name="logger"/> in.</param>
   /// <param name="directory">The directory in which the log should be saved.</param>
   /// <param name="logger">The writer that can be used for disposing/closing reasons.</param>
   /// <returns>The given <paramref name="builder"/> instance.</returns>
   /// <remarks>It is the caller's responsibility to ensure that the <paramref name="logger"/> is disposed correctly.</remarks>
   public static ILoggerBuilder WithFileSystem(this ILoggerBuilder builder, string directory, out IFileSystemLogger logger)
   {
      Directory.CreateDirectory(directory);

      FileSystemLogWriter fsWriter = new FileSystemLogWriter(builder.Facade, directory);
      logger = new FileSystemLoggerWrapper(builder.Logger, fsWriter, directory);

      builder.With(fsWriter);

      return builder;
   }

   /// <inheritdoc cref="ILoggerBuilder.Build(out ILogDataDistributor)"/>
   /// <param name="builder">The builder to use.</param>
   public static ILogger Build(this ILoggerBuilder builder)
      => builder.Build(out _);
   #endregion
}
