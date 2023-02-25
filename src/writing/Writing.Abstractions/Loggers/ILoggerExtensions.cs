using System.Runtime.CompilerServices;
using System.Threading;
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
   {
      logger.Log(importance, message, out _, file, line);
      return logger;
   }

   /// <inheritdoc cref="ILogger.LogTag(Importance, string, out ulong, string, uint)"/>
   public static ILogger LogTag(this ILogger logger, Importance importance, string tag,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      logger.LogTag(importance, tag, out _, file, line);
      return logger;
   }

   /// <inheritdoc cref="ILogger.Log(Importance, Thread, out ulong, string, uint)"/>
   public static ILogger Log(this ILogger logger, Importance importance, Thread thread,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      logger.Log(importance, thread, out _, file, line);
      return logger;
   }

   /// <summary>
   /// Write the <see cref="Thread.CurrentThread"/> to the log.
   /// </summary>
   /// <inheritdoc cref="ILogger.Log(Importance, Thread, out ulong, string, uint)"/>
   public static ILogger LogCurrentThread(this ILogger logger, Importance importance,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      logger.Log(importance, Thread.CurrentThread, out _, file, line);
      return logger;
   }

   /// <inheritdoc cref="ILogger.StartEntry(Importance, out ulong, string, uint)"/>
   public static IEntryBuilder StartEntry(this ILogger logger, Importance importance,
     [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
      => logger.StartEntry(importance, out _, file, line);
   #endregion
}
