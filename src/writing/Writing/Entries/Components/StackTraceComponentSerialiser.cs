using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IStackTraceComponent"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.StackTrace)]
public sealed class StackTraceComponentSerialiser : ISerialiser<IStackTraceComponent>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackTraceComponentSerialiser"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public StackTraceComponentSerialiser(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IStackTraceComponent data)
   {
      IStackTraceInfo info = data.StackTrace;

      _serialiser.Serialise(writer, info);
   }

   /// <inheritdoc/>
   public ulong Count(IStackTraceComponent data) => _serialiser.Count(data.StackTrace);
   #endregion
}
