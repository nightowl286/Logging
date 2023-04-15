namespace TNO.Logging.Common.Exceptions.Abstractions.System;

/// <summary>
/// Denotes the custom data related to an <see cref="ArgumentOutOfRangeException"/>.
/// </summary>
public interface IArgumentOutOfRangeExceptionData : IArgumentExceptionData
{
   #region Properties
   /// <summary>The argument value that caused the exception.</summary>
   object? ActualValue { get; }
   #endregion
}
