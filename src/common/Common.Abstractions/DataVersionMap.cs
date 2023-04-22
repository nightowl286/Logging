using System.Collections.Generic;
using TNO.Logging.Common.Abstractions.DataKinds;

namespace TNO.Logging.Common.Abstractions;

/// <summary>
/// Represents a map of data kinds and an associated version.
/// </summary>
public class DataVersionMap : HashSet<DataKindVersion>
{
   #region Methods
   /// <summary>Tries to get a <paramref name="version"/> for the given <paramref name="dataKind"/>.</summary>
   /// <param name="dataKind">The <see cref="VersionedDataKind"/> to try and get a <paramref name="version"/> for.</param>
   /// <param name="version">The version of the given <paramref name="dataKind"/>, or the default value for <see cref="uint"/>.</param>
   /// <returns><see langword="true"/> if a <paramref name="version"/> could be found, <see langword="false"/> otherwise.</returns>
   public bool TryGetVersion(VersionedDataKind dataKind, out uint version)
   {
      foreach (DataKindVersion dataKindVersion in this)
      {
         if (dataKindVersion.DataKind == dataKind)
         {
            version = dataKindVersion.Version;
            return true;
         }
      }

      version = default;
      return false;
   }
   #endregion
}