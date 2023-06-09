﻿using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;

namespace TNO.Logging.Reading.Entries.Components.Thread.Versions;

/// <summary>
/// A deserialiser for <see cref="IThreadComponent"/>, version #0.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.Thread)]
public sealed class ThreadComponentDeserialiser0 : IDeserialiser<IThreadComponent>
{
   #region Methods
   /// <inheritdoc/>
   public IThreadComponent Deserialise(BinaryReader reader)
   {
      int id = reader.ReadInt32();
      string name = reader.ReadString();
      bool isThreadPool = reader.ReadBoolean();

      ushort rawState = reader.ReadUInt16();
      byte rawPriority = reader.ReadByte();
      byte rawApartmentState = reader.ReadByte();

      ThreadState state = (ThreadState)rawState;
      ThreadPriority priority = (ThreadPriority)rawPriority;
      ApartmentState apartmentState = (ApartmentState)rawApartmentState;

      return ThreadComponentFactory.Version0(
         id,
         name,
         state,
         isThreadPool,
         priority,
         apartmentState);
   }
   #endregion
}
