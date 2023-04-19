using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.LogData.Methods;
using TNO.Logging.Common.LogData.StackTraces;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Collectors;
using BindingFlags = System.Reflection.BindingFlags;
using MethodBase = System.Reflection.MethodBase;
using MethodImplAttributes = System.Reflection.MethodImplAttributes;
using ReflectionConstructorInfo = System.Reflection.ConstructorInfo;
using ReflectionMethodInfo = System.Reflection.MethodInfo;
using ReflectionParameterInfo = System.Reflection.ParameterInfo;

namespace TNO.Logging.Logging.Helpers;

/// <summary>
/// Contains useful functions related to the <see cref="IStackTraceInfo"/>.
/// </summary>
public static class StackTraceInfoHelper
{
   #region Functions
   /// <summary>
   /// Gets the <see cref="IStackTraceInfo"/> for the given <paramref name="stackTrace"/>,
   /// along with logging all the relevant data, surrounding the stack trace and the methods.
   /// </summary>
   /// <param name="writeContext">The write context to use to generate the needed ids.</param>
   /// <param name="dataCollector">The data collector to deposit new log data to.</param>
   /// <param name="stackTrace">The <see cref="StackTrace"/> to get the info about.</param>
   /// <param name="threadId">
   /// The <see cref="Thread.ManagedThreadId"/> to associate 
   /// with the <paramref name="stackTrace"/>, <c>0</c> if unknown.
   /// </param>
   /// <returns>The generated <see cref="IStackTraceInfo"/> for the given <paramref name="stackTrace"/>.</returns>
   public static IStackTraceInfo GetStackTraceInfo(
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      StackTrace stackTrace,
      int threadId)
   {
      int frameCount = stackTrace.FrameCount;
      List<IStackFrameInfo> stackFrameInfos = new List<IStackFrameInfo>();

      for (int i = 0; i < frameCount; i++)
      {
         StackFrame? frame = stackTrace.GetFrame(i);
         MethodBase? methodBase = frame?.GetMethod();

         bool shouldSkip =
            methodBase is null ||
            ShouldShowInStackTrace(methodBase) == false && i < frameCount - 1;

         if (shouldSkip) continue;
         Debug.Assert(methodBase is not null && frame is not null);

         IStackFrameInfo frameInfo = GetStackFrameInfo(writeContext, dataCollector, frame, methodBase);
         stackFrameInfos.Add(frameInfo);
      }

      return new StackTraceInfo(threadId, stackFrameInfos);
   }

   private static IStackFrameInfo GetStackFrameInfo(
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      StackFrame frame, MethodBase methodBase)
   {
      ulong fileId = 0;
      uint line = 0;
      uint column = 0;

      string? fileName = frame.GetFileName();
      if (fileName is not null && writeContext.GetOrCreateFileId(fileName, out fileId))
      {
         FileReference fileReference = new FileReference(fileName, fileId);
         dataCollector.Deposit(fileReference);
      }

      if (methodBase.IsInOptimisedAssembly() == false)
      {
         line = ConvertToUInt(frame.GetFileLineNumber());
         column = ConvertToUInt(frame.GetFileColumnNumber());
      }

      MethodBase mainMethod;
      MethodBase? secondaryMethod;
      bool foundNewMain = TryFindMainMethod(methodBase, out MethodBase? resolvedMainMethod);

      if (foundNewMain)
      {
         Debug.Assert(resolvedMainMethod is not null);

         mainMethod = resolvedMainMethod;
         secondaryMethod = methodBase;
      }
      else
      {
         mainMethod = methodBase;
         secondaryMethod = null;
      }

      IMethodBaseInfo mainMethodInfo = GetMethodBaseInfo(writeContext, dataCollector, mainMethod);
      IMethodBaseInfo? secondaryMethodInfo = secondaryMethod is null ? null : GetMethodBaseInfo(writeContext, dataCollector, secondaryMethod);

      return new StackFrameInfo(
         fileId,
         line,
         column,
         mainMethodInfo,
         secondaryMethodInfo);
   }

   private static bool TryFindMainMethod(
      MethodBase possibleSecondaryMethod,
      [NotNullWhen(true)] out MethodBase? mainMethod)
   {
      mainMethod = null;

      Type? declaringType = possibleSecondaryMethod.DeclaringType;
      if (declaringType is null || declaringType.IsDefined<CompilerGeneratedAttribute>(false) == false)
         return false;

      bool isTypeStateMachine =
         declaringType.IsAssignableTo(typeof(IAsyncStateMachine)) ||
         declaringType.IsAssignableTo(typeof(IEnumerator));

      if (isTypeStateMachine == false) return false;

      Type? parentType = declaringType.DeclaringType;
      if (parentType is null) return false;

      // Note(Nightowl): Flags copied from StackTrace.TryResolveStateMachineMethod(ref MethodBase, out Type)
      // https://source.dot.net/#System.Private.CoreLib/src/libraries/System.Private.CoreLib/src/System/Diagnostics/StackTrace.cs,9e4adfc22edb9ff2,references;
      ReflectionMethodInfo[] declaredMethods = parentType.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.DeclaredOnly);

      foreach (ReflectionMethodInfo candidateMethod in declaredMethods)
      {
         IEnumerable<StateMachineAttribute> attributes = System.Reflection.CustomAttributeExtensions.GetCustomAttributes<StateMachineAttribute>(candidateMethod, false);

         foreach (StateMachineAttribute attribute in attributes)
         {
            if (attribute.StateMachineType == declaringType)
            {
               mainMethod = candidateMethod;
               return true;
            }
         }
      }

      return false;

   }

   private static uint ConvertToUInt(int num)
   {
      if (num < 0) return 0;

      return (uint)num;
   }

   private static IMethodBaseInfo GetMethodBaseInfo(
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      MethodBase methodBase)
   {
      if (methodBase is ReflectionMethodInfo reflectionMethodInfo)
         return GetMethodInfo(writeContext, dataCollector, reflectionMethodInfo);
      else if (methodBase is ReflectionConstructorInfo reflectionConstructorInfo)
         return GetConstructorInfo(writeContext, dataCollector, reflectionConstructorInfo);

      throw new ArgumentException($"Unknown method base type ({methodBase.GetType()}).", nameof(methodBase));
   }

   private static IMethodInfo GetMethodInfo(
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      ReflectionMethodInfo reflectionMethodInfo)
   {
      ulong declaringTypeId = 0;
      ulong returnTypeId = 0;

      if (reflectionMethodInfo.DeclaringType is not null)
         declaringTypeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(writeContext, dataCollector, reflectionMethodInfo.DeclaringType);

      if (reflectionMethodInfo.ReturnType is not null && reflectionMethodInfo.ReturnType != typeof(void))
         returnTypeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(writeContext, dataCollector, reflectionMethodInfo.ReturnType);

      ReflectionParameterInfo[] reflectionParameters = reflectionMethodInfo.GetParameters();
      IParameterInfo[] parameterInfos = new IParameterInfo[reflectionParameters.Length];
      for (int i = 0; i < reflectionParameters.Length; i++)
      {
         IParameterInfo parameterInfo = GetParameterInfo(writeContext, dataCollector, reflectionParameters[i]);
         parameterInfos[i] = parameterInfo;
      }

      ulong[] genericTypeIds;
      if (reflectionMethodInfo.IsGenericMethod)
      {
         Type[] genericTypes = reflectionMethodInfo.GetGenericArguments();
         genericTypeIds = new ulong[genericTypes.Length];
         for (int i = 0; i < genericTypes.Length; i++)
         {
            ulong genericTypeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(writeContext, dataCollector, genericTypes[i]);
            genericTypeIds[i] = genericTypeId;
         }
      }
      else
         genericTypeIds = Array.Empty<ulong>();

      MethodInfo methodInfo = MethodInfo.FromReflectionMethodInfo(declaringTypeId, parameterInfos, returnTypeId, genericTypeIds, reflectionMethodInfo);
      return methodInfo;
   }

   private static IConstructorInfo GetConstructorInfo(
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      ReflectionConstructorInfo reflectionConstructorInfo)
   {
      ulong declaringTypeId = 0;
      if (reflectionConstructorInfo.DeclaringType is not null)
         declaringTypeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(writeContext, dataCollector, reflectionConstructorInfo.DeclaringType);

      ReflectionParameterInfo[] reflectionParameters = reflectionConstructorInfo.GetParameters();
      IParameterInfo[] parameterInfos = new IParameterInfo[reflectionParameters.Length];
      for (int i = 0; i < reflectionParameters.Length; i++)
      {
         IParameterInfo parameterInfo = GetParameterInfo(writeContext, dataCollector, reflectionParameters[i]);
         parameterInfos[i] = parameterInfo;
      }

      ConstructorInfo constructorInfo = ConstructorInfo.FromReflectionConstructorInfo(declaringTypeId, parameterInfos, reflectionConstructorInfo);
      return constructorInfo;
   }

   private static IParameterInfo GetParameterInfo(
      ILogWriteContext writeContext,
      ILogDataCollector dataCollector,
      ReflectionParameterInfo reflectionParameterInfo)
   {
      ulong parameterTypeId = TypeInfoHelper.EnsureIdsForAssociatedTypes(writeContext, dataCollector, reflectionParameterInfo.ParameterType);

      ParameterInfo parameterInfo = ParameterInfo.FromReflectionParameterInfo(parameterTypeId, reflectionParameterInfo);
      return parameterInfo;
   }

   private static bool ShouldShowInStackTrace(MethodBase methodBase)
   {
      // Note(Nightowl): Implementation copied over from StackTrace.ShowInStackTrace(MethodBase)
      // https://source.dot.net/#System.Private.CoreLib/src/libraries/System.Private.CoreLib/src/System/Diagnostics/StackTrace.cs,0b3723fb6fb382f1;

      if (methodBase.MethodImplementationFlags.HasFlag(MethodImplAttributes.AggressiveInlining))
         return false;

      try
      {
         if (methodBase.IsDefined<StackTraceHiddenAttribute>(false))
            return false;

         Type? declaringType = methodBase.DeclaringType;

         if (declaringType?.IsDefined<StackTraceHiddenAttribute>(false) == true)
            return false;
      }
      catch { }

      return true;
   }
   #endregion
}
