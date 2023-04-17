using System.Reflection;
using System.Text;
using TNO.Logging.Common.Abstractions.Versioning;

namespace TNO.ReadingWriting.CodeTests;

[TestClass]
[TestCategory(Category.Versioning)]
public class IVersionedTests
{
   #region Tests
   [TestMethod]
   public void EnsureIVersionedHaveVersionAttribute()
   {
      // Arrange
      Assembly[] assemblies = TestAssemblies.GetAssemblies();
      HashSet<Type> missing = new HashSet<Type>();

      // Act
      foreach (Type type in assemblies.SelectMany(a => a.DefinedTypes))
      {
         if (type.IsInterface || type.IsAbstract) continue;

         bool isVersioned = type.GetInterfaces().Contains(typeof(IVersioned));

         if (isVersioned)
         {
            bool hasVersion = type.TryGetVersion(out _);

            if (hasVersion == false)
               missing.Add(type);
         }
      }

      // Assert
      if (missing.Count > 0)
      {
         StringBuilder messageBuilder = new StringBuilder();
         messageBuilder
            .AppendLine($"Some versioned types were missing the {nameof(VersionAttribute)}.")
            .AppendLine()
            .AppendLine($"Types without a {nameof(VersionAttribute)}:");

         foreach (Type type in missing)
            messageBuilder.AppendLine($"- {type.FullName}");

         Assert.Fail(messageBuilder.ToString());
      }
   }
   #endregion
}
