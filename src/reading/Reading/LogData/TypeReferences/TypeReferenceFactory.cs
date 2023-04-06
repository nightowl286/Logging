using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Reading.Abstractions.LogData.TypeReferences;

namespace TNO.Logging.Reading.LogData.TypeReferences;

/// <summary>
/// A factory that should be used in instances of the see <see cref="ITypeReferenceDeserialiser"/>.
/// </summary>
internal static class TypeReferenceFactory
{
   #region Functions
   public static TypeReference Version0(ulong id, ITypeInfo assemblyInfo) => new TypeReference(assemblyInfo, id);
   #endregion
}
