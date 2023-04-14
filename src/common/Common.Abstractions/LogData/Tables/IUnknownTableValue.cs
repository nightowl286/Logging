using System;

namespace TNO.Logging.Common.Abstractions.LogData.Tables;

/// <summary>
/// Denotes info about an unknown table value.
/// </summary>
public interface IUnknownTableValue
{
   #region Properties
   /// <summary>The type of the <see cref="Type"/> that the value was of.</summary>
   /// <remarks>This id is only used within the log.</remarks>
   ulong TypeId { get; }
   #endregion
}
