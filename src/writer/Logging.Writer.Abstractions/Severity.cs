namespace TNO.Logging.Writer.Abstractions;

/// <summary>
/// The severity of a log entry.
/// </summary>
public enum Severity : byte
{
   /// <summary>Denotes that the entry is pointless.</summary>
   /// <remarks>Do you even need to be logging this?</remarks>
   None = byte.MinValue,

   /// <summary>Denotes that the entry is meant to provide information that will be helpful during debugging.</summary>
   Diagnostic = 32,

   /// <summary>Denotes that the entry is meant to provide information that will be helpful in tracking the execution flow.</summary>
   Trace = 96,

   /// <summary>Denotes that the importance of the entry really just depends on the context of other things.</summary>
   /// <remarks>Use this value if you don't know why you're logging something, but you do know that you need to log it.</remarks>
   Contextual = 128,

   /// <summary>Denotes that something unexpected happened, however the application did not go into a bad state.</summary>
   Warning = 160,

   /// <summary>Denotes that the application has entered a bad state, but recovery should be possible.</summary>
   Critical = 192,

   /// <summary>Denotes that the application has entered a bad state that it cannot safely recover from.</summary>
   Fatal = 224,

   /// <summary>The world has ended.</summary>
   /// <remarks>Nothing can be more severe than this.</remarks>
   GoodByeWorld = byte.MaxValue,
}
