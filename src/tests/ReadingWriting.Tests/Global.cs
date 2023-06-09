﻿global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using TNO.Tests.Common;
global using AssemblyComponentDeserialiserLatest = TNO.Logging.Reading.Entries.Components.Assembly.Versions.AssemblyComponentDeserialiser0;
global using EntryLinkComponentDeserialiserLatest = TNO.Logging.Reading.Entries.Components.EntryLink.Versions.EntryLinkComponentDeserialiser0;
global using MessageComponentDeserialiserLatest = TNO.Logging.Reading.Entries.Components.Message.Versions.MessageComponentDeserialiser0;
global using StackTraceComponentDeserialiserLatest = TNO.Logging.Reading.Entries.Components.StackTrace.Versions.StackTraceComponentDeserialiser0;
global using TableComponentDeserialiserLatest = TNO.Logging.Reading.Entries.Components.Table.Versions.TableComponentDeserialiser0;
global using TagComponentDeserialiserLatest = TNO.Logging.Reading.Entries.Components.Tag.Versions.TagComponentDeserialiser0;
global using ThreadComponentDeserialiserLatest = TNO.Logging.Reading.Entries.Components.Thread.Versions.ThreadComponentDeserialiser0;
global using TypeComponentDeserialiserLatest = TNO.Logging.Reading.Entries.Components.Type.Versions.TypeComponentDeserialiser0;
global using EntryDeserialiserLatest = TNO.Logging.Reading.Entries.Versions.EntryDeserialiser0;
global using AssemblyInfoDeserialiserLatest = TNO.Logging.Reading.LogData.AssemblyInfos.Versions.AssemblyInfoDeserialiser0;
global using AssemblyReferenceDeserialiserLatest = TNO.Logging.Reading.LogData.AssemblyReferences.Versions.AssemblyReferenceDeserialiser0;
global using ContextInfoDeserialiserLatest = TNO.Logging.Reading.LogData.ContextInfos.Versions.ContextInfoDeserialiser0;
global using FileReferenceDeserialiserLatest = TNO.Logging.Reading.LogData.FileReferences.Versions.FileReferenceDeserialiser0;
global using ConstructorInfoDeserialiserLatest = TNO.Logging.Reading.LogData.Methods.ConstructorInfos.Versions.ConstructorInfoDeserialiser0;
global using MethodInfoDeserialiserLatest = TNO.Logging.Reading.LogData.Methods.MethodInfos.Versions.MethodInfoDeserialiser0;
global using ParameterInfoDeserialiserLatest = TNO.Logging.Reading.LogData.Methods.ParameterInfos.Versions.ParameterInfoDeserialiser0;
global using StackFrameInfoDeserialiserLatest = TNO.Logging.Reading.LogData.StackTraces.StackFrameInfos.Versions.StackFrameInfoDeserialiser0;
global using StackTraceInfoDeserialiserLatest = TNO.Logging.Reading.LogData.StackTraces.StackTraceInfos.Versions.StackTraceInfoDeserialiser0;
global using TableKeyReferenceDeserialiserLatest = TNO.Logging.Reading.LogData.TableKeyReferences.Versions.TableKeyReferenceDeserialiser0;
global using TableInfoDeserialiserLatest = TNO.Logging.Reading.LogData.General.TableInfoDeserialiser;
global using TagReferenceDeserialiserLatest = TNO.Logging.Reading.LogData.TagReferences.Versions.TagReferenceDeserialiser0;
global using TypeInfoDeserialiserLatest = TNO.Logging.Reading.LogData.TypeInfos.Versions.TypeInfoDeserialiser0;
global using TypeReferenceDeserialiserLatest = TNO.Logging.Reading.LogData.TypeReferences.Versions.TypeReferenceDeserialiser0;

#if DEBUG
[assembly: Parallelize(Scope = ExecutionScope.ClassLevel, Workers = 1)]
#else
[assembly: Parallelize(Scope = ExecutionScope.MethodLevel, Workers = 0)]
#endif

internal static class Category
{
   public const string Serialisation = nameof(Serialisation);
   public const string Components = nameof(Components);
}