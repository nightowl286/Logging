using TNO.Logging.Abstractions;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;

namespace TNO.Logging.Logging;

/// <summary>
/// Denotes a builder for <see cref="ITableComponent"/> data.
/// </summary>
internal sealed class TableComponentBuilder<T> : ITableComponentBuilder<T>
{
   #region Fields
   private readonly T _caller;
   private readonly Dictionary<uint, object?> _table = new Dictionary<uint, object?>();
   private readonly ILogWriteContext _writeContext;
   private readonly ILogDataCollector _collector;
   private readonly Action<ITableComponent> _callback;
   #endregion

   #region Constructors
   public TableComponentBuilder(T caller, ILogWriteContext writeContext, ILogDataCollector collector, Action<ITableComponent> callback)
   {
      _caller = caller;
      _writeContext = writeContext;
      _collector = collector;
      _callback = callback;
   }
   #endregion

   #region Methods
   #region Numeric
   #region Unsigned
   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, byte? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, ushort? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, uint? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, ulong? value) => Add(key, value);
   #endregion
   #region Signed
   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, sbyte? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, short? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, int? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, long? value) => Add(key, value);
   #endregion
   #region Floating
   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, float? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, double? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, decimal? value) => Add(key, value);
   #endregion
   #endregion

   #region Date/Time/TimeZone
   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, TimeSpan? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, DateTime? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, DateTimeOffset? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, TimeZoneInfo? value) => Add(key, value);
   #endregion

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, bool? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, char? value) => Add(key, value);

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, string? value) => Add(key, value);

   /// <inheritdoc/>
   public T BuildTable()
   {
      TableInfo tableInfo = new TableInfo(_table);
      TableComponent component = new TableComponent(tableInfo);
      _callback.Invoke(component);

      return _caller;
   }
   #endregion

   #region Helpers
   private ITableComponentBuilder<T> Add<U>(string key, U? value)
   {
      if (_writeContext.GetOrCreateTableKeyId(key, out uint keyId))
      {
         TableKeyReference reference = new TableKeyReference(key, keyId);
         _collector.Deposit(reference);
      }

      _table.Add(keyId, value);

      return this;
   }
   #endregion
}
