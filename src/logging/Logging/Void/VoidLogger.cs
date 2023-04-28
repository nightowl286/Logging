using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using TNO.Logging.Abstractions;
using TNO.Logging.Abstractions.Scopes;
using TNO.Logging.Common.Abstractions.Entries.Importance;

namespace TNO.Logging.Void;

/// <summary>
/// Represents a <see cref="IContextLogger"/> that will void any data it gets.
/// </summary>
public sealed class VoidLogger : IContextLogger
{
   #region Fields
   private static readonly VoidTableComponentBuilder<VoidLogger> TableBuilder;
   #endregion

   #region Properties
   /// <summary>A singleton instance of the <see cref="VoidLogger"/>.</summary>
   public static VoidLogger Instance { get; }
   #endregion

   #region Constructors
   private VoidLogger() { }
   static VoidLogger()
   {
      Instance = new VoidLogger();
      TableBuilder = new VoidTableComponentBuilder<VoidLogger>(Instance);
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, string message, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = 0;
      return this;
   }

   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, Thread thread, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = 0;
      return this;
   }

   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, Assembly assembly, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = 0;
      return this;
   }

   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, StackTrace stackTrace, int? threadId, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = 0;
      return this;
   }

   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, Type type, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = 0;
      return this;
   }

   /// <inheritdoc/>
   public ILogger Log(ImportanceCombination importance, Exception exception, int? threadId, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = 0;
      return this;
   }

   /// <inheritdoc/>
   public ILogger LogTag(ImportanceCombination importance, string tag, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = 0;
      return this;
   }

   /// <inheritdoc/>
   public IEntryBuilder StartEntry(ImportanceCombination importance, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = 0;
      return VoidEntryBuilder.Instance;
   }

   /// <inheritdoc/>
   public ITableComponentBuilder<ILogger> StartTable(ImportanceCombination importance, out ulong entryId, [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0)
   {
      entryId = 0;
      return TableBuilder;
   }

   /// <inheritdoc/>
   public IContextLogger CreateContext(string name, [CallerFilePath] string file = "", [CallerLineNumber] uint line = 0) => this;

   /// <inheritdoc/>
   public ILogger CreateScoped() => this;
   #endregion
}
