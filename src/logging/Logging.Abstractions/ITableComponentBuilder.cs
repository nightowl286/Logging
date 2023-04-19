using System;
using TNO.Logging.Common.Abstractions.Entries.Components;

namespace TNO.Logging.Abstractions;

/// <summary>
/// Denotes a builder for a <see cref="ITableComponent"/> data.
/// </summary>
public interface ITableComponentBuilder<out T>
{
   #region Methods
   #region Numeric
   #region Unsigned
   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="byte"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="byte"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, byte? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="ushort"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="ushort"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, ushort? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="uint"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="uint"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, uint? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="ulong"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="ulong"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, ulong? value);
   #endregion
   #region Signed
   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="sbyte"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="sbyte"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, sbyte? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="short"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="short"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, short? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="int"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="int"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, int? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="long"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="long"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, long? value);
   #endregion
   #region Floating
   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="float"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="float"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, float? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="double"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="double"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, double? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="decimal"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="decimal"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, decimal? value);
   #endregion
   #endregion

   #region Date/Time/TimeZone
   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="TimeSpan"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="TimeSpan"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, TimeSpan? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="DateTime"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="DateTime"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, DateTime? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="DateTimeOffset"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="DateTimeOffset"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, DateTimeOffset? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="TimeZoneInfo"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="TimeZoneInfo"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, TimeZoneInfo? value);
   #endregion

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="bool"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="bool"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, bool? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="char"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="char"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, char? value);

   /// <summary>
   /// Adds the given <paramref name="key"/> and associates it with
   /// the given <see cref="string"/> <paramref name="value"/>.
   /// </summary>
   /// <param name="key">The key to associate the given <paramref name="value"/> with.</param>
   /// <param name="value">The <see cref="string"/> value to add.</param>
   /// <returns>The table builder that was used.</returns>
   /// <remarks><see cref="BuildTable"/> must be called in order to finish the table.</remarks>
   ITableComponentBuilder<T> With(string key, string? value);

   /// <summary>Finishes creating the table.</summary>
   T BuildTable();
   #endregion
}
