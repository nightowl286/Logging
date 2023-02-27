using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Reading.Abstractions.LogData.TableKeyReferences;

namespace TNO.Logging.Reading.LogData.TableKeyReferences;

/// <summary>
/// A factory that should be used in instances of the <see cref="ITableKeyReferenceDeserialiser"/>.
/// </summary>
internal static class TableKeyReferenceFactory
{
   #region Functions
   public static TableKeyReference Version0(string key, uint id)
      => new TableKeyReference(key, id);
   #endregion
}
