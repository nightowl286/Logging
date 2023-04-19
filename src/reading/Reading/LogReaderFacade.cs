using System.Reflection;
using System.Runtime.InteropServices;
using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Reading.Abstractions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Exceptions;
using TNO.Logging.Reading.Abstractions.Readers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries;
using TNO.Logging.Reading.Entries.Components.Assembly;
using TNO.Logging.Reading.Entries.Components.EntryLink;
using TNO.Logging.Reading.Entries.Components.Exception;
using TNO.Logging.Reading.Entries.Components.Message;
using TNO.Logging.Reading.Entries.Components.StackTrace;
using TNO.Logging.Reading.Entries.Components.Tag;
using TNO.Logging.Reading.Entries.Components.Thread;
using TNO.Logging.Reading.Entries.Components.Type;
using TNO.Logging.Reading.Exceptions;
using TNO.Logging.Reading.Exceptions.ExceptionInfos;
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
      Deserialiser deserialiser = new Deserialiser(scope.Requester);
      registrar.Instance<IDeserialiser>(deserialiser);

      registrar.RegisterComponents();

      RegisterSelectors(registrar);

      Deserialiser<IMethodBaseInfo, MethodBaseInfoDeserialiserDispatcher>(registrar);

      RegisterExceptions(registrar);

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
         VersionedDataKind.ExceptionInfo,

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
         VersionedDataKind.Exception,

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
   public IDeserialiser<T> GetDeserialiser<T>() where T : notnull => _serviceScope.Requester.Get<IDeserialiser<T>>();

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
      Selector<IEntry, EntryDeserialiserSelector>(registrar);
      Selector<FileReference, FileReferenceDeserialiserSelector>(registrar);
      Selector<ContextInfo, ContextInfoDeserialiserSelector>(registrar);
      Selector<TagReference, TagReferenceDeserialiserSelector>(registrar);
      Selector<TableKeyReference, TableKeyReferenceDeserialiserSelector>(registrar);
      Selector<IAssemblyInfo, AssemblyInfoDeserialiserSelector>(registrar);
      Selector<ITypeInfo, TypeInfoDeserialiserSelector>(registrar);
      Selector<AssemblyReference, AssemblyReferenceDeserialiserSelector>(registrar);
      Selector<TypeReference, TypeReferenceDeserialiserSelector>(registrar);
      Selector<IParameterInfo, ParameterInfoDeserialiserSelector>(registrar);
      Selector<IMethodInfo, MethodInfoDeserialiserSelector>(registrar);
      Selector<IConstructorInfo, ConstructorInfoDeserialiserSelector>(registrar);
      Selector<IStackFrameInfo, StackFrameInfoDeserialiserSelector>(registrar);
      Selector<IStackTraceInfo, StackTraceInfoDeserialiserSelector>(registrar);
      Selector<ITableInfo, TableInfoDeserialiserSelector>(registrar);
      Selector<IExceptionInfo, ExceptionInfoDeserialiserSelector>(registrar);
   }
   private static void RegisterComponentSelectors(IServiceRegistrar registrar)
   {
      Selector<IMessageComponent, MessageComponentDeserialiserSelector>(registrar);
      Selector<ITagComponent, TagComponentDeserialiserSelector>(registrar);
      Selector<IThreadComponent, ThreadComponentDeserialiserSelector>(registrar);
      Selector<IEntryLinkComponent, EntryLinkComponentDeserialiserSelector>(registrar);
      Selector<ITableComponent, TableComponentDeserialiserSelector>(registrar);
      Selector<IAssemblyComponent, AssemblyComponentDeserialiserSelector>(registrar);
      Selector<IStackTraceComponent, StackTraceComponentDeserialiserSelector>(registrar);
      Selector<ITypeComponent, TypeComponentDeserialiserSelector>(registrar);
      Selector<IExceptionComponent, ExceptionComponentDeserialiserSelector>(registrar);
   }
   private static void RegisterFromKind(IServiceScope scope, VersionedDataKind kind, uint version)
   {
      // components
      if (kind is VersionedDataKind.Entry) Deserialiser<IEntry>(scope, version);
      else if (kind is VersionedDataKind.Message) Deserialiser<IMessageComponent>(scope, version);
      else if (kind is VersionedDataKind.Tag) Deserialiser<ITagComponent>(scope, version);
      else if (kind is VersionedDataKind.Thread) Deserialiser<IThreadComponent>(scope, version);
      else if (kind is VersionedDataKind.EntryLink) Deserialiser<IEntryLinkComponent>(scope, version);
      else if (kind is VersionedDataKind.TableInfo) Deserialiser<ITableInfo>(scope, version);
      else if (kind is VersionedDataKind.Assembly) Deserialiser<IAssemblyComponent>(scope, version);
      else if (kind is VersionedDataKind.StackTrace) Deserialiser<IStackTraceComponent>(scope, version);
      else if (kind is VersionedDataKind.Exception) Deserialiser<IExceptionComponent>(scope, version);
      else if (kind is VersionedDataKind.Type) Deserialiser<ITypeComponent>(scope, version);
      else if (kind is VersionedDataKind.Table) Deserialiser<ITableComponent>(scope, version);
      else if (kind is VersionedDataKind.ExceptionInfo) Deserialiser<IExceptionComponent>(scope, version);

      // log data
      else if (kind is VersionedDataKind.FileReference) Deserialiser<FileReference>(scope, version);
      else if (kind is VersionedDataKind.ContextInfo) Deserialiser<ContextInfo>(scope, version);
      else if (kind is VersionedDataKind.TagReference) Deserialiser<TagReference>(scope, version);
      else if (kind is VersionedDataKind.TableKeyReference) Deserialiser<TableKeyReference>(scope, version);
      else if (kind is VersionedDataKind.AssemblyInfo) Deserialiser<IAssemblyInfo>(scope, version);
      else if (kind is VersionedDataKind.AssemblyReference) Deserialiser<AssemblyReference>(scope, version);
      else if (kind is VersionedDataKind.TypeReference) Deserialiser<TypeReference>(scope, version);

      // log infos
      else if (kind is VersionedDataKind.TypeInfo) Deserialiser<ITypeInfo>(scope, version);
      else if (kind is VersionedDataKind.ParameterInfo) Deserialiser<IParameterInfo>(scope, version);
      else if (kind is VersionedDataKind.MethodInfo) Deserialiser<IMethodInfo>(scope, version);
      else if (kind is VersionedDataKind.ConstructorInfo) Deserialiser<IConstructorInfo>(scope, version);
      else if (kind is VersionedDataKind.StackFrameInfo) Deserialiser<IStackFrameInfo>(scope, version);
      else if (kind is VersionedDataKind.StackTraceInfo) Deserialiser<IStackTraceInfo>(scope, version);
      else
         throw new ArgumentException($"Unknown kind ({kind}).", nameof(kind));
   }
   private static void Deserialiser<TType>(IServiceScope scope, uint version) where TType : notnull
   {
      IDeserialiserSelector<TType> selector = scope.Requester.Get<IDeserialiserSelector<TType>>();
      if (selector.TrySelect(version, out IDeserialiser<TType>? deserialiser))
         scope.Registrar.Instance(deserialiser);
      else
         throw new ArgumentException($"No deserialiser for the required type ({typeof(TType)}) could be selected for the version #{version:n0}.", nameof(version));
   }
   private static void RegisterNonVersioned(IServiceRegistrar registrar)
   {
      Deserialiser<DataVersionMap, DataVersionMapDeserialiser>(registrar);
   }
   private static void RegisterExceptions(IServiceRegistrar registrar)
   {
      Dictionary<Guid, Type> deserialiserTypes = new Dictionary<Guid, Type>();

      Assembly currentAssembly = Assembly.GetExecutingAssembly();
      foreach (Type type in currentAssembly.GetTypes())
      {
         const string namespacePrefix = "TNO.Logging.Reading.Exceptions";
         bool isExceptionDeserialiserType =
            type.IsClass &&
            !type.IsInterface &&
            type.Namespace?.StartsWith(namespacePrefix) == true;

         GuidAttribute? attr = type.GetCustomAttribute<GuidAttribute>(false);
         if (isExceptionDeserialiserType && attr is not null)
         {
            Guid guid = new Guid(attr.Value);
            deserialiserTypes.Add(guid, type);
         }
      }

      ExceptionDataDeserialiser exceptionDataDeserialiser = new ExceptionDataDeserialiser(deserialiserTypes);

      registrar.Instance<IExceptionDataDeserialiser>(exceptionDataDeserialiser);
   }

   private static void Deserialiser<TType, TDeserialiser>(IServiceRegistrar registrar)
      where TDeserialiser : IDeserialiser<TType>
   {
      registrar.Singleton<IDeserialiser<TType>, TDeserialiser>();
   }
   private static void Selector<TType, TSelector>(IServiceRegistrar registrar)
      where TType : notnull
      where TSelector : IDeserialiserSelector<TType>
   {
      registrar.Singleton<IDeserialiserSelector<TType>, TSelector>();
   }
   #endregion
}