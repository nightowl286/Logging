using TNO.Logging.Reading.Abstractions;
using TNO.Logging.Reading.Abstractions.Readers;
using TNO.Logging.Reading.Readers;

namespace TNO.Logging.Reading.Builders;

/// <summary>
/// Contains useful extension methods related to the <see cref="ILogReaderConfigurator"/>.
/// </summary>
public static class LogReaderConfiguratorExtensions
{
   #region Methods
   /// <summary>
   /// Creates a file system log reader using the given <paramref name="logReaderConfigurator"/>.
   /// </summary>
   /// <param name="logReaderConfigurator">The log reader configurator to use.</param>
   /// <param name="path">The path to the log that should be read.</param>
   /// <returns>The newly created <see cref="IFileSystemLogReader"/>.</returns>
   public static IFileSystemLogReader FromFileSystem(this ILogReaderConfigurator logReaderConfigurator, string path)
   {
      FileSystemLogReader reader = new FileSystemLogReader(path, logReaderConfigurator.GetScope());
      return reader;
   }
   #endregion
}
