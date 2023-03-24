using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Reading.Abstractions.Entries.Components.SimpleStackTrace;

namespace TNO.Logging.Reading.Entries.Components.SimpleStackTrace;

/// <summary>
/// A factory class that should be used in instances of the <see cref="ISimpleStackTraceComponentDeserialiser"/>.
/// </summary>
internal static class SimpleStackTraceComponentFactory
{
   #region Functions
   public static ISimpleStackTraceComponent Version0(string stackTrace, int threadId)
       => new SimpleStackTraceComponent(stackTrace, threadId);
   #endregion
}
