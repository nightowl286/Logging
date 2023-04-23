using TNO.Logging.Abstractions;

namespace TNO.Logging.Logging.Void;

/// <summary>
/// Represents an <see cref="ITableComponentBuilder{T}"/> that will void any data it gets.
/// </summary>
/// <typeparam name="T">The type of the owner.</typeparam>
public sealed class VoidTableComponentBuilder<T> : ITableComponentBuilder<T>
{
   #region Fields
   private readonly T _owner;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="VoidTableComponentBuilder{T}"/>.</summary>
   /// <param name="owner">The owner of this builder.</param>
   public VoidTableComponentBuilder(T owner) => _owner = owner;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, byte? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, ushort? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, uint? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, ulong? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, sbyte? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, short? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, int? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, long? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, float? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, double? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, decimal? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, TimeSpan? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, DateTime? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, DateTimeOffset? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, TimeZoneInfo? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, bool? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, char? value) => this;

   /// <inheritdoc/>
   public ITableComponentBuilder<T> With(string key, string? value) => this;

   /// <inheritdoc/>
   public T BuildTable() => _owner;
   #endregion
}
