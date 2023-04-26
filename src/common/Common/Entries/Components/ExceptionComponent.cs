using TNO.Logging.Common.Abstractions.Entries;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Common.Entries.Components;

/// <summary>
/// Represents a <see cref="ComponentKind.Exception"/> component.
/// </summary>
public class ExceptionComponent : IExceptionComponent
{
   #region Properties
   /// <inheritdoc/>
   public IExceptionInfo ExceptionInfo { get; }

   /// <inheritdoc/>
   public ComponentKind Kind => ComponentKind.Exception;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionComponent"/>.</summary>
   /// <param name="exceptionInfo">The information about an <see cref="Exception"/>.</param>
   public ExceptionComponent(IExceptionInfo exceptionInfo)
   {
      ExceptionInfo = exceptionInfo;
   }
   #endregion
}
