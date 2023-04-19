using System.Runtime.CompilerServices;

namespace TNO.Logging.Abstractions.Scopes;

/// <summary>
/// Denotes a creator for logger contexts.
/// </summary>
public interface ILoggerContextCreator
{
   #region Methods
   /// <summary>Creates a new context with the given <paramref name="name"/>.</summary>
   /// <param name="name">The name to give to the context.</param>
   /// <param name="file">
   /// The file from which this method was called. 
   /// This should be provided by the compiler.
   /// </param>
   /// <param name="line">
   /// The line number in the <paramref name="file"/> where this method was called from.
   /// This should be provided by the compiler.
   /// </param>
   /// <returns>A new logger bound to the newly created scope.</returns>
   IContextLogger CreateContext(string name,
      [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0);
   #endregion
}
