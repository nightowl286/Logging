using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.StackTrace;

namespace TNO.Logging.Reading.Entries.Components.StackTrace;

/// <summary>
/// A factory class that should be used in instances of the <see cref="IStackTraceComponentDeserialiser"/>.
/// </summary>
internal static class StackTraceComponentFactory
{
   #region Functions
   public static IStackTraceComponent Version0(IStackTraceInfo stackTraceInfo)
       => new StackTraceComponent(stackTraceInfo);
   #endregion
}
