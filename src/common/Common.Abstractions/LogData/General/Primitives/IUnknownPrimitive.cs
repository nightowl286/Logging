using System;

namespace TNO.Logging.Common.Abstractions.LogData.General.Primitives;

/// <summary>
/// Denotes info about an unknown primitive value.
/// </summary>
public interface IUnknownPrimitive
{
   #region Properties
   /// <summary>The type id of the <see cref="Type"/> that the value was of.</summary>
   /// <remarks>This id is only used within the log.</remarks>
   ulong TypeId { get; }
   #endregion
}
