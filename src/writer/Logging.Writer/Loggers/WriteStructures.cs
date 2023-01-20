namespace TNO.Logging.Writer.Loggers;

internal record struct Tag(string Name, ulong Id);
internal record struct Links(ulong ContextId, ulong FileRef, int Line, ulong[] Ids);
internal record struct FileReference(string FilePath, ulong Id);
internal record struct Context(string Name, ulong Id, ulong Parent);