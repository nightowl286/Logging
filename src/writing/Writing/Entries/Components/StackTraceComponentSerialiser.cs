using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.StackTraces;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IStackTraceComponent"/>.
/// </summary>
[Version(0)]
public sealed class StackTraceComponentSerialiser : IStackTraceComponentSerialiser
{
   #region Fields
   private readonly IStackTraceInfoSerialiser _stackTraceInfoSerialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackTraceComponentSerialiser"/>.</summary>
   /// <param name="stackTraceInfoSerialiser">The <see cref="IStackTraceInfoSerialiser"/> to use.</param>
   public StackTraceComponentSerialiser(IStackTraceInfoSerialiser stackTraceInfoSerialiser)
   {
      _stackTraceInfoSerialiser = stackTraceInfoSerialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IStackTraceComponent data)
   {
      IStackTraceInfo info = data.StackTrace;

      _stackTraceInfoSerialiser.Serialise(writer, info);
   }

   /// <inheritdoc/>
   public ulong Count(IStackTraceComponent data) => _stackTraceInfoSerialiser.Count(data.StackTrace);
   #endregion
}
