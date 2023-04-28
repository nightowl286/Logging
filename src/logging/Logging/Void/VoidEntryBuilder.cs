using System.Diagnostics;
using System.Reflection;
using TNO.Logging.Abstractions;

namespace TNO.Logging.Logging.Void;

/// <summary>
/// Represents an <see cref="IEntryBuilder"/> that will void any data it gets.
/// </summary>
public sealed class VoidEntryBuilder : IEntryBuilder
{
   #region Fields
   private static readonly VoidTableComponentBuilder<VoidEntryBuilder> TableBuilder;
   #endregion

   #region Properties
   /// <summary>A singleton instance of the <see cref="VoidEntryBuilder"/>.</summary>
   public static VoidEntryBuilder Instance { get; private set; }
   #endregion

   #region Constructors
   private VoidEntryBuilder() { }

   static VoidEntryBuilder()
   {
      Instance = new VoidEntryBuilder();
      TableBuilder = new VoidTableComponentBuilder<VoidEntryBuilder>(Instance);
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IEntryBuilder With(string message) => this;

   /// <inheritdoc/>
   public IEntryBuilder With(ulong entryIdToLink) => this;

   /// <inheritdoc/>
   public IEntryBuilder With(Thread thread) => this;

   /// <inheritdoc/>
   public IEntryBuilder With(Assembly assembly) => this;

   /// <inheritdoc/>
   public IEntryBuilder With(Type type) => this;

   /// <inheritdoc/>
   public IEntryBuilder With(StackTrace stackTrace, int? threadId = null) => this;

   /// <inheritdoc/>
   public IEntryBuilder WithTag(string tag) => this;

   /// <inheritdoc/>
   public IEntryBuilder With(Exception exception, int? threadId = null) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<IEntryBuilder> WithTable() => TableBuilder;

   /// <inheritdoc/>
   public ILogger FinishEntry() => VoidLogger.Instance;
   #endregion
}
