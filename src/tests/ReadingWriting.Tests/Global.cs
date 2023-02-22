global using Microsoft.VisualStudio.TestTools.UnitTesting;
global using MessageComponentDeserialiserLatest = TNO.Logging.Reading.Entries.Components.Message.Versions.MessageComponentDeserialiser0;
global using TagComponentDeserialiserLatest = TNO.Logging.Reading.Entries.Components.Tag.Versions.TagComponentDeserialiser0;

global using EntryDeserialiserLatest = TNO.Logging.Reading.Entries.Versions.EntryDeserialiser0;
global using ContextInfoDeserialiserLatest = TNO.Logging.Reading.LogData.ContextInfos.Versions.ContextInfoDeserialiser0;
global using FileReferenceDeserialiserLatest = TNO.Logging.Reading.LogData.FileReferences.Versions.FileReferenceDeserialiser0;
global using TagReferenceDeserialiserLatest = TNO.Logging.Reading.LogData.TagReferences.Versions.TagReferenceDeserialiser0;

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