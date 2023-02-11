﻿using TNO.Logging.Common.Abstractions;
using TNO.Logging.Reading.Abstractions.FileReferences;

namespace TNO.Logging.Reading.FileReferences.Versions;

/// <summary>
/// A deserialiser for <see cref="FileReference"/>, version #0.
/// </summary>
public sealed class FileReferenceDeserialiser0 : IFileReferenceDeserialiser
{
   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Methods
   /// <inheritdoc/>
   public FileReference Deserialise(BinaryReader reader)
   {
      string file = reader.ReadString();
      ulong id = reader.ReadUInt64();

      return FileReferenceFactory.Version0(file, id);
   }
   #endregion
}
