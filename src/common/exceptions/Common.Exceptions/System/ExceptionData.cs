using TNO.Logging.Common.Abstractions.LogData.Exceptions;

namespace TNO.Logging.Common.Exceptions.System;

/// <summary>
/// Represents the custom data related to an <see cref="Exception"/>.
/// </summary>
public record class ExceptionData : IExceptionData
{
   #region Properties
   /// <summary>The reusable, empty instance of <see cref="ExceptionData"/>.</summary>
   public static ExceptionData Empty { get; } = new ExceptionData();
   #endregion
}
