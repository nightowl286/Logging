using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Assemblies;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Constructors;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Methods;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Parameters;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.StackTraces;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Types;
using TNO.Logging.Writing.Entries;
using TNO.Logging.Writing.Entries.Components;
using TNO.Logging.Writing.Serialisers;
using TNO.Logging.Writing.Serialisers.LogData;
using TNO.Logging.Writing.Serialisers.LogData.Assemblies;
using TNO.Logging.Writing.Serialisers.LogData.Constructors;
using TNO.Logging.Writing.Serialisers.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.StackTraces;
using TNO.Logging.Writing.Serialisers.LogData.Types;

namespace TNO.Logging.Writing;

/// <summary>
/// Represents a facade for the log writing system.
/// </summary>
public class LogWriterFacade : ILogWriterFacade
{
   #region Fields
   private readonly ServiceFacade _serviceFacade = new ServiceFacade();
   private readonly DataVersionMap _map;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="LogWriterFacade"/>.</summary>
   public LogWriterFacade()
   {
      _serviceFacade.RegisterSelf();
      RegisterSerialisers(_serviceFacade);

      _map = GenerateVersionMap();
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public T GetSerialiser<T>() where T : notnull, ISerialiser => _serviceFacade.Get<T>();

   /// <inheritdoc/>
   public DataVersionMap GetVersionMap() => _map;
   #endregion

   #region Helpers
   private DataVersionMap GenerateVersionMap()
   {
      IEnumerable<IVersioned> versioned = GetAllVersioned();

      DataVersionMap map = new DataVersionMap();
      foreach (IVersioned version in versioned)
      {
         IEnumerable<VersionedDataKind> kinds = version
            .GetType()
            .GetDataKinds();

         foreach (VersionedDataKind kind in kinds)
            map.Add(kind, version.Version);
      }

      return map;
   }
   private IEnumerable<IVersioned> GetAllVersioned()
   {
      IEnumerable<IVersioned> collection = _serviceFacade.GetAll<IVersioned>()
         .Where(v => v is ISerialiser);

      return collection;
   }
   private static void RegisterSerialisers(IServiceFacade facade)
   {
      RegisterLogDataSerialisers(facade);
      RegisterComponentSerialisers(facade);

      VersionedSingleton<IEntrySerialiser, EntrySerialiser>(facade);

      facade.Singleton<IDataVersionMapSerialiser, DataVersionMapSerialiser>();
   }
   private static void RegisterLogDataSerialisers(IServiceFacade facade)
   {
      // Methods
      VersionedSingleton<IParameterInfoSerialiser, ParameterInfoSerialiser>(facade);
      VersionedSingleton<IMethodInfoSerialiser, MethodInfoSerialiser>(facade);
      VersionedSingleton<IConstructorInfoSerialiser, ConstructorInfoSerialiser>(facade);
      facade.Singleton<IMethodBaseInfoSerialiserDispatcher, MethodBaseInfoSerialiserDispatcher>();

      // Stack Traces
      VersionedSingleton<IStackFrameInfoSerialiser, StackFrameInfoSerialiser>(facade);
      VersionedSingleton<IStackTraceInfoSerialiser, StackTraceInfoSerialiser>(facade);

      // Log Info
      VersionedSingleton<IContextInfoSerialiser, ContextInfoSerialiser>(facade);
      VersionedSingleton<IAssemblyInfoSerialiser, AssemblyInfoSerialiser>(facade);
      VersionedSingleton<ITypeInfoSerialiser, TypeInfoSerialiser>(facade);

      // Log References
      VersionedSingleton<IFileReferenceSerialiser, FileReferenceSerialiser>(facade);
      VersionedSingleton<ITagReferenceSerialiser, TagReferenceSerialiser>(facade);
      VersionedSingleton<ITableKeyReferenceSerialiser, TableKeyReferenceSerialiser>(facade);
      VersionedSingleton<IAssemblyReferenceSerialiser, AssemblyReferenceSerialiser>(facade);
      VersionedSingleton<ITypeReferenceSerialiser, TypeReferenceSerialiser>(facade);
   }
   private static void RegisterComponentSerialisers(IServiceFacade facade)
   {
      VersionedSingleton<IMessageComponentSerialiser, MessageComponentSerialiser>(facade);
      VersionedSingleton<ITagComponentSerialiser, TagComponentSerialiser>(facade);
      VersionedSingleton<IThreadComponentSerialiser, ThreadComponentSerialiser>(facade);
      VersionedSingleton<IEntryLinkComponentSerialiser, EntryLinkComponentSerialiser>(facade);
      VersionedSingleton<ITableComponentSerialiser, TableComponentSerialiser>(facade);
      VersionedSingleton<IAssemblyComponentSerialiser, AssemblyComponentSerialiser>(facade);
      VersionedSingleton<IStackTraceComponentSerialiser, StackTraceComponentSerialiser>(facade);

      facade.Singleton<IComponentSerialiserDispatcher, ComponentSerialiserDispatcher>();
   }
   private static void VersionedSingleton<TService, TType>(IServiceFacade facade)
      where TService : notnull
      where TType : notnull, TService, IVersioned
   {
      TType instance = facade.Build<TType>();
      facade.Instance<IVersioned>(instance, AppendValueMode.Append);
      facade.Instance<TService>(instance);
   }
   #endregion
}