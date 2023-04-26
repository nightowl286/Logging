using TNO.Logging.Common.Exceptions.Abstractions.System;

namespace TNO.Logging.Common.Exceptions.System;

/// <summary>
/// Represents the custom data related to an <see cref="ArgumentNullException"/>.
/// </summary>
public class ArgumentOutOfRangeExceptionData : ArgumentExceptionData, IArgumentOutOfRangeExceptionData
{
   #region Properties
   /// <inheritdoc/>
   public object? ActualValue { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ArgumentOutOfRangeExceptionData"/>.</summary>
   /// <param name="parameterName">The name of the parameter that caused the exception.</param>
   /// <param name="actualValue">The argument value that caused the exception.</param>
   public ArgumentOutOfRangeExceptionData(string? parameterName, object? actualValue) : base(parameterName)
   {
      ActualValue = actualValue;
   }
   #endregion
}
