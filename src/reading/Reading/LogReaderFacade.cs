using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Reading.Abstractions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries;
using TNO.Logging.Reading.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;
using TNO.Logging.Reading.Entries.Components;
using TNO.Logging.Reading.Entries.Components.Message;
using TNO.Logging.Reading.Entries.Versions;

namespace TNO.Logging.Reading;

/// <summary>
/// Represents a facade for the log reading system.
/// </summary>
public class LogReaderFacade : ILogReaderFacade
{
   #region Fields
   private readonly ServiceFacade _serviceFacade = new ServiceFacade();
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="LogReaderFacade"/>.</summary>
   public LogReaderFacade()
   {
      _serviceFacade.RegisterSelf();
      RegisterSelectors(_serviceFacade);
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IDeserialiserProvider GenerateProvider(DataVersionMap map)
   {
      ServiceFacade providerFacade = new ServiceFacade();

      RegisterFromKinds(providerFacade, map, GetComponentDataKinds());
      providerFacade.Singleton<IComponentDeserialiserDispatcher, ComponentDeserialiserDispatcher>();
      RegisterFromKinds(providerFacade, map, GetNonComponentDataKinds());

      DeserialiserProvider provider = new DeserialiserProvider(providerFacade);
      return provider;
   }
   private void RegisterFromKinds(IServiceRegistrar registrar, DataVersionMap map, IEnumerable<VersionedDataKind> kinds)
   {
      foreach (VersionedDataKind kind in GetComponentDataKinds())
      {
         if (map.TryGetValue(kind, out uint version))
            RegisterFromKind(registrar, kind, version);
      }
   }
   private void RegisterFromKind(IServiceRegistrar registrar, VersionedDataKind kind, uint version)
   {
      if (kind is VersionedDataKind.Entry)
         RegisterWithProvider<IEntryDeserialiserSelector, IEntryDeserialiser>(registrar, version);
      else if (kind is VersionedDataKind.Message)
         RegisterWithProvider<IMessageComponentDeserialiserSelector, IMessageComponentDeserialiser>(registrar, version);
   }
   private void RegisterWithProvider<TSelector, TDeserialiser>(IServiceRegistrar registrar, uint version)
      where TSelector : notnull, IDeserialiserSelector<TDeserialiser>
      where TDeserialiser : notnull, IDeserialiser
   {
      TSelector selector = _serviceFacade.Get<TSelector>();
      if (selector.TrySelect(version, out TDeserialiser? deserialiser))
         registrar.Instance(deserialiser);
      else
         throw new ArgumentException($"No deserialiser of the required type ({typeof(TDeserialiser)}) could be selected for the version #{version:n0}.", nameof(version));
   }
   #endregion

   #region Helpers
   private static IEnumerable<VersionedDataKind> GetComponentDataKinds()
   {
      yield return VersionedDataKind.Message;
   }
   private static IEnumerable<VersionedDataKind> GetNonComponentDataKinds()
   {
      yield return VersionedDataKind.Entry;
   }
   private static void RegisterSelectors(IServiceFacade facade)
   {
      RegisterComponentSelectors(facade);

      facade
         .Singleton<IEntryDeserialiserSelector, EntryDeserialiserSelector>();
   }

   private static void RegisterComponentSelectors(IServiceFacade facade)
   {
      facade
         .Singleton<IMessageComponentDeserialiserSelector, MessageComponentDeserialiserSelector>();
   }
   #endregion
}
