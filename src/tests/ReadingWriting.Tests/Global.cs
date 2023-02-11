global using Microsoft.VisualStudio.TestTools.UnitTesting;

global using MessageComponentDeserialiserLatest = TNO.Logging.Reading.Entries.Components.Message.Versions.MessageComponentDeserialiser0;
global using EntryDeserialiserLatest = TNO.Logging.Reading.Entries.Versions.EntryDeserialiser0;
global using FileReferenceDeserialiserLatest = TNO.Logging.Reading.FileReferences.Versions.FileReferenceDeserialiser0;

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