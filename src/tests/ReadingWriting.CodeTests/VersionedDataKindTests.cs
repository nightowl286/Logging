using System.Reflection;
using System.Text;
using TNO.Common.Extensions;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.ReadingWriting.CodeTests;

[TestClass]
[TestCategory(Category.Versioning)]
public class VersionedDataKindTests
{
   #region Methods
   [TestMethod]
   public void EnsureVersionedSerialisersHaveDataKindAttribute()
   {
      // Arrange
      Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
      HashSet<Type> missing = new HashSet<Type>();

      // Act
      foreach (Type type in assemblies.SelectMany(a => a.DefinedTypes))
      {
         if (type.IsInterface == false)
            continue;
         bool isSerialiser = type.ImplementsOpenInterface(typeof(IBinarySerialiser<>));
         bool isVersioned = type.GetInterfaces().Contains(typeof(IVersioned));

         if (isSerialiser && isVersioned)
         {
            bool hasKinds =
               type
               .GetDataKinds()
               .Any();

            if (hasKinds == false)
               missing.Add(type);
         }
      }

      // Assert
      if (missing.Count > 0)
      {
         StringBuilder messageBuilder = new StringBuilder();
         messageBuilder
            .AppendLine($"Some versioned serialisers were missing the {nameof(VersionedDataKindAttribute)}.")
            .AppendLine()
            .AppendLine($"Interfaces without an {nameof(VersionedDataKindAttribute)}:");

         foreach (Type type in missing)
            messageBuilder.AppendLine($"- {type.FullName}");

         Assert.Fail(messageBuilder.ToString());
      }
   }
   #endregion
}
