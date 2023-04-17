using System;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.LogData.Tables;

namespace TNO.Logging.Common.Abstractions.LogData.Exceptions;

/// <summary>
/// Denotes information about an <see cref="Exception"/>.
/// </summary>
public interface IExceptionInfo
{
   #region Properties
   /// <summary>The type id of the <see cref="Exception"/>.</summary>
   ulong ExceptionTypeId { get; }

   /// <summary>The type id of the <see cref="Exception"/> that was recognised by the <see cref="ExceptionGroup"/>.</summary>
   ulong ExceptionDataTypeId { get; }

   /// <summary>The id of the exception group.</summary>
   Guid ExceptionGroup { get; }

   /// <summary>The <see cref="Exception.Message"/>.</summary>
   string Message { get; }

   /// <summary>The stack trace info about the <see cref="Exception"/>.</summary>
   IStackTraceInfo StackTrace { get; }

   /// <summary>The additional data contained in the <see cref="Exception.Data"/>.</summary>
   ITableInfo AdditionalData { get; }

   /// <summary>The custom data related to the <see cref="Exception"/>.</summary>
   IExceptionData Data { get; }

   /// <summary>The <see cref="Exception.InnerException"/>.</summary>
   IExceptionInfo? InnerException { get; }
   #endregion
}
