using System.Collections;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;

namespace TNO.Logging.Logging.Helpers;

/// <summary>
/// Contains useful functions related to the <see cref="ITableInfo"/>.
/// </summary>
public static class TableInfoHelper
{
   #region Fields
   // Note(Nightowl): Copied from the TableInfoSerialiser.cs;
   private static readonly Dictionary<Type, TableDataKind> DataKinds = new Dictionary<Type, TableDataKind>()
   {
      { typeof(byte), TableDataKind.Byte },
      { typeof(sbyte), TableDataKind.SByte },
      { typeof(ushort), TableDataKind.UShort},
      { typeof(short), TableDataKind.Short },
      { typeof(uint), TableDataKind.UInt },
      { typeof(int), TableDataKind.Int },
      { typeof(ulong), TableDataKind.ULong },
      { typeof(long), TableDataKind.Long },

      { typeof(float), TableDataKind.Float },
      { typeof(double), TableDataKind.Double },
      { typeof(decimal), TableDataKind.Decimal },

      { typeof(char), TableDataKind.Char },
      { typeof(string), TableDataKind.String },
      { typeof(bool), TableDataKind.Bool },

      { typeof(TimeSpan), TableDataKind.TimeSpan },
      { typeof(DateTime), TableDataKind.DateTime },
      { typeof(DateTimeOffset), TableDataKind.DateTimeOffset },
      { typeof(TimeZoneInfo), TableDataKind.TimeZoneInfo },
   };

   private record class KeyValuePairAccessor(Func<object, string> GetKey, Func<object, object?> GetValue);
   private static readonly ReaderWriterLockSlim AccessorsLock = new ReaderWriterLockSlim();
   private static readonly ConditionalWeakTable<Type, KeyValuePairAccessor> KeyValuePairAccessors = new ConditionalWeakTable<Type, KeyValuePairAccessor>();
   #endregion

   #region Functions
   #region Table
   /// <summary>Gets the given <see cref="ITableInfo"/> for the given <paramref name="dictionary"/>.</summary>
   /// <typeparam name="TKey">The type of the keys in the given <paramref name="dictionary"/>.</typeparam>
   /// <typeparam name="TValue">The type of the values in the given <paramref name="dictionary"/>.</typeparam>
   /// <param name="writeContext">The write context to use to generate the needed ids.</param>
   /// <param name="dataCollector">The data collector to deposit new log data to.</param>
   /// <param name="dictionary">The <see cref="IDictionary{TKey, TValue}"/> to generate the <see cref="ITableInfo"/> for.</param>
   /// <returns>The generated <see cref="ITableInfo"/>.</returns>
   public static ITableInfo Convert<TKey, TValue>(ILogWriteContext writeContext, ILogDataCollector dataCollector, IDictionary<TKey, TValue> dictionary)
   {
      return ConvertDictionary(writeContext, dataCollector, dictionary);
   }

   /// <summary>Gets the given <see cref="ITableInfo"/> for the given <paramref name="dictionary"/>.</summary>
   /// <typeparam name="TKey">The type of the keys in the given <paramref name="dictionary"/>.</typeparam>
   /// <typeparam name="TValue">The type of the values in the given <paramref name="dictionary"/>.</typeparam>
   /// <param name="writeContext">The write context to use to generate the needed ids.</param>
   /// <param name="dataCollector">The data collector to deposit new log data to.</param>
   /// <param name="dictionary">The <see cref="IReadOnlyDictionary{TKey, TValue}"/> to generate the <see cref="ITableInfo"/> for.</param>
   /// <returns>The generated <see cref="ITableInfo"/>.</returns>
   public static ITableInfo Convert<TKey, TValue>(ILogWriteContext writeContext, ILogDataCollector dataCollector, IReadOnlyDictionary<TKey, TValue> dictionary)
   {
      return ConvertDictionary(writeContext, dataCollector, dictionary);
   }

   /// <summary>Gets the given <see cref="ITableInfo"/> for the given <paramref name="dictionary"/>.</summary>
   /// <param name="writeContext">The write context to use to generate the needed ids.</param>
   /// <param name="dataCollector">The data collector to deposit new log data to.</param>
   /// <param name="dictionary">The <see cref="IDictionary"/> to generate the <see cref="ITableInfo"/> for.</param>
   /// <returns>The generated <see cref="ITableInfo"/>.</returns>
   public static ITableInfo Convert(ILogWriteContext writeContext, ILogDataCollector dataCollector, IDictionary dictionary)
   {
      return ConvertDictionary(writeContext, dataCollector, dictionary);
   }
   #endregion

   #region Collection
   /// <summary>Gets the <see cref="ICollectionInfo"/> for the given <paramref name="collection"/>.</summary>
   /// <typeparam name="TValue">The type of the values in the given <paramref name="collection"/>.</typeparam>
   /// <param name="writeContext">The write context to use to generate the needed ids.</param>
   /// <param name="dataCollector">The data collector to deposit new log data to.</param>
   /// <param name="collection">The <see cref="ICollection{T}"/> to generate the <see cref="ICollectionInfo"/> for.</param>
   /// <returns>The generated <see cref="ICollectionInfo"/> for.</returns>
   public static ICollectionInfo Convert<TValue>(ILogWriteContext writeContext, ILogDataCollector dataCollector, ICollection<TValue> collection)
   {
      return ConvertCollection(writeContext, dataCollector, collection);
   }

   /// <summary>Gets the <see cref="ICollectionInfo"/> for the given <paramref name="collection"/>.</summary>
   /// <typeparam name="TValue">The type of the values in the given <paramref name="collection"/>.</typeparam>
   /// <param name="writeContext">The write context to use to generate the needed ids.</param>
   /// <param name="dataCollector">The data collector to deposit new log data to.</param>
   /// <param name="collection">The <see cref="IReadOnlyCollection{T}"/> to generate the <see cref="ICollectionInfo"/> for.</param>
   /// <returns>The generated <see cref="ICollectionInfo"/> for.</returns>
   public static ICollectionInfo Convert<TValue>(ILogWriteContext writeContext, ILogDataCollector dataCollector, IReadOnlyCollection<TValue> collection)
   {
      return ConvertCollection(writeContext, dataCollector, collection);
   }

   /// <summary>Gets the <see cref="ICollectionInfo"/> for the given <paramref name="collection"/>.</summary>
   /// <param name="writeContext">The write context to use to generate the needed ids.</param>
   /// <param name="dataCollector">The data collector to deposit new log data to.</param>
   /// <param name="collection">The <see cref="ICollection"/> to generate the <see cref="ICollectionInfo"/> for.</param>
   /// <returns>The generated <see cref="ICollectionInfo"/> for.</returns>
   public static ICollectionInfo Convert<TValue>(ILogWriteContext writeContext, ILogDataCollector dataCollector, ICollection collection)
   {
      return ConvertCollection(writeContext, dataCollector, collection);
   }
   #endregion

   private static ITableInfo ConvertDictionary(ILogWriteContext writeContext, ILogDataCollector dataCollector, IEnumerable enumerable)
   {
      Dictionary<uint, object?> table = new Dictionary<uint, object?>();
      foreach (object pair in enumerable)
      {
         string key;
         object? value;
         if (pair is DictionaryEntry entry)
         {
            key = entry.Key?.ToString() ?? "<null>";
            value = entry.Value;
         }
         else if (pair.GetType().IsSubclassOfDefinition(typeof(KeyValuePair<,>), out Type? pairType))
            GetKeyValuePairInfo(pair, pairType, out key, out value);
         else
            continue;

         if (writeContext.GetOrCreateTableKeyId(key, out uint tableKeyId))
         {
            TableKeyReference keyRef = new TableKeyReference(key, tableKeyId);
            dataCollector.Deposit(keyRef);
         }

         object? convertedValue = ConvertValue(writeContext, dataCollector, value);
         table.Add(tableKeyId, convertedValue);
      }

      return new TableInfo(table);
   }
   private static void GetKeyValuePairInfo(object pair, Type constructedPairType, out string key, out object? value)
   {
      KeyValuePairAccessor? accessors;

      AccessorsLock.EnterUpgradeableReadLock();
      try
      {
         if (KeyValuePairAccessors.TryGetValue(constructedPairType, out accessors) == false)
         {
            AccessorsLock.EnterWriteLock();
            try
            {
               accessors = GenerateKeyValuePairAccessor(constructedPairType);
               KeyValuePairAccessors.Add(constructedPairType, accessors);
            }
            finally
            {
               AccessorsLock.ExitWriteLock();
            }
         }
      }
      finally
      {
         AccessorsLock.ExitUpgradeableReadLock();
      }

      key = accessors.GetKey.Invoke(pair);
      value = accessors.GetValue.Invoke(pair);
   }

   private static ICollectionInfo ConvertCollection(ILogWriteContext writeContext, ILogDataCollector dataCollector, IEnumerable enumerable)
   {
      List<object?> values = new List<object?>();
      foreach (object? value in enumerable)
      {
         object? convertedValue = ConvertValue(writeContext, dataCollector, value);
         values.Add(convertedValue);
      }

      return new CollectionInfo(values);
   }
   private static object? ConvertValue(ILogWriteContext writeContext, ILogDataCollector dataCollector, object? value)
   {
      if (value is null)
         return null;

      Type type = value.GetType();
      if (DataKinds.ContainsKey(type))
         return value;

      if (IsDictionary(type))
         return ConvertDictionary(writeContext, dataCollector, (IEnumerable)value);
      if (IsCollection(type))
         return ConvertCollection(writeContext, dataCollector, (IEnumerable)value);

      ulong valueTypeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(writeContext, dataCollector, type);
      return new UnknownTableValue(valueTypeId);
   }

   private static bool IsDictionary(Type type)
   {
      Type[] interfaces = new Type[]
      {
         typeof(IDictionary<,>),
         typeof(IReadOnlyDictionary<,>),
      };

      foreach (Type interfaceType in interfaces)
      {
         if (type.ImplementsOpenInterface(interfaceType))
            return true;
      }

      return type == typeof(IDictionary);
   }
   private static bool IsCollection(Type type)
   {

      Type[] interfaces = new Type[]
      {
         typeof(ICollection<>),
         typeof(IReadOnlyCollection<>),
      };

      foreach (Type interfaceType in interfaces)
      {
         if (type.ImplementsOpenInterface(interfaceType))
            return true;
      }

      return type == typeof(ICollection);
   }

   private static KeyValuePairAccessor GenerateKeyValuePairAccessor(Type type)
   {
      Func<object, string> keyAccessor = GenerateKeyAccessor(type);
      Func<object, object?> valueAccessor = GenerateValueAccessor(type);

      return new KeyValuePairAccessor(keyAccessor, valueAccessor);
   }
   private static Func<object, object?> GenerateValueAccessor(Type type)
   {
      ParameterExpression pairParameter = Expression.Parameter(typeof(object), "pair");
      UnaryExpression cast = Expression.Convert(pairParameter, type);

      MemberExpression value = Expression.Property(cast, nameof(KeyValuePair<object, object>.Value));
      UnaryExpression objectCast = Expression.Convert(value, typeof(object));

      Expression<Func<object, object?>> expression = Expression.Lambda<Func<object, object?>>(objectCast, pairParameter);
      return expression.Compile();
   }
   private static Func<object, string> GenerateKeyAccessor(Type type)
   {
      ParameterExpression pairParameter = Expression.Parameter(typeof(object), "pair");
      UnaryExpression cast = Expression.Convert(pairParameter, type);

      MemberExpression key = Expression.Property(cast, nameof(KeyValuePair<object, object>.Key));
      MethodCallExpression stringKey = Expression.Call(key, nameof(object.ToString), null);
      ConstantExpression nullConst = Expression.Constant("<null>");

      BinaryExpression notNullStringKey = Expression.Coalesce(stringKey, nullConst);

      Expression<Func<object, string>> expression = Expression.Lambda<Func<object, string>>(notNullStringKey, pairParameter);
      return expression.Compile();
   }
   #endregion
}
