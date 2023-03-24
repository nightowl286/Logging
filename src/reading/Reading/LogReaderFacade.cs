using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Reading.Abstractions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries;
using TNO.Logging.Reading.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.Assembly;
using TNO.Logging.Reading.Abstractions.Entries.Components.EntryLink;
using TNO.Logging.Reading.Abstractions.Entries.Components.Message;
using TNO.Logging.Reading.Abstractions.Entries.Components.SimpleStackTrace;
using TNO.Logging.Reading.Abstractions.Entries.Components.Table;
using TNO.Logging.Reading.Abstractions.Entries.Components.Tag;
using TNO.Logging.Reading.Abstractions.Entries.Components.Thread;
using TNO.Logging.Reading.Abstractions.LogData.AssemblyInfos;
using TNO.Logging.Reading.Abstractions.LogData.ContextInfos;
using TNO.Logging.Reading.Abstractions.LogData.FileReferences;
using TNO.Logging.Reading.Abstractions.LogData.TableKeyReferences;
using TNO.Logging.Reading.Abstractions.LogData.TagReferences;
using TNO.Logging.Reading.Abstractions.Readers;
using TNO.Logging.Reading.Entries;
using TNO.Logging.Reading.Entries.Components;
using TNO.Logging.Reading.Entries.Components.Assembly;
using TNO.Logging.Reading.Entries.Components.EntryLink;
using TNO.Logging.Reading.Entries.Components.Message;
using TNO.Logging.Reading.Entries.Components.SimpleStackTrace;
using TNO.Logging.Reading.Entries.Components.Table;
using TNO.Logging.Reading.Entries.Components.Tag;
using TNO.Logging.Reading.Entries.Components.Thread;
using TNO.Logging.Reading.LogData.AssemblyInfos;
using TNO.Logging.Reading.LogData.ContextInfos;
using TNO.Logging.Reading.LogData.FileReferences;
using TNO.Logging.Reading.LogData.TableKeyReferences;
using TNO.Logging.Reading.LogData.TagReferences;
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
      RegisterFromKinds(providerFacade, map,
         VersionedDataKind.Message,
         VersionedDataKind.Tag,
         VersionedDataKind.Thread,
         VersionedDataKind.EntryLink,
         VersionedDataKind.Table,
         VersionedDataKind.Assembly,
         VersionedDataKind.SimpleStackTrace);
      providerFacade.Singleton<IComponentDeserialiserDispatcher, ComponentDeserialiserDispatcher>();

      RegisterFromKinds(providerFacade, map,
         VersionedDataKind.Entry,
         VersionedDataKind.FileReference,
         VersionedDataKind.ContextInfo,
         VersionedDataKind.TagReference,
         VersionedDataKind.TableKeyReference,
         VersionedDataKind.AssemblyInfo);

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
      RegisterLogDataSelectors(facade);
   }
   private static void RegisterLogDataSelectors(IServiceFacade facade)
   {
      facade
         .Singleton<IEntryDeserialiserSelector, EntryDeserialiserSelector>()
         .Singleton<IFileReferenceDeserialiserSelector, FileReferenceDeserialiserSelector>()
         .Singleton<IContextInfoDeserialiserSelector, ContextInfoDeserialiserSelector>()
         .Singleton<ITagReferenceDeserialiserSelector, TagReferenceDeserialiserSelector>()
         .Singleton<ITableKeyReferenceDeserialiserSelector, TableKeyReferenceDeserialiserSelector>()
         .Singleton<IAssemblyInfoDeserialiserSelector, AssemblyInfoDeserialiserSelector>();
   }
   private static void RegisterComponentSelectors(IServiceFacade facade)
   {
      facade
         .Singleton<IMessageComponentDeserialiserSelector, MessageComponentDeserialiserSelector>()
         .Singleton<ITagComponentDeserialiserSelector, TagComponentDeserialiserSelector>()
         .Singleton<IThreadComponentDeserialiserSelector, ThreadComponentDeserialiserSelector>()
         .Singleton<IEntryLinkComponentDeserialiserSelector, EntryLinkComponentDeserialiserSelector>()
         .Singleton<ITableComponentDeserialiserSelector, TableComponentDeserialiserSelector>()
         .Singleton<IAssemblyComponentDeserialiserSelector, AssemblyComponentDeserialiserSelector>()
         .Singleton<ISimpleStackTraceComponentDeserialiserSelector, SimpleStackTraceComponentDeserialiserSelector>();
   }
   private static void RegisterFromKind(IServiceFacade facade, VersionedDataKind kind, uint version)
   {
      if (kind is VersionedDataKind.Entry)
         RegisterWithProvider<IEntryDeserialiserSelector, IEntryDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.Message)
         RegisterWithProvider<IMessageComponentDeserialiserSelector, IMessageComponentDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.Tag)
         RegisterWithProvider<ITagComponentDeserialiserSelector, ITagComponentDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.Thread)
         RegisterWithProvider<IThreadComponentDeserialiserSelector, IThreadComponentDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.EntryLink)
         RegisterWithProvider<IEntryLinkComponentDeserialiserSelector, IEntryLinkComponentDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.Table)
         RegisterWithProvider<ITableComponentDeserialiserSelector, ITableComponentDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.Assembly)
         RegisterWithProvider<IAssemblyComponentDeserialiserSelector, IAssemblyComponentDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.FileReference)
         RegisterWithProvider<IFileReferenceDeserialiserSelector, IFileReferenceDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.ContextInfo)
         RegisterWithProvider<IContextInfoDeserialiserSelector, IContextInfoDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.TagReference)
         RegisterWithProvider<ITagReferenceDeserialiserSelector, ITagReferenceDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.TableKeyReference)
         RegisterWithProvider<ITableKeyReferenceDeserialiserSelector, ITableKeyReferenceDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.AssemblyInfo)
         RegisterWithProvider<IAssemblyInfoDeserialiserSelector, IAssemblyInfoDeserialiser>(facade, version);
      else if (kind is VersionedDataKind.SimpleStackTrace)
         RegisterWithProvider<ISimpleStackTraceComponentDeserialiserSelector, ISimpleStackTraceComponentDeserialiser>(facade, version);
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