using TNO.DependencyInjection;
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
using TNO.Logging.Reading.Abstractions.Entries.Components.StackTrace;
using TNO.Logging.Reading.Abstractions.Entries.Components.Table;
using TNO.Logging.Reading.Abstractions.Entries.Components.Tag;
using TNO.Logging.Reading.Abstractions.Entries.Components.Thread;
using TNO.Logging.Reading.Abstractions.Entries.Components.Type;
using TNO.Logging.Reading.Abstractions.LogData.AssemblyInfos;
using TNO.Logging.Reading.Abstractions.LogData.AssemblyReferences;
using TNO.Logging.Reading.Abstractions.LogData.ContextInfos;
using TNO.Logging.Reading.Abstractions.LogData.FileReferences;
using TNO.Logging.Reading.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.LogData.Methods.ConstructorInfos;
using TNO.Logging.Reading.Abstractions.LogData.Methods.MethodInfos;
using TNO.Logging.Reading.Abstractions.LogData.Methods.ParameterInfos;
using TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackFrameInfos;
using TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackTraceInfos;
using TNO.Logging.Reading.Abstractions.LogData.TableKeyReferences;
using TNO.Logging.Reading.Abstractions.LogData.Tables;
using TNO.Logging.Reading.Abstractions.LogData.TagReferences;
using TNO.Logging.Reading.Abstractions.LogData.TypeInfos;
using TNO.Logging.Reading.Abstractions.LogData.TypeReferences;
using TNO.Logging.Reading.Abstractions.Readers;
using TNO.Logging.Reading.Entries;
using TNO.Logging.Reading.Entries.Components;
using TNO.Logging.Reading.Entries.Components.Assembly;
using TNO.Logging.Reading.Entries.Components.EntryLink;
using TNO.Logging.Reading.Entries.Components.Message;
using TNO.Logging.Reading.Entries.Components.StackTrace;
using TNO.Logging.Reading.Entries.Components.Tag;
using TNO.Logging.Reading.Entries.Components.Thread;
using TNO.Logging.Reading.Entries.Components.Type;
using TNO.Logging.Reading.LogData.AssemblyInfos;
using TNO.Logging.Reading.LogData.AssemblyReferences;
using TNO.Logging.Reading.LogData.ContextInfos;
using TNO.Logging.Reading.LogData.FileReferences;
using TNO.Logging.Reading.LogData.Methods;
using TNO.Logging.Reading.LogData.Methods.ConstructorInfos;
using TNO.Logging.Reading.LogData.Methods.MethodInfos;
using TNO.Logging.Reading.LogData.Methods.ParameterInfos;
using TNO.Logging.Reading.LogData.StackTraces.StackFrameInfos;
using TNO.Logging.Reading.LogData.StackTraces.StackTraceInfos;
using TNO.Logging.Reading.LogData.TableKeyReferences;
using TNO.Logging.Reading.LogData.Tables;
using TNO.Logging.Reading.LogData.TagReferences;
using TNO.Logging.Reading.LogData.TypeInfos;
using TNO.Logging.Reading.LogData.TypeReferences;
using TNO.Logging.Reading.Readers;

namespace TNO.Logging.Reading;

/// <summary>
/// Represents a facade for the log reading system.
/// </summary>
public class LogReaderFacade : ILogReaderFacade
{
   #region Fields
   private readonly IServiceScope _serviceScope = new ServiceFacade().CreateNew();
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="LogReaderFacade"/>.</summary>
   public LogReaderFacade()
   {
      RegisterNonVersioned(_serviceScope.Registrar);
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IDeserialiserProvider GenerateProvider(DataVersionMap map)
   {
      IServiceScope scope = new ServiceFacade().CreateNew();

      IServiceRegistrar registrar = scope.Registrar;
      registrar.RegisterComponents();
      RegisterSelectors(registrar);

      registrar
         .Singleton<IMethodBaseInfoDeserialiserDispatcher, MethodBaseInfoDeserialiserDispatcher>()
         .Singleton<IComponentDeserialiserDispatcher, ComponentDeserialiserDispatcher>();

      VersionedDataKind[] kinds = new VersionedDataKind[]
      {
         // Method Info
         VersionedDataKind.ParameterInfo,
         VersionedDataKind.MethodInfo,
         VersionedDataKind.ConstructorInfo,

         // Stack Traces
         VersionedDataKind.StackFrameInfo,
         VersionedDataKind.StackTraceInfo,

         // Log Data Info
         VersionedDataKind.ContextInfo,
         VersionedDataKind.AssemblyInfo,
         VersionedDataKind.TypeInfo,
         VersionedDataKind.TableInfo,

         // Log Data References
         VersionedDataKind.FileReference,
         VersionedDataKind.TagReference,
         VersionedDataKind.TableKeyReference,
         VersionedDataKind.AssemblyReference,
         VersionedDataKind.TypeReference,

         // Components
         VersionedDataKind.Message,
         VersionedDataKind.Tag,
         VersionedDataKind.Thread,
         VersionedDataKind.EntryLink,
         VersionedDataKind.Table,
         VersionedDataKind.Assembly,
         VersionedDataKind.StackTrace,
         VersionedDataKind.Type,

         // Entry
         VersionedDataKind.Entry,
      };

      RegisterFromKinds(scope, map, kinds);

      DeserialiserProvider provider = new DeserialiserProvider(scope.Requester);
      return provider;
   }
   private static void RegisterFromKinds(IServiceScope scope, DataVersionMap map, params VersionedDataKind[] kinds)
   {
      foreach (VersionedDataKind kind in kinds)
         RegisterFromKind(scope, map, kind);
   }

   private static void RegisterFromKind(IServiceScope scope, DataVersionMap map, VersionedDataKind kind)
   {
      if (map.TryGetValue(kind, out uint version))
         RegisterFromKind(scope, kind, version);
   }

   /// <inheritdoc/>
   public T GetDeserialiser<T>() where T : notnull, IDeserialiser => _serviceScope.Requester.Get<T>();

   /// <inheritdoc/>
   public IFileSystemLogReader ReadFromFileSystem(string path)
   {
      FileSystemLogReader reader = new FileSystemLogReader(path, this);

      return reader;
   }
   #endregion

   #region Helpers
   private static void RegisterSelectors(IServiceRegistrar registrar)
   {
      RegisterComponentSelectors(registrar);
      RegisterLogDataSelectors(registrar);
   }
   private static void RegisterLogDataSelectors(IServiceRegistrar registrar)
   {
      registrar
         .Singleton<IEntryDeserialiserSelector, EntryDeserialiserSelector>()
         .Singleton<IFileReferenceDeserialiserSelector, FileReferenceDeserialiserSelector>()
         .Singleton<IContextInfoDeserialiserSelector, ContextInfoDeserialiserSelector>()
         .Singleton<ITagReferenceDeserialiserSelector, TagReferenceDeserialiserSelector>()
         .Singleton<ITableKeyReferenceDeserialiserSelector, TableKeyReferenceDeserialiserSelector>()
         .Singleton<IAssemblyInfoDeserialiserSelector, AssemblyInfoDeserialiserSelector>()
         .Singleton<ITypeInfoDeserialiserSelector, TypeInfoDeserialiserSelector>()
         .Singleton<IAssemblyReferenceDeserialiserSelector, AssemblyReferenceDeserialiserSelector>()
         .Singleton<ITypeReferenceDeserialiserSelector, TypeReferenceDeserialiserSelector>()
         .Singleton<IParameterInfoDeserialiserSelector, ParameterInfoDeserialiserSelector>()
         .Singleton<IMethodInfoDeserialiserSelector, MethodInfoDeserialiserSelector>()
         .Singleton<IConstructorInfoDeserialiserSelector, ConstructorInfoDeserialiserSelector>()
         .Singleton<IStackFrameInfoDeserialiserSelector, StackFrameInfoDeserialiserSelector>()
         .Singleton<IStackTraceInfoDeserialiserSelector, StackTraceInfoDeserialiserSelector>()
         .Singleton<ITableInfoDeserialiserSelector, TableInfoDeserialiserSelector>();
   }
   private static void RegisterComponentSelectors(IServiceRegistrar registrar)
   {
      registrar
         .Singleton<IMessageComponentDeserialiserSelector, MessageComponentDeserialiserSelector>()
         .Singleton<ITagComponentDeserialiserSelector, TagComponentDeserialiserSelector>()
         .Singleton<IThreadComponentDeserialiserSelector, ThreadComponentDeserialiserSelector>()
         .Singleton<IEntryLinkComponentDeserialiserSelector, EntryLinkComponentDeserialiserSelector>()
         .Singleton<ITableComponentDeserialiserSelector, TableComponentDeserialiserSelector>()
         .Singleton<IAssemblyComponentDeserialiserSelector, AssemblyComponentDeserialiserSelector>()
         .Singleton<IStackTraceComponentDeserialiserSelector, StackTraceComponentDeserialiserSelector>()
         .Singleton<ITypeComponentDeserialiserSelector, TypeComponentDeserialiserSelector>();
   }
   private static void RegisterFromKind(IServiceScope scope, VersionedDataKind kind, uint version)
   {
      if (kind is VersionedDataKind.Entry)
         RegisterWithProvider<IEntryDeserialiserSelector, IEntryDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.Message)
         RegisterWithProvider<IMessageComponentDeserialiserSelector, IMessageComponentDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.Tag)
         RegisterWithProvider<ITagComponentDeserialiserSelector, ITagComponentDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.Thread)
         RegisterWithProvider<IThreadComponentDeserialiserSelector, IThreadComponentDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.EntryLink)
         RegisterWithProvider<IEntryLinkComponentDeserialiserSelector, IEntryLinkComponentDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.TableInfo)
         RegisterWithProvider<ITableInfoDeserialiserSelector, ITableInfoDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.Assembly)
         RegisterWithProvider<IAssemblyComponentDeserialiserSelector, IAssemblyComponentDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.StackTrace)
         RegisterWithProvider<IStackTraceComponentDeserialiserSelector, IStackTraceComponentDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.FileReference)
         RegisterWithProvider<IFileReferenceDeserialiserSelector, IFileReferenceDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.ContextInfo)
         RegisterWithProvider<IContextInfoDeserialiserSelector, IContextInfoDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.TagReference)
         RegisterWithProvider<ITagReferenceDeserialiserSelector, ITagReferenceDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.TableKeyReference)
         RegisterWithProvider<ITableKeyReferenceDeserialiserSelector, ITableKeyReferenceDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.AssemblyInfo)
         RegisterWithProvider<IAssemblyInfoDeserialiserSelector, IAssemblyInfoDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.AssemblyReference)
         RegisterWithProvider<IAssemblyReferenceDeserialiserSelector, IAssemblyReferenceDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.TypeInfo)
         RegisterWithProvider<ITypeInfoDeserialiserSelector, ITypeInfoDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.TypeReference)
         RegisterWithProvider<ITypeReferenceDeserialiserSelector, ITypeReferenceDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.ParameterInfo)
         RegisterWithProvider<IParameterInfoDeserialiserSelector, IParameterInfoDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.MethodInfo)
         RegisterWithProvider<IMethodInfoDeserialiserSelector, IMethodInfoDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.ConstructorInfo)
         RegisterWithProvider<IConstructorInfoDeserialiserSelector, IConstructorInfoDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.StackFrameInfo)
         RegisterWithProvider<IStackFrameInfoDeserialiserSelector, IStackFrameInfoDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.StackTraceInfo)
         RegisterWithProvider<IStackTraceInfoDeserialiserSelector, IStackTraceInfoDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.Type)
         RegisterWithProvider<ITypeComponentDeserialiserSelector, ITypeComponentDeserialiser>(scope, version);
      else if (kind is VersionedDataKind.Table)
         RegisterWithProvider<ITableComponentDeserialiserSelector, ITableComponentDeserialiser>(scope, version);
   }
   private static void RegisterWithProvider<TSelector, TDeserialiser>(IServiceScope scope, uint version)
      where TSelector : notnull, IDeserialiserSelector<TDeserialiser>
      where TDeserialiser : notnull, IDeserialiser
   {
      TSelector selector = scope.Requester.Get<TSelector>();
      if (selector.TrySelect(version, out TDeserialiser? deserialiser))
         scope.Registrar.Instance(deserialiser);
      else
         throw new ArgumentException($"No deserialiser of the required type ({typeof(TDeserialiser)}) could be selected for the version #{version:n0}.", nameof(version));
   }
   private static void RegisterNonVersioned(IServiceRegistrar registrar)
   {
      registrar
         .Singleton<IDataVersionMapDeserialiser, DataVersionMapDeserialiser>();
   }
   #endregion
}