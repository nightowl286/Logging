using System.Runtime.CompilerServices;
using TNO.Logging.Common.Abstractions.Entries;

namespace TNO.Logging.Writing.Abstractions.Loggers;

/// <summary>
/// Contains useful extension methods related to the <see cref="ILogger"/>.
/// </summary>
public static class ILoggerExtensions
{
   #region Methods
   /// <inheritdoc cref="ILogger.Log(Importance, string, out ulong, string, uint)"/>
   public static ILogger Log(this ILogger logger, Importance importance, string message,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
      => logger.Log(importance, message, out _, file, line);
   #endregion
}
