using TNO.Logging.Common.Exceptions.Abstractions.System;

namespace TNO.Logging.Common.Exceptions.System;

/// <summary>
/// Represents the custom data related to an <see cref="ArgumentException"/>.
/// </summary>
public class ArgumentExceptionData : IArgumentExceptionData
{
   #region Properties
   /// <inheritdoc/>
   public string? ParameterName { get; }
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ArgumentExceptionData"/>.</summary>
   /// <param name="parameterName">The name of the parameter that caused the exception.</param>
   public ArgumentExceptionData(string? parameterName)
   {
      ParameterName = parameterName;
   }
   #endregion
}
