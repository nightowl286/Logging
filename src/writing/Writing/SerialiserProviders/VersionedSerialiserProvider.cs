using System.Reflection;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions.Entries.Components;
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

      VersionedSingleton<IEntrySerialiser, EntrySerialiser>(Scope.Registrar);
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
      VersionedSingleton<IParameterInfoSerialiser, ParameterInfoSerialiser>(registrar);
      VersionedSingleton<IMethodInfoSerialiser, MethodInfoSerialiser>(registrar);
      VersionedSingleton<IConstructorInfoSerialiser, ConstructorInfoSerialiser>(registrar);

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
      VersionedSingleton<IExceptionComponentSerialiser, ExceptionComponentSerialiser>(registrar);
   }
   private void VersionedSingleton<TService, TType>(IServiceRegistrar registrar)
     where TService : notnull
     where TType : notnull, TService, IVersioned
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
