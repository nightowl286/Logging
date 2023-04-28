using System.Diagnostics;
using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.LogData;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using AssemblyReference = TNO.Logging.Common.Abstractions.LogData.Assemblies.AssemblyReference;
using TypeInfo = TNO.Logging.Common.LogData.TypeInfo;

namespace TNO.Logging.Helpers;

/// <summary>
/// Contains useful functions related to the <see cref="ITypeInfo"/>.
/// </summary>
public static class TypeInfoHelper
{
   #region Functions
   /// <summary>Ensures that the given <paramref name="type"/>, and any associated types have been saved in the log.</summary>
   /// <param name="writeContext">The write context to use to generate the needed ids.</param>
   /// <param name="dataCollector">The data collector to deposit new log data to.</param>
   /// <param name="type">The type (and its associated ids) to save.</param>
   /// <returns>The id for the given <paramref name="type"/>.</returns>
   public static ulong EnsureIdsForAssociatedTypes(
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      Type type)
   {
      ulong assemblyId = EnsureIdForAssembly(writeContext, dataCollector, type.Assembly);
      ulong baseTypeId = 0;
      ulong declaringTypeId = 0;
      ulong elementTypeId = 0;
      ulong genericTypeDefinitionId = 0;

      List<ulong> genericTypeIds = new List<ulong>();

      if (type.BaseType is not null)
         baseTypeId = EnsureIdsForAssociatedTypes(writeContext, dataCollector, type.BaseType);

      if (type.DeclaringType is not null)
         declaringTypeId = EnsureIdsForAssociatedTypes(writeContext, dataCollector, type.DeclaringType);

      if (type.HasElementType)
      {
         Type? elementType = type.GetElementType();
         Debug.Assert(elementType is not null);
         elementTypeId = EnsureIdsForAssociatedTypes(writeContext, dataCollector, elementType);
      }

      if (type.IsConstructedGenericType)
      {
         Type genericTypeDefinition = type.GetGenericTypeDefinition();
         genericTypeDefinitionId = EnsureIdsForAssociatedTypes(writeContext, dataCollector, genericTypeDefinition);
      }

      if (type.IsGenericType)
      {
         foreach (Type genericType in type.GenericTypeArguments)
         {
            ulong genericTypeId = EnsureIdsForAssociatedTypes(writeContext, dataCollector, genericType);
            genericTypeIds.Add(genericTypeId);
         }
      }

      TypeIdentity identity = new TypeIdentity(type);
      if (writeContext.GetOrCreateTypeId(identity, out ulong typeId))
      {
         ITypeInfo typeInfo = TypeInfo.FromType(assemblyId, declaringTypeId, baseTypeId, elementTypeId, genericTypeDefinitionId, genericTypeIds, type);
         TypeReference typeReference = new TypeReference(typeInfo, typeId);

         dataCollector.Deposit(typeReference);
      }

      return typeId;
   }
   #endregion

   #region Helpers
   private static ulong EnsureIdForAssembly(
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      Assembly assembly)
   {
      AssemblyIdentity identity = new AssemblyIdentity(assembly);
      if (writeContext.GetOrCreateAssemblyId(identity, out ulong assemblyId))
      {
         IAssemblyInfo info = AssemblyInfo.FromAssembly(assembly);
         AssemblyReference assemblyReference = new AssemblyReference(info, assemblyId);

         dataCollector.Deposit(assemblyReference);
      }

      return assemblyId;
   }
   #endregion
}
