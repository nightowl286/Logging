namespace TNO.Logging.Common.Exceptions.Abstractions.System;

/// <summary>
/// Denotes the custom data related to an <see cref="ArgumentException"/>.
/// </summary>
public interface IArgumentExceptionData : IExceptionData
{
   #region Properties
   /// <summary>The name of the parameter that caused the exception.</summary>
   string? ParameterName { get; }
   #endregion
}
