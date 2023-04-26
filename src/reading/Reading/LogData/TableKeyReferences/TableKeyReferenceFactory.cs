using TNO.Logging.Common.Abstractions.LogData.Primitives;

namespace TNO.Logging.Reading.LogData.TableKeyReferences;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="TableKeyReference"/>.
/// </summary>
internal static class TableKeyReferenceFactory
{
   #region Functions
   public static TableKeyReference Version0(string key, uint id)
      => new TableKeyReference(key, id);
   #endregion
}
