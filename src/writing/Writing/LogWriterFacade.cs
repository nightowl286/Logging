using TNO.DependencyInjection;
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
   private readonly IServiceScope _serviceScope = new ServiceFacade().CreateNew();
   private readonly DataVersionMap _map = new DataVersionMap();

   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="LogWriterFacade"/>.</summary>
   public LogWriterFacade()
   {
      _serviceScope.Registrar.RegisterComponents();
      RegisterSerialisers(_serviceScope.Registrar);
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public T GetSerialiser<T>() where T : notnull, ISerialiser => _serviceScope.Requester.Get<T>();

   /// <inheritdoc/>
   public DataVersionMap GetVersionMap() => _map;
   #endregion

   #region Helpers
   private void RegisterSerialisers(IServiceRegistrar registrar)
   {
      RegisterLogDataSerialisers(registrar);
      RegisterComponentSerialisers(registrar);

      VersionedSingleton<IEntrySerialiser, EntrySerialiser>(registrar);

      registrar.Singleton<IDataVersionMapSerialiser, DataVersionMapSerialiser>();
   }
   private void RegisterLogDataSerialisers(IServiceRegistrar registrar)
   {
      // Methods
      VersionedSingleton<IParameterInfoSerialiser, ParameterInfoSerialiser>(registrar);
      VersionedSingleton<IMethodInfoSerialiser, MethodInfoSerialiser>(registrar);
      VersionedSingleton<IConstructorInfoSerialiser, ConstructorInfoSerialiser>(registrar);
      registrar.Singleton<IMethodBaseInfoSerialiserDispatcher, MethodBaseInfoSerialiserDispatcher>();

      // Stack Traces
      VersionedSingleton<IStackFrameInfoSerialiser, StackFrameInfoSerialiser>(registrar);
      VersionedSingleton<IStackTraceInfoSerialiser, StackTraceInfoSerialiser>(registrar);

      // Log Info
      VersionedSingleton<IContextInfoSerialiser, ContextInfoSerialiser>(registrar);
      VersionedSingleton<IAssemblyInfoSerialiser, AssemblyInfoSerialiser>(registrar);
      VersionedSingleton<ITypeInfoSerialiser, TypeInfoSerialiser>(registrar);
      VersionedSingleton<ITableInfoSerialiser, TableInfoSerialiser>(registrar);

      // Log References
      VersionedSingleton<IFileReferenceSerialiser, FileReferenceSerialiser>(registrar);
      VersionedSingleton<ITagReferenceSerialiser, TagReferenceSerialiser>(registrar);
      VersionedSingleton<ITableKeyReferenceSerialiser, TableKeyReferenceSerialiser>(registrar);
      VersionedSingleton<IAssemblyReferenceSerialiser, AssemblyReferenceSerialiser>(registrar);
      VersionedSingleton<ITypeReferenceSerialiser, TypeReferenceSerialiser>(registrar);
   }
   private void RegisterComponentSerialisers(IServiceRegistrar registrar)
   {
      VersionedSingleton<IMessageComponentSerialiser, MessageComponentSerialiser>(registrar);
      VersionedSingleton<ITagComponentSerialiser, TagComponentSerialiser>(registrar);
      VersionedSingleton<IThreadComponentSerialiser, ThreadComponentSerialiser>(registrar);
      VersionedSingleton<IEntryLinkComponentSerialiser, EntryLinkComponentSerialiser>(registrar);
      VersionedSingleton<IAssemblyComponentSerialiser, AssemblyComponentSerialiser>(registrar);
      VersionedSingleton<IStackTraceComponentSerialiser, StackTraceComponentSerialiser>(registrar);
      VersionedSingleton<ITypeComponentSerialiser, TypeComponentSerialiser>(registrar);
      VersionedSingleton<ITableComponentSerialiser, TableComponentSerialiser>(registrar);

      registrar.Singleton<IComponentSerialiserDispatcher, ComponentSerialiserDispatcher>();
   }
   private void VersionedSingleton<TService, TType>(IServiceRegistrar registrar)
      where TService : notnull
      where TType : notnull, TService, IVersioned
   {
      Type type = typeof(TType);
      if (typeof(TType).TryGetVersion(out uint version))
      {
         foreach (VersionedDataKind kind in type.GetDataKinds())
            _map.Add(kind, version);
      }

      registrar.Singleton<TService, TType>();
   }
   #endregion
}