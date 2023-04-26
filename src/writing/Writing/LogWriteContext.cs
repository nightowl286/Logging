using System.Diagnostics;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;
using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Common.IdFactories;
using TNO.Logging.Writing.Abstractions;

namespace TNO.Logging.Writing;

/// <summary>
/// Represents a context that handles information about the current logging session.
/// </summary>
public class LogWriteContext : ILogWriteContext
{
   #region Fields
   private readonly SafeIdFactory _entryIdFactory = new SafeIdFactory(1);
   private readonly SafeIdFactory<string> _fileIdFactory = new SafeIdFactory<string>(1);
   private readonly SafeIdFactory _contextIdFactory = new SafeIdFactory(1);
   private readonly SafeIdFactory<string> _tagIdFactory = new SafeIdFactory<string>(1);
   private readonly SafeIdFactory<string> _tableKeyIdFactory = new SafeIdFactory<string>(1);
   private readonly SafeIdFactory<AssemblyIdentity> _assemblyIdFactory = new SafeIdFactory<AssemblyIdentity>(1);
   private readonly SafeIdFactory<TypeIdentity> _typeIdFactory = new SafeIdFactory<TypeIdentity>(1);
   private readonly HashSet<TypeIdentity> _reportedUnknownExceptions = new HashSet<TypeIdentity>();
   private readonly Stopwatch _timestampWatch = Stopwatch.StartNew();
   #endregion

   #region Methods
   /// <inheritdoc/>
   public ulong NewEntryId() => _entryIdFactory.GetNext();

   /// <inheritdoc/>
   public TimeSpan GetTimestamp() => _timestampWatch.Elapsed;

   /// <inheritdoc/>
   public bool GetOrCreateFileId(string file, out ulong fileId) => _fileIdFactory.GetOrCreate(file, out fileId);

   /// <inheritdoc/>
   public ulong CreateContextId() => _contextIdFactory.GetNext();

   /// <inheritdoc/>
   public bool GetOrCreateTagId(string tag, out ulong tagId) => _tagIdFactory.GetOrCreate(tag, out tagId);

   /// <inheritdoc/>
   public bool GetOrCreateAssemblyId(AssemblyIdentity assemblyIdentity, out ulong assemblyId) => _assemblyIdFactory.GetOrCreate(assemblyIdentity, out assemblyId);

   /// <inheritdoc/>
   public bool GetOrCreateTypeId(TypeIdentity typeIdentity, out ulong typeId) => _typeIdFactory.GetOrCreate(typeIdentity, out typeId);

   /// <inheritdoc/>
   public bool GetOrCreateTableKeyId(string key, out uint tableKeyId)
   {
      bool isNewId = _tableKeyIdFactory.GetOrCreate(key, out ulong keyId);
      tableKeyId = (uint)keyId;

      return isNewId;
   }

   /// <inheritdoc/>
   public bool ShouldLogUnknownException(Type exceptionType)
   {
      if (typeof(Exception).IsAssignableFrom(exceptionType))
      {
         TypeIdentity identity = new TypeIdentity(exceptionType);
         return _reportedUnknownExceptions.Add(identity);
      }

      return false;
   }
   #endregion
}