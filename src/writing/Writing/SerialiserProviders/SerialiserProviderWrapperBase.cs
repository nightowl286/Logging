using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;

namespace TNO.Logging.Writing.SerialiserProviders;
internal abstract class SerialiserProviderWrapperBase : ISerialiserProvider
{
   #region Fields
   private readonly IServiceScope _scope;
   private readonly ISerialiserProvider? _innerProvider;
   private readonly DataVersionMap _map = new DataVersionMap();
   #endregion

   #region Properties
   protected DataVersionMap Map => _map;
   protected IServiceScope Scope => _scope;
   #endregion
   #region Constructors
   protected SerialiserProviderWrapperBase(IServiceScope scope, ISerialiserProvider? innerProvider = null)
   {
      _scope = scope;
      _innerProvider = innerProvider;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public T GetSerialiser<T>() where T : notnull, ISerialiser
   {
      if (_innerProvider is null)
         return _scope.Requester.Get<T>();

      if (_scope.Requester.TryGet(out T? instance))
         return instance;

      return _innerProvider.GetSerialiser<T>();
   }

   /// <inheritdoc/>
   public DataVersionMap GetVersionMap()
   {
      DataVersionMap newMap = new DataVersionMap();

      if (_innerProvider is not null)
      {
         DataVersionMap baseMap = _innerProvider.GetVersionMap();

         foreach (KeyValuePair<VersionedDataKind, uint> pair in baseMap)
            newMap.Add(pair.Key, pair.Value);
      }

      foreach (KeyValuePair<VersionedDataKind, uint> pair in _map)
         newMap.Add(pair.Key, pair.Value);

      return newMap;
   }
   #endregion
}
