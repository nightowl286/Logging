using System.Reflection;
using TNO.Common.Abstractions;

namespace Common.Abstractions.CodeTests;

[TestClass]
[TestCategory(Category.SeverityAndPurpose)]
public class SeverityAndPurposeCodeTests
{
   #region Consts
   private const byte SeverityBitMask = Severity.BitMask;
   private const byte PurposeBitMask = Purpose.BitMask;
   #endregion

   #region Test Methods
   [TestMethod]
   [TestCategory(Category.Severity)]
   public void Severity_HasCorrectProperties()
      => VerifyCorrectPropertiesIncluded(typeof(Severity), SeverityBitMask, PurposeBitMask, SeverityAndPurpose.NoSeverity);

   [TestMethod]
   [TestCategory(Category.Severity)]
   public void Severity_ExcludesCorrectProperties()
      => VerifyBlacklistedPropertiesAreExcluded(typeof(Severity), SeverityBitMask, PurposeBitMask, SeverityAndPurpose.NoSeverity);

   [TestMethod]
   [TestCategory(Category.Purpose)]
   public void Purpose_HasCorrectProperties()
      => VerifyCorrectPropertiesIncluded(typeof(Purpose), PurposeBitMask, SeverityBitMask, SeverityAndPurpose.NoPurpose);

   [TestMethod]
   [TestCategory(Category.Purpose)]
   public void Purpose_ExcludesCorrectProperties()
      => VerifyBlacklistedPropertiesAreExcluded(typeof(Purpose), PurposeBitMask, SeverityBitMask, SeverityAndPurpose.NoPurpose);


   private void VerifyCorrectPropertiesIncluded(Type helperClassType, byte includeMask, byte excludeMask, params SeverityAndPurpose[] blacklist)
   {
      PropertyInfo[] properties = GetProperties(helperClassType);

      HashSet<SeverityAndPurpose> required = GetValuesToInclude(includeMask, excludeMask, blacklist);
      HashSet<SeverityAndPurpose> missing = new HashSet<SeverityAndPurpose>(required);

      foreach (PropertyInfo propertyInfo in properties)
      {
         SeverityAndPurpose value = (SeverityAndPurpose)propertyInfo.GetValue(null)!;
         if (required.Contains(value))
         {
            string name = propertyInfo.Name;

            // Technically not needed, but it makes it a bit nicer.
            if (missing.Remove(value) == false)
               Assert.Fail($"The property ({helperClassType.Name}.{name}) contains a value ({value}) that has already been included.");

            string valueName = value.ToString();
            Assert.AreEqual(valueName, name, $"The property ({helperClassType.Name}.{name}) should match the value that is returned ({valueName}).");
         }
      }

      if (missing.Count > 0)
         Assert.Fail($"The helper class ({helperClassType.Name}) is missing the properties for: {string.Join(", ", missing)}");
   }

   private void VerifyBlacklistedPropertiesAreExcluded(Type helperClassType, byte includeMask, byte excludeMask, params SeverityAndPurpose[] blacklist)
   {
      PropertyInfo[] properties = GetProperties(helperClassType);

      HashSet<SeverityAndPurpose> toExclude = Inverse(GetValuesToInclude(includeMask, excludeMask, blacklist));

      foreach (PropertyInfo propertyInfo in properties)
      {
         SeverityAndPurpose value = (SeverityAndPurpose)propertyInfo.GetValue(null)!;

         if (toExclude.Contains(value))
         {
            string name = propertyInfo.Name;

            Assert.Fail($"The helper class ({helperClassType.Name}) contains a property ({name}) with a blacklisted value ({value}).");
         }
      }
   }
   #endregion

   #region Helpers
   private static PropertyInfo[] GetProperties(Type type)
   {
      return type
         .GetProperties(BindingFlags.Public | BindingFlags.Static)
         .Where(prop => prop.PropertyType == typeof(SeverityAndPurpose))
         .ToArray();
   }
   private static HashSet<SeverityAndPurpose> Inverse(IEnumerable<SeverityAndPurpose> values)
   {
      HashSet<SeverityAndPurpose> newValues = new HashSet<SeverityAndPurpose>();

      foreach (SeverityAndPurpose value in Enum.GetValues<SeverityAndPurpose>())
      {
         if (values.Contains(value) == false)
            newValues.Add(value);
      }

      return newValues;
   }
   private static HashSet<SeverityAndPurpose> GetValuesToInclude(byte includeMask, byte excludeMask, params SeverityAndPurpose[] blacklist)
   {
      HashSet<SeverityAndPurpose> values = new HashSet<SeverityAndPurpose>();
      HashSet<SeverityAndPurpose> blacklistSet = new HashSet<SeverityAndPurpose>(blacklist);

      foreach (SeverityAndPurpose value in Enum.GetValues<SeverityAndPurpose>())
      {
         byte rawValue = (byte)value;
         bool shouldInclude = (rawValue & includeMask) != 0;
         bool shouldExclude =
            ((rawValue & excludeMask) != 0) ||
            blacklistSet.Contains(value) ||
            rawValue == includeMask;

         if (shouldInclude && !shouldExclude)
         {
            SeverityAndPurpose typedRaw = (SeverityAndPurpose)rawValue;
            values.Add(typedRaw);
         }
      }

      return values;
   }
   #endregion
}
