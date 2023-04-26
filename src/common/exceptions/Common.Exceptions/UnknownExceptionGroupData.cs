using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Common.Exceptions;

/// <summary>
/// Represents that the <see cref="IExceptionData"/> came from an unknown exception group.
/// </summary>
public sealed class UnknownExceptionGroupData : IExceptionData
{
   #region Properties
   /// <inheritdoc/>
   public Guid ExceptionGroupId { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="UnknownExceptionGroupData"/>.</summary>
   /// <param name="exceptionGroupId">The <see cref="Guid"/> that represents the exception group.</param>
   public UnknownExceptionGroupData(Guid exceptionGroupId)
   {
      ExceptionGroupId = exceptionGroupId;
   }
   #endregion
}
