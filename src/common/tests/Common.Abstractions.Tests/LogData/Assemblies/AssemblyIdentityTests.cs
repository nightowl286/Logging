using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using TNO.Logging.Common.Abstractions.LogData.Assemblies;

namespace Common.Abstractions.Tests.LogData.Assemblies;

[TestClass]
public class AssemblyIdentityTests
{
   public record class AnnotatedIdentity(AssemblyIdentity Identity, string Annotation);

   #region Tests
   [DynamicData(nameof(GetEqualValuesData), DynamicDataSourceType.Method,
      DynamicDataDisplayName = nameof(GetAnnotatedTestMethodName))]
   [TestMethod]
   public void Equals_EqualValues_ReturnsTrue(AnnotatedIdentity annotatedA, AnnotatedIdentity annotatedB)
   {
      // Arrange
      AssemblyIdentity idA = annotatedA.Identity;
      AssemblyIdentity idB = annotatedB.Identity;

      // Act
      bool result = idA.Equals(idB);

      // Assert
      Assert.IsTrue(result);
   }

   [DynamicData(nameof(GetEqualValuesData), DynamicDataSourceType.Method,
      DynamicDataDisplayName = nameof(GetAnnotatedTestMethodName))]
   [TestMethod]
   public void EqualityOperator_EqualValues_ReturnsTrue(AnnotatedIdentity annotatedA, AnnotatedIdentity annotatedB)
   {
      // Arrange
      AssemblyIdentity idA = annotatedA.Identity;
      AssemblyIdentity idB = annotatedB.Identity;

      // Act
      bool result = idA == idB;

      // Assert
      Assert.IsTrue(result);
   }

   [DynamicData(nameof(GetEqualValuesData), DynamicDataSourceType.Method,
      DynamicDataDisplayName = nameof(GetAnnotatedTestMethodName))]
   [TestMethod]
   public void InequalityOperator_EqualValues_ReturnsFalse(AnnotatedIdentity annotatedA, AnnotatedIdentity annotatedB)
   {
      // Arrange
      AssemblyIdentity idA = annotatedA.Identity;
      AssemblyIdentity idB = annotatedB.Identity;

      // Act
      bool result = idA != idB;

      // Assert
      Assert.IsFalse(result);
   }

   [DynamicData(nameof(GetEqualValuesData), DynamicDataSourceType.Method,
      DynamicDataDisplayName = nameof(GetAnnotatedTestMethodName))]
   [TestMethod]
   public void GetHashCode_EqualValues_SameHashCode(AnnotatedIdentity annotatedA, AnnotatedIdentity annotatedB)
   {
      // Arrange
      AssemblyIdentity idA = annotatedA.Identity;
      AssemblyIdentity idB = annotatedB.Identity;

      // Pre-Act Assert
      Assert.That.IsInconclusiveIfNot(idA.Equals(idB),
         $"The values were not equivalent, it is likely that either the test data broke or the test '{nameof(Equals_EqualValues_ReturnsTrue)}' has also broken.");

      // Act
      int hashCodeA = idA.GetHashCode();
      int hashCodeB = idB.GetHashCode();

      // Assert
      Assert.AreEqual(hashCodeA, hashCodeB);
   }

   [DynamicData(nameof(GetDifferentValuesData), DynamicDataSourceType.Method,
      DynamicDataDisplayName = nameof(GetAnnotatedTestMethodName))]
   [TestMethod]
   public void Equals_DifferentValues_ReturnsFalse(AnnotatedIdentity annotatedA, AnnotatedIdentity annotatedB)
   {
      // Arrange
      AssemblyIdentity idA = annotatedA.Identity;
      AssemblyIdentity idB = annotatedB.Identity;

      // Act
      bool result = idA.Equals(idB);

      // Assert
      Assert.IsFalse(result);
   }

   [DynamicData(nameof(GetDifferentValuesData), DynamicDataSourceType.Method,
      DynamicDataDisplayName = nameof(GetAnnotatedTestMethodName))]
   [TestMethod]
   public void EqualityOperator_DifferentValues_ReturnsFalse(AnnotatedIdentity annotatedA, AnnotatedIdentity annotatedB)
   {
      // Arrange
      AssemblyIdentity idA = annotatedA.Identity;
      AssemblyIdentity idB = annotatedB.Identity;

      // Act
      bool result = idA == idB;

      // Assert
      Assert.IsFalse(result);
   }

   [DynamicData(nameof(GetDifferentValuesData), DynamicDataSourceType.Method,
      DynamicDataDisplayName = nameof(GetAnnotatedTestMethodName))]
   [TestMethod]
   public void InequalityOperator_DifferentValues_ReturnsTrue(AnnotatedIdentity annotatedA, AnnotatedIdentity annotatedB)
   {
      // Arrange
      AssemblyIdentity idA = annotatedA.Identity;
      AssemblyIdentity idB = annotatedB.Identity;

      // Act
      bool result = idA != idB;

      // Assert
      Assert.IsTrue(result);
   }
   #endregion

   #region Test Data
   public static IEnumerable<object[]> GetEqualValuesData()
   {
      foreach (AnnotatedIdentity id in GetUniqueValues())
      {
         yield return new object[] { id, id };
      }
   }
   public static IEnumerable<object[]> GetDifferentValuesData()
   {
      foreach ((AnnotatedIdentity a, AnnotatedIdentity b) in GetNonEqualValues())
      {
         yield return new object[] { a, b };
      }
   }

   public static string GetAnnotatedTestMethodName(MethodInfo methodInfo, object[] values)
   {
      AnnotatedIdentity valA = (AnnotatedIdentity)values[0];
      AnnotatedIdentity valB = (AnnotatedIdentity)values[1];

      return $"{methodInfo.Name}({valA.Annotation}, {valB.Annotation})";
   }
   #endregion

   #region Helpers
   private static IEnumerable<AnnotatedIdentity> GetUniqueValues()
   {
      string cultureName = CultureInfo.CurrentCulture.Name;
      Version version = new Version(1, 2, 3, 4);
      byte[] token = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
      string name = "name";
      string location = "location";

      // all data
      yield return GetIdentity("all data", name, cultureName, location, version, token);

      // no version
      yield return GetIdentity("no version", name, cultureName, location, null, token);

      // no culture
      yield return GetIdentity("no culture", name, null, location, version, token);

      // no token
      yield return GetIdentity("no token", name, cultureName, location, version, null);

      // location empty
      yield return GetIdentity("location empty", name, cultureName, string.Empty, version, token);

      // no name
      yield return GetIdentity("no name", null, cultureName, location, version, token);
   }

   private static IEnumerable<(AnnotatedIdentity, AnnotatedIdentity)> GetNonEqualValues()
   {
      string cultureName = CultureInfo.CurrentCulture.Name;
      Version version = new Version(1, 2, 3, 4);
      byte[] token = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
      string name = "name";
      string location = "location";

      AnnotatedIdentity allData = GetIdentity("all data", name, cultureName, location, version, token);
      AnnotatedIdentity noVersion = GetIdentity("no version", name, cultureName, location, null, token);
      AnnotatedIdentity noCulture = GetIdentity("no culture", name, null, location, version, token);
      AnnotatedIdentity noToken = GetIdentity("no token", name, cultureName, location, version, null);
      AnnotatedIdentity locationEmpty = GetIdentity("location empty", name, cultureName, string.Empty, version, token);
      AnnotatedIdentity noName = GetIdentity("no name", null, cultureName, location, version, token);

      AnnotatedIdentity[] data = new AnnotatedIdentity[] { allData, noVersion, noCulture, noToken, locationEmpty, noName };

      for (int i = 0; i < data.Length; i++)
      {
         for (int j = i + 1; j < data.Length; j++)
         {
            AnnotatedIdentity a = data[i];
            AnnotatedIdentity b = data[j];

            yield return (a, b);
         }
      }
   }

   private static AnnotatedIdentity GetIdentity(string annotation, string? name, string? cultureName, string location, Version? version, byte[]? token)
   {
      Debug.WriteLine($"Creating ({annotation}) with {name}, {cultureName}, {location}, {version}");
      AssemblyName assemblyName = new AssemblyName()
      {
         Name = name,
         CultureName = cultureName,
         Version = version
      };
      assemblyName.SetPublicKeyToken(token);

      AssemblyIdentity identity = new AssemblyIdentity(assemblyName, location);
      AnnotatedIdentity annotated = new AnnotatedIdentity(identity, annotation);

      Debug.WriteLine($"Done creating ({annotation}) with {name}, {cultureName}, {location}, {version}");
      return annotated;
   }
   #endregion
}
