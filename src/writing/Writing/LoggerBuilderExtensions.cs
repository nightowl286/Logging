using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Loggers;
using TNO.Logging.Writing.Loggers.Writers;

namespace TNO.Logging.Writing;

/// <summary>
/// Contains useful extension methods related to the <see cref="ILoggerBuilder"/>.
/// </summary>
public static class LoggerBuilderExtensions
{
   #region Methods
   /// <summary>Includes a file system log <paramref name="writer"/>.</summary>
   /// <param name="builder">The builder to include the <paramref name="writer"/> in.</param>
   /// <param name="directory">The directory in which the log should be saved.</param>
   /// <param name="writer">The writer that can be used for disposing/closing reasons.</param>
   /// <returns>The given <paramref name="builder"/> instance.</returns>
   /// <remarks>It is the caller's responsibility to ensure that the <paramref name="writer"/> is disposed correctly.</remarks>
   public static ILoggerBuilder WithFileSystem(this ILoggerBuilder builder, string directory, out IDisposable writer)
   {
      Directory.CreateDirectory(directory);

      FileSystemLogWriter fsWriter = new FileSystemLogWriter(builder.Facade, directory);

      builder.With(fsWriter);
      writer = fsWriter;

      return builder;
   }

   /// <inheritdoc cref="ILoggerBuilder.Build(out ILogDataDistributor)"/>
   /// <param name="builder">The builder to use.</param>
   public static ILogger Build(this ILoggerBuilder builder)
      => builder.Build(out _);
   #endregion
}
