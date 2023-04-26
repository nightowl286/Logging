using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.LogData.Primitives;
using TNO.Logging.Common.LogData.Tables;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;

namespace TNO.Logging.Logging.Helpers.General;

/// <summary>
/// Contains useful functions related to the <see cref="ITableInfo"/>.
/// </summary>
public static class TableInfoHelper
{
   #region Delegates
   private delegate string GetKeyDelegate(object pair);
   private delegate object? GetValueDelegate(object pair);
   #endregion

   #region Fields
   private record class KeyValuePairAccessor(GetKeyDelegate GetKey, GetValueDelegate GetValue);
   private static readonly ReaderWriterLockSlim AccessorsLock = new ReaderWriterLockSlim();
   private static readonly ConditionalWeakTable<Type, KeyValuePairAccessor> KeyValuePairAccessors = new ConditionalWeakTable<Type, KeyValuePairAccessor>();
   #endregion

   #region Functions
   /// <summary>
   /// Tries to convert the given <paramref name="value"/> into an <see cref="ITableInfo"/>.
   /// </summary>
   /// <param name="writeContext">The write context to use to generate the needed ids.</param>
   /// <param name="dataCollector">The data collector to deposit new log data to.</param>
   /// <param name="value">The value to try and convert.</param>
   /// <param name="converted">The converted <paramref name="value"/>.</param>
   /// <returns>
   /// <see langword="true"/> if the conversion was successful, <see langword="false"/> otherwise.
   /// </returns>
   public static bool TryConvert(ILogWriteContext writeContext, ILogDataCollector dataCollector, object value, [NotNullWhen(true)] out ITableInfo? converted)
   {
      if (IsDictionary(value.GetType()))
      {
         converted = Convert(writeContext, dataCollector, (IEnumerable)value);
         return true;
      }

      converted = null;
      return false;
   }
   #endregion

   #region Helpers
   private static ITableInfo Convert(ILogWriteContext writeContext, ILogDataCollector dataCollector, IEnumerable enumerable)
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

         object? convertedValue = PrimitiveValueHelper.Convert(writeContext, dataCollector, value);
         table.Add(tableKeyId, convertedValue);
      }

      return new TableInfo(table);
   }
   private static bool IsDictionary(Type type)
   {
      if (type.ImplementsOpenInterface(typeof(IDictionary<,>)))
         return true;

      if (type.ImplementsOpenInterface(typeof(IReadOnlyDictionary<,>)))
         return true;

      return type.FindInterfaces(IsInterface, typeof(IDictionary)).Length > 0;
   }
   private static bool IsInterface(Type type, object? targetInterface)
   {
      if (targetInterface is not Type typedInterface)
         return false;

      return type == typedInterface;
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
   private static KeyValuePairAccessor GenerateKeyValuePairAccessor(Type type)
   {
      GetKeyDelegate keyAccessor = GenerateKeyAccessor(type);
      GetValueDelegate valueAccessor = GenerateValueAccessor(type);

      return new KeyValuePairAccessor(keyAccessor, valueAccessor);
   }
   private static GetValueDelegate GenerateValueAccessor(Type type)
   {
      ParameterExpression pairParameter = Expression.Parameter(typeof(object), "pair");
      Expression cast = Expression.Convert(pairParameter, type);

      Expression value = Expression.Property(cast, nameof(KeyValuePair<object, object>.Value));
      Expression objectCast = Expression.Convert(value, typeof(object));

      Expression<GetValueDelegate> expression = Expression.Lambda<GetValueDelegate>(objectCast, pairParameter);
      return expression.Compile();
   }
   private static GetKeyDelegate GenerateKeyAccessor(Type type)
   {
      ParameterExpression pairParameter = Expression.Parameter(typeof(object), "pair");
      Expression cast = Expression.Convert(pairParameter, type);

      Expression key = Expression.Property(cast, nameof(KeyValuePair<object, object>.Key));
      Expression stringKey = Expression.Call(key, nameof(object.ToString), null);
      Expression nullConst = Expression.Constant("<null>");

      Expression notNullStringKey = Expression.Coalesce(stringKey, nullConst);

      Expression<GetKeyDelegate> expression = Expression.Lambda<GetKeyDelegate>(notNullStringKey, pairParameter);
      return expression.Compile();
   }
   #endregion
}
