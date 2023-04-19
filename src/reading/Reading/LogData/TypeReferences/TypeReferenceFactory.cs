using TNO.Logging.Common.Abstractions.LogData.Types;

namespace TNO.Logging.Reading.LogData.TypeReferences;

/// <summary>
/// A factory class that should be used in deserialisers for <see cref="TypeReference"/>.
/// </summary>
internal static class TypeReferenceFactory
{
   #region Functions
   public static TypeReference Version0(ulong id, ITypeInfo assemblyInfo) => new TypeReference(assemblyInfo, id);
   #endregion
}
