using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Exception"/> component.
/// </summary>
/// <param name="ExceptionInfo">The information about an <see cref="Exception"/>.</param>
public record class ExceptionComponent(IExceptionInfo ExceptionInfo) : IExceptionComponent
{
   #region Properties
   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Exception;
   #endregion
}
