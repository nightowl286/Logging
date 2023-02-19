using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Writers;
using TNO.Logging.Writing.Writers;

namespace TNO.Logging.Writing.Builders;

/// <summary>
/// Contains useful extension methods related to the <see cref="ILoggerConfigurator"/>.
/// </summary>
public static class LoggerConfiguratorExtensions
{
   #region Methods
   /// <summary>Includes a file system log <paramref name="writer"/>.</summary>
   /// <param name="configurator">The configurator to include the <paramref name="writer"/> in.</param>
   /// <param name="directory">The directory in which the log should be saved.</param>
   /// <param name="writer">The created file system log writer.</param>
   /// <returns>The given <paramref name="configurator"/> instance.</returns>
   /// <remarks>It is the caller's responsibility to ensure that the <paramref name="writer"/> is disposed correctly.</remarks>
   public static ILoggerConfigurator WithFileSystem(this ILoggerConfigurator configurator, string directory, out IFileSystemLogWriter writer)
      => WithFileSystem(configurator, new FileSystemLogWriterSettings(directory), out writer);

   /// <summary>Includes a file system log <paramref name="writer"/>.</summary>
   /// <param name="configurator">The configurator to include the <paramref name="writer"/> in.</param>
   /// <param name="settings">The settings to use when creating the writer..</param>
   /// <param name="writer">The created file system log writer.</param>
   /// <returns>The given <paramref name="configurator"/> instance.</returns>
   /// <remarks>It is the caller's responsibility to ensure that the <paramref name="writer"/> is disposed correctly.</remarks>
   public static ILoggerConfigurator WithFileSystem(this ILoggerConfigurator configurator, FileSystemLogWriterSettings settings, out IFileSystemLogWriter writer)
   {
      Directory.CreateDirectory(settings.LogPath);

      writer = new FileSystemLogWriter(configurator.Facade, settings);

      configurator.With(writer);

      return configurator;
   }
   #endregion
}
