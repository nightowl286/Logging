using TNO.Logging.Common.Exceptions.Abstractions.System;

namespace TNO.Logging.Common.Exceptions.System;

/// <summary>
/// Represents the custom data related to an <see cref="ArgumentNullException"/>.
/// </summary>
public class ArgumentNullExceptionData : ArgumentExceptionData, IArgumentNullExceptionData
{
   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ArgumentNullExceptionData"/>.</summary>
   /// <param name="parameterName">The name of the parameter that was <see langword="null"/>.</param>
   public ArgumentNullExceptionData(string? parameterName) : base(parameterName) { }
   #endregion
}
