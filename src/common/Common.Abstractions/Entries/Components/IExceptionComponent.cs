using System;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Common.Abstractions.Entries.Components;

/// <summary>
/// Denotes a <see cref="ComponentKind.Exception"/> component.
/// </summary>
public interface IExceptionComponent : IComponent
{
   #region Properties
   /// <summary>The information about an <see cref="Exception"/>.</summary>
   IExceptionInfo ExceptionInfo { get; }
   #endregion
}
