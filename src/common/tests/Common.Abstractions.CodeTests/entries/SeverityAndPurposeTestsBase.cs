using System.Reflection;
using System.Text;
using TNO.Logging.Common.Abstractions.Entries.Importance;

namespace TNO.Common.Abstractions.CodeTests.entries;

[TestClass]
public abstract class ImportanceTestsBase<T>
{
   #region Tests
   protected static void PropertiesWithReturnType_NoUnexpectedNames(IEnumerable<ImportanceCombination> values)
   {
      // Arrange
      HashSet<string> validNames = values
         .Select(v => v.ToString())
         .ToHashSet();

      IEnumerable<PropertyInfo> properties = WithReturnType(typeof(T));

      // Check
      StringBuilder output = new StringBuilder();
      output
         .AppendLine()
         .AppendLine($"Properties with the type {nameof(ImportanceCombination)} with unexpected names:");

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

   protected void PropertiesWithExpectedName_HaveExpectedReturnTypeAndValue(IEnumerable<ImportanceCombination> values)
   {
      // Arrange
      HashSet<ImportanceCombination> missing = new();
      HashSet<ImportanceCombination> wrongType = new();
      HashSet<ImportanceCombination> wrongValue = new();

      // Check
      foreach (ImportanceCombination value in values)
      {
         string name = value.ToString();
         PropertyInfo? property = GetPropertyWithName(typeof(T), name);

         if (property is null)
         {
            missing.Add(value);
            continue;
         }

         if (property.PropertyType != typeof(T))
         {
            wrongType.Add(value);
            continue;
         }

         object? rawValue = property.GetValue(null);
         T propertyValue = (T)rawValue!;
         ImportanceCombination importanceValue = GetImportanceValue(propertyValue);

         if (importanceValue != value)
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
   protected abstract ImportanceCombination GetImportanceValue(T component);
   private static void AddToOutput(string header, StringBuilder output, IEnumerable<ImportanceCombination> values)
   {
      output.AppendLine(header);
      foreach (ImportanceCombination value in values)
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
         .Where(p => p.PropertyType == helperType);
   }
   #endregion
}