using System.Collections.Generic;

namespace TNO.Logging.Common.Exceptions.Abstractions.System;

/// <summary>
/// Denotes the custom data related to an <see cref="AggregateException"/>.
/// </summary>
public interface IAggregateExceptionData : IExceptionData
{
   #region Properties
   /// <summary>Information about the collection of exception instances that caused the exception.</summary>
   IReadOnlyCollection<IExceptionInfo> InnerExceptions { get; }
   #endregion
}