using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Reading.Abstractions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries;
using TNO.Logging.Reading.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;
using TNO.Logging.Reading.Abstractions.FileReferences;
using TNO.Logging.Reading.Abstractions.Readers;
using TNO.Logging.Reading.Entries;
using TNO.Logging.Reading.Entries.Components;
using TNO.Logging.Reading.Entries.Components.Message;
using TNO.Logging.Reading.FileReferences;
using TNO.Logging.Reading.Readers;

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

      // components
      RegisterFromKinds(providerFacade, map, VersionedDataKind.Message);
      providerFacade.Singleton<IComponentDeserialiserDispatcher, ComponentDeserialiserDispatcher>();

      RegisterFromKinds(providerFacade, map,
         VersionedDataKind.Entry,
         VersionedDataKind.FileReference);

      DeserialiserProvider provider = new DeserialiserProvider(providerFacade);
      return provider;
   }
   private static void RegisterFromKinds(IServiceFacade facade, DataVersionMap map, params VersionedDataKind[] kinds)
   {
      foreach (VersionedDataKind kind in kinds)
      {
         if (map.TryGetValue(kind, out uint version))
            RegisterFromKind(facade, kind, version);
      }
   }

   /// <inheritdoc/>
   public T GetDeserialiser<T>() where T : notnull, IDeserialiser => _serviceFacade.Get<T>();

   /// <inheritdoc/>
   public IFileSystemLogReader ReadFromFileSystem(string path)
   {
      FileSystemLogReader reader = new FileSystemLogReader(path, this);

      return reader;
   }
   #endregion

   #region Helpers
   private static void RegisterSelectors(IServiceFacade facade)
   {
      RegisterComponentSelectors(facade);

      facade
         .Singleton<IEntryDeserialiserSelector, EntryDeserialiserSelector>()
         .Singleton<IFileReferenceDeserialiserSelector, FileReferenceDeserialiserSelector>();
   }
   private static void RegisterComponentSelectors(IServiceFacade facade)
   {
      facade
         .Singleton<IMessageComponentDeserialiserSelector, MessageComponentDeserialiserSelector>();
   }
   private static void RegisterFromKind(IServiceFacade facade, VersionedDataKind kind, uint version)
   {
      if (kind is VersionedDataKind.Entry)
         RegisterWithProvider<IEntryDeserialiserSelector, IEntryDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.Message)
         RegisterWithProvider<IMessageComponentDeserialiserSelector, IMessageComponentDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.FileReference)
         RegisterWithProvider<IFileReferenceDeserialiserSelector, IFileReferenceDeserialiser>(facade, version);
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
   private static void RegisterNonVersioned(IServiceFacade facade)
   {
      facade
         .Singleton<IDataVersionMapDeserialiser, DataVersionMapDeserialiser>();
   }
   #endregion
}