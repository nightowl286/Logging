using System.Reflection;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Entries;
using TNO.Logging.Writing.Entries.Components;
using TNO.Logging.Writing.Exceptions;
using TNO.Logging.Writing.Serialisers.LogData;
using TNO.Logging.Writing.Serialisers.LogData.Assemblies;
using TNO.Logging.Writing.Serialisers.LogData.Methods;
using TNO.Logging.Writing.Serialisers.LogData.StackTraces;
using TNO.Logging.Writing.Serialisers.LogData.Tables;
using TNO.Logging.Writing.Serialisers.LogData.Types;

namespace TNO.Logging.Writing.SerialiserProviders;
internal class VersionedSerialiserProvider : SerialiserProviderWrapperBase
{
   public VersionedSerialiserProvider(IServiceScope scope, ISerialiserProvider? innerProvider = null) : base(scope, innerProvider)
   {
      RegisterLogDataSerialisers(Scope.Registrar);
      RegisterComponentSerialisers(Scope.Registrar);

      Serialiser<IEntry, EntrySerialiser>(Scope.Registrar);
      AccountForExceptionInfo();
   }

   #region Methods
   // Hack(Nightowl): This is most definitely a hack, I need a better separation / setup;
   private void AccountForExceptionInfo()
   {
      VersionAttribute attr = typeof(ExceptionInfoSerialiser)
         .GetCustomAttribute<VersionAttribute>() ??
         throw new NotSupportedException($"This should never happen");

      Map.Add(VersionedDataKind.ExceptionInfo, attr.Version);
   }
   private void RegisterLogDataSerialisers(IServiceRegistrar registrar)
   {
      // Methods
      Serialiser<IParameterInfo, ParameterInfoSerialiser>(registrar);
      Serialiser<IMethodInfo, MethodInfoSerialiser>(registrar);
      Serialiser<IConstructorInfo, ConstructorInfoSerialiser>(registrar);

      Serialiser<IMethodBaseInfo, MethodBaseInfoSerialiserDispatcher>(registrar);

      // Stack Traces
      Serialiser<IStackFrameInfo, StackFrameInfoSerialiser>(registrar);
      Serialiser<IStackTraceInfo, StackTraceInfoSerialiser>(registrar);

      // Log Info
      Serialiser<ContextInfo, ContextInfoSerialiser>(registrar);
      Serialiser<IAssemblyInfo, AssemblyInfoSerialiser>(registrar);
      Serialiser<ITypeInfo, TypeInfoSerialiser>(registrar);
      Serialiser<ITableInfo, TableInfoSerialiser>(registrar);

      // Log References
      Serialiser<FileReference, FileReferenceSerialiser>(registrar);
      Serialiser<TagReference, TagReferenceSerialiser>(registrar);
      Serialiser<TableKeyReference, TableKeyReferenceSerialiser>(registrar);
      Serialiser<AssemblyReference, AssemblyReferenceSerialiser>(registrar);
      Serialiser<TypeReference, TypeReferenceSerialiser>(registrar);
   }
   private void RegisterComponentSerialisers(IServiceRegistrar registrar)
   {
      Serialiser<IMessageComponent, MessageComponentSerialiser>(registrar);
      Serialiser<ITagComponent, TagComponentSerialiser>(registrar);
      Serialiser<IThreadComponent, ThreadComponentSerialiser>(registrar);
      Serialiser<IEntryLinkComponent, EntryLinkComponentSerialiser>(registrar);
      Serialiser<IAssemblyComponent, AssemblyComponentSerialiser>(registrar);
      Serialiser<IStackTraceComponent, StackTraceComponentSerialiser>(registrar);
      Serialiser<ITypeComponent, TypeComponentSerialiser>(registrar);
      Serialiser<ITableComponent, TableComponentSerialiser>(registrar);
      Serialiser<IExceptionComponent, ExceptionComponentSerialiser>(registrar);

      Serialiser<IComponent, ComponentSerialiserDispatcher>(registrar);
   }

   private void Serialiser<TType, TSerialiser>(IServiceRegistrar registrar)
      where TSerialiser : notnull, ISerialiser<TType>
   {
      VersionedSingleton<ISerialiser<TType>, TSerialiser>(registrar);
   }
   private void VersionedSingleton<TService, TType>(IServiceRegistrar registrar)
     where TService : notnull
     where TType : notnull, TService
   {
      Type type = typeof(TType);
      if (typeof(TType).TryGetVersion(out uint version))
      {
         foreach (VersionedDataKind kind in type.GetDataKinds())
            Map.Add(kind, version);
      }

      registrar.Singleton<TService, TType>();
   }
   #endregion
}
