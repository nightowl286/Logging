using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Entries.Components;

namespace TNO.Logging.Reading.Entries.Components.Exception;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="IExceptionComponent"/>.
/// </summary>
internal static class ExceptionComponentFactory
{
   #region Functions
   public static IExceptionComponent Version0(IExceptionInfo exceptionInfo) => new ExceptionComponent(exceptionInfo);
   #endregion
}