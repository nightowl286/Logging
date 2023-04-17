using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Versioning;
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
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Tables;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Types;
using TNO.Logging.Writing.Entries;
using TNO.Logging.Writing.Entries.Components;
using TNO.Logging.Writing.Serialisers;
using TNO.Logging.Writing.Serialisers.LogData;
using TNO.Logging.Writing.Serialisers.LogData.Assemblies;
using TNO.Logging.Writing.Serialisers.LogData.Constructors;
using TNO.Logging.Writing.Serialisers.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.StackTraces;
using TNO.Logging.Writing.Serialisers.LogData.Tables;
using TNO.Logging.Writing.Serialisers.LogData.Types;

namespace TNO.Logging.Writing;

/// <summary>
/// Represents a facade for the log writing system.
/// </summary>
public class LogWriterFacade : ILogWriterFacade
{
   #region Fields
   private readonly ServiceFacade _serviceFacade = new ServiceFacade();
   private readonly DataVersionMap _map = new DataVersionMap();

   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="LogWriterFacade"/>.</summary>
   public LogWriterFacade()
   {
      _serviceFacade.RegisterSelf();
      RegisterSerialisers(_serviceFacade);
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public T GetSerialiser<T>() where T : notnull, ISerialiser => _serviceFacade.Get<T>();

   /// <inheritdoc/>
   public DataVersionMap GetVersionMap() => _map;
   #endregion

   #region Helpers
   private void RegisterSerialisers(IServiceFacade facade)
   {
      RegisterLogDataSerialisers(facade);
      RegisterComponentSerialisers(facade);

      VersionedSingleton<IEntrySerialiser, EntrySerialiser>(facade);

      facade.Singleton<IDataVersionMapSerialiser, DataVersionMapSerialiser>();
   }
   private void RegisterLogDataSerialisers(IServiceFacade facade)
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
      VersionedSingleton<ITableInfoSerialiser, TableInfoSerialiser>(facade);

      // Log References
      VersionedSingleton<IFileReferenceSerialiser, FileReferenceSerialiser>(facade);
      VersionedSingleton<ITagReferenceSerialiser, TagReferenceSerialiser>(facade);
      VersionedSingleton<ITableKeyReferenceSerialiser, TableKeyReferenceSerialiser>(facade);
      VersionedSingleton<IAssemblyReferenceSerialiser, AssemblyReferenceSerialiser>(facade);
      VersionedSingleton<ITypeReferenceSerialiser, TypeReferenceSerialiser>(facade);
   }
   private void RegisterComponentSerialisers(IServiceFacade facade)
   {
      VersionedSingleton<IMessageComponentSerialiser, MessageComponentSerialiser>(facade);
      VersionedSingleton<ITagComponentSerialiser, TagComponentSerialiser>(facade);
      VersionedSingleton<IThreadComponentSerialiser, ThreadComponentSerialiser>(facade);
      VersionedSingleton<IEntryLinkComponentSerialiser, EntryLinkComponentSerialiser>(facade);
      VersionedSingleton<IAssemblyComponentSerialiser, AssemblyComponentSerialiser>(facade);
      VersionedSingleton<IStackTraceComponentSerialiser, StackTraceComponentSerialiser>(facade);
      VersionedSingleton<ITypeComponentSerialiser, TypeComponentSerialiser>(facade);
      VersionedSingleton<ITableComponentSerialiser, TableComponentSerialiser>(facade);

      facade.Singleton<IComponentSerialiserDispatcher, ComponentSerialiserDispatcher>();
   }
   private void VersionedSingleton<TService, TType>(IServiceFacade facade)
      where TService : notnull
      where TType : notnull, TService, IVersioned
   {
      Type type = typeof(TType);
      if (typeof(TType).TryGetVersion(out uint version))
      {
         foreach (VersionedDataKind kind in type.GetDataKinds())
            _map.Add(kind, version);
      }

      facade.Singleton<TService, TType>();
   }
   #endregion
}