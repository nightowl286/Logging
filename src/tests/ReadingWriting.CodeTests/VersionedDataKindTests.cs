using System.Reflection;
using System.Text;
using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Versioning;

namespace TNO.ReadingWriting.CodeTests;

[TestClass]
[TestCategory(Category.Versioning)]
public class VersionedDataKindTests
{
   #region Tests
   [TestMethod]
   public void VersionedDataKindHandlersHaveVersionAndDataKindAttribute()
   {
      // Arrange
      Assembly[] assemblies = TestAssemblies.GetAssemblies();
      HashSet<TypeInfo> missing = new HashSet<TypeInfo>();

      // Act
      foreach (TypeInfo typeInfo in assemblies.SelectMany(a => a.DefinedTypes))
      {
         bool hasVersion = typeInfo.IsDefined<VersionAttribute>();
         bool hasDataKind = typeInfo.IsDefined<VersionedDataKindAttribute>();

         if (hasVersion != hasDataKind)
            missing.Add(typeInfo);
      }

      // Assert
      if (missing.Count > 0)
      {
         StringBuilder messageBuilder = new StringBuilder();
         messageBuilder
            .AppendLine($"The {nameof(VersionAttribute)} should always be used with the {nameof(VersionedDataKindAttribute)}.")
            .AppendLine()
            .AppendLine($"The types that had mixed attributes were:");

         foreach (TypeInfo type in missing)
            messageBuilder.AppendLine(type.FullName);

         string message = messageBuilder.ToString();
         messageBuilder.Clear();
         Assert.Fail(message);
      }
   }
   #endregion
}
