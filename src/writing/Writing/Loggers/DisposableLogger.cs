using TNO.Common.Extensions;
using TNO.Logging.Writing.Abstractions.Loggers;

namespace TNO.Logging.Writing.Loggers;

/// <summary>
/// Represents a logger decorator that can be used to modify what will be disposed.
/// </summary>
public sealed class DisposableLogger : IDisposableLogger
{
   #region Fields
   private readonly IDisposable _toDispose;
   private readonly ILogger _logger;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="DisposableLogger"/>.</summary>
   /// <param name="logger">The logger to decorate.</param>
   /// <param name="toDispose">The disposable object.</param>
   public DisposableLogger(ILogger logger, IDisposable toDispose)
   {
      _logger = logger;
      _toDispose = toDispose;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ILogger Log(string message) => _logger.Log(message);

   /// <inheritdoc/>
   /// <remarks>
   /// This will also try to dispose the given logger first.
   /// <see cref="ObjectExtensions.TryDispose(object)"/> for more info.
   /// </remarks>
   public void Dispose()
   {
      _logger.TryDispose();
      _toDispose.Dispose();
   }
   #endregion
}
