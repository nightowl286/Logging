﻿using TNO.DependencyInjection;
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

      RegisterNonVersioned(_serviceFacade);
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IDeserialiserProvider GenerateProvider(DataVersionMap map)
   {
      ServiceFacade providerFacade = new ServiceFacade();
      providerFacade.RegisterSelf();
      RegisterSelectors(providerFacade);

      RegisterFromKinds(providerFacade, map, GetComponentDataKinds());
      providerFacade.Singleton<IComponentDeserialiserDispatcher, ComponentDeserialiserDispatcher>();
      RegisterFromKinds(providerFacade, map, GetNonComponentDataKinds());

      DeserialiserProvider provider = new DeserialiserProvider(providerFacade);
      return provider;
   }
   private static void RegisterFromKinds(IServiceFacade facade, DataVersionMap map, IEnumerable<VersionedDataKind> kinds)
   {
      foreach (VersionedDataKind kind in kinds)
      {
         if (map.TryGetValue(kind, out uint version))
            RegisterFromKind(facade, kind, version);
      }
   }
   private static void RegisterFromKind(IServiceFacade facade, VersionedDataKind kind, uint version)
   {
      if (kind is VersionedDataKind.Entry)
         RegisterWithProvider<IEntryDeserialiserSelector, IEntryDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.Message)
         RegisterWithProvider<IMessageComponentDeserialiserSelector, IMessageComponentDeserialiser>(facade, version);
   }
   private static void RegisterWithProvider<TSelector, TDeserialiser>(IServiceFacade facade, uint version)
      where TSelector : notnull, IDeserialiserSelector<TDeserialiser>
      where TDeserialiser : notnull, IDeserialiser
   {
      TSelector selector = facade.Get<TSelector>();
      if (selector.TrySelect(version, out TDeserialiser? deserialiser))
         facade.Instance(deserialiser);
      else
         throw new ArgumentException($"No deserialiser of the required type ({typeof(TDeserialiser)}) could be selected for the version #{version:n0}.", nameof(version));
   }

   /// <inheritdoc/>
   public T GetDeserialiser<T>() where T : notnull, IDeserialiser => _serviceFacade.Get<T>();
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
   private static void RegisterNonVersioned(IServiceFacade facade)
   {
      facade
         .Singleton<IDataVersionMapDeserialiser, DataVersionMapDeserialiser>();
   }
   #endregion
}
