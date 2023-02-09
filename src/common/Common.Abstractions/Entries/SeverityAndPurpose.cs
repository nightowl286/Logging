namespace TNO.Logging.Common.Abstractions.Entries;

/// <summary>
/// The severity and purpose of a log entry.
/// </summary>
public enum SeverityAndPurpose : byte
{
   /// <summary>The default value for this enum, logically equivalent to <see cref="None"/>.</summary>
   /// <remarks>Please don't log this, this has only been included to help in comparisons.</remarks>
   Empty = 0,

   #region Severity
   /// <summary>No severity has been selected.</summary>
   NoSeverity = 1,

   /// <summary>Not really important, but it should be able to help somewhere.</summary>
   Negligible = 2,

   /// <summary>Ought to be investigated, but the application should continue working.</summary>
   Substantial = 6,

   /// <summary>Something happened when it shouldn't have.</summary>
   /// <remarks>Probably shouldn't ignore this.</remarks>
   Critical = 8,

   /// <summary>The application might be in a bad state. (Or should be closed in order to preserve/not corrupt data).</summary>
   /// <remarks>This needs to be fixed.</remarks>
   Fatal = 14,

   /// <summary>Inherit the severity from the linked entry.</summary>
   InheritSeverity = 15,
   #endregion

   #region Purpose
   /// <summary>No purpose has been selected.</summary>
   NoPurpose = 16,

   /// <summary>Should help in understanding how the application is used.</summary>
   Telemetry = 32,

   /// <summary>Should help in tracking the execution flow of the application.</summary>
   Tracing = 48,

   /// <summary>Should help when debugging the application.</summary>
   Diagnostics = 64,

   /// <summary>Should help in analysing the performance of the application.</summary>
   Performance = 80,

   /// <summary>Inherit the purpose from the linked entry.</summary>
   InheritPurpose = 240,
   #endregion

   #region Common Combinations
   /// <summary>No severity or purpose has been selected.</summary>
   /// <remarks>Why are you logging this?</remarks>
   None = NoSeverity | NoPurpose,  // 17

   /// <summary>Inherit the severity and purpose from the linked entry.</summary>
   Inherit = InheritSeverity | InheritPurpose, // 255
   #endregion
}
