using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Exceptions.Abstractions.System;

namespace TNO.Logging.Common.Exceptions.System;

/// <summary>
/// Represents the custom data related to an <see cref="AggregateException"/>.
/// </summary>
public class AggregateExceptionData : IAggregateExceptionData
{
   #region Properties
   /// <inheritdoc/>
   public IReadOnlyCollection<IExceptionInfo> InnerExceptions { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="AggregateExceptionData"/>.</summary>
   /// <param name="innerExceptions">Information about the collection of the exception instances that caused the exception.</param>
   public AggregateExceptionData(IReadOnlyCollection<IExceptionInfo> innerExceptions)
   {
      InnerExceptions = innerExceptions;
   }
   #endregion
}
