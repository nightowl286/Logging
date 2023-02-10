using System.Collections.Generic;
using System.Reflection;
using System.Text;
using TNO.Logging.Common.Abstractions.Entries;

namespace TNO.Common.Abstractions.CodeTests.entries;

[TestClass]
public abstract class SeverityAndPurposeTestsBase
{
   #region Tests
   protected static void PropertiesWithReturnType_NoUnexpectedNames(Type helperType, IEnumerable<SeverityAndPurpose> values)
   {
      // Arrange
      HashSet<string> validNames = values
         .Select(v => v.ToString())
         .ToHashSet();

      IEnumerable<PropertyInfo> properties = WithReturnType(helperType);

      // Check
      StringBuilder output = new StringBuilder();
      output
         .AppendLine()
         .AppendLine($"Properties with the type {nameof(SeverityAndPurpose)} with unexpected names:");

      bool hadInvalid = false;
      foreach (PropertyInfo property in properties)
      {
         string name = property.Name;
         if (validNames.Contains(name) == false)
         {
            hadInvalid = true;
            output
               .Append(" - ")
               .AppendLine(name);
         }
      }

      // Output
      if (hadInvalid)
         Assert.Fail(output.ToString());
   }

   protected static void PropertiesWithExpectedName_HaveExpectedReturnTypeAndValue(Type helperType, IEnumerable<SeverityAndPurpose> values)
   {
      // Arrange
      HashSet<SeverityAndPurpose> missing = new();
      HashSet<SeverityAndPurpose> wrongType = new();
      HashSet<SeverityAndPurpose> wrongValue = new();

      // Check
      foreach (SeverityAndPurpose value in values)
      {
         string name = value.ToString();
         PropertyInfo? property = GetPropertyWithName(helperType, name);

         if (property is null)
         {
            missing.Add(value);
            continue;
         }

         if (property.PropertyType != typeof(SeverityAndPurpose))
         {
            wrongType.Add(value);
            continue;
         }

         object? rawValue = property.GetValue(null);
         SeverityAndPurpose propertyValue = (SeverityAndPurpose)rawValue!;

         if (propertyValue != value)
            wrongValue.Add(value);
      }

      // Output
      bool anyInvalid =
         missing.Count > 0 ||
         wrongType.Count > 0 ||
         wrongValue.Count > 0;

      if (anyInvalid == false)
         return;

      StringBuilder output = new StringBuilder();
      output.AppendLine();
      if (missing.Count > 0)
         AddToOutput("Properties that were missing: ", output, missing);

      if (wrongType.Count > 0)
         AddToOutput("Properties with the expected name, but the wrong type: ", output, wrongType);

      if (wrongValue.Count > 0)
         AddToOutput("Properties with the expected name, but the wrong value: ", output, wrongValue);

      Assert.Fail(output.ToString());
   }
   #endregion

   #region Helpers
   private static void AddToOutput(string header, StringBuilder output, IEnumerable<SeverityAndPurpose> values)
   {
      output.AppendLine(header);
      foreach (SeverityAndPurpose value in values)
         output.AppendLine($" - {value}");

      output.AppendLine();
   }
   private static PropertyInfo? GetPropertyWithName(Type helperType, string name)
   {
      return helperType
         .GetProperty(name, BindingFlags.Public | BindingFlags.Static);
   }
   private static IEnumerable<PropertyInfo> WithReturnType(Type helperType)
   {
      return helperType
         .GetProperties(BindingFlags.Public | BindingFlags.Static)
         .Where(p => p.PropertyType == typeof(SeverityAndPurpose));
   }
   #endregion
}
