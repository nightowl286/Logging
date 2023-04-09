using TNO.Logging.Common.Abstractions.LogData.Types;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Loggers;

namespace TNO.Writing.Tests.Loggers;

[TestClass]
public class TypeInfoHelperTests
{
   #region Tests
   [TestMethod]
   public void EnsureIdsForAssociatedTypes_WithTestType_DepositsAssociatedTypes()
   {
      // Arrange
      Type type = typeof(TestType);
      ulong noTypeId = 0;
      ulong expectedObjectId = 1;
      ulong expectedTypeInfoHelperTestsId = 2;
      ulong expectedTestTypeId = 3;

      List<TypeReference> depositedTypeReferences = new List<TypeReference>();

      LogWriteContext writeContext = new LogWriteContext();
      Mock<ILogDataCollector> collectorMock = new Mock<ILogDataCollector>();
      collectorMock.Setup(m => m.Deposit(Capture.In(depositedTypeReferences)));

      // Act
      ulong result = TypeInfoHelper.EnsureIdsForAssociatedTypes(writeContext, collectorMock.Object, type);

      // Assert
      CollectionAssert.That.IsOfSize(depositedTypeReferences, 3);

      // Object Asserts
      TypeReference objectReference = depositedTypeReferences[0];
      ITypeInfo objectInfo = objectReference.TypeInfo;

      Assert.That.AreEqual(nameof(Object), objectInfo.Name);
      Assert.That.AreEqual(expectedObjectId, objectReference.Id);
      Assert.That.AreEqual(noTypeId, objectInfo.BaseTypeId);
      Assert.That.AreEqual(noTypeId, objectInfo.DeclaringTypeId);

      // TypeInfoHelperTests Asserts
      TypeReference helperTestsReference = depositedTypeReferences[1];
      ITypeInfo helperTestsInfo = helperTestsReference.TypeInfo;

      Assert.That.AreEqual(nameof(TypeInfoHelperTests), helperTestsInfo.Name);
      Assert.That.AreEqual(expectedTypeInfoHelperTestsId, helperTestsReference.Id);
      Assert.That.AreEqual(objectReference.Id, helperTestsInfo.BaseTypeId);
      Assert.That.AreEqual(noTypeId, helperTestsInfo.DeclaringTypeId);

      // TestType Asserts
      TypeReference testTypeReference = depositedTypeReferences[2];
      ITypeInfo testTypeInfo = testTypeReference.TypeInfo;

      Assert.That.AreEqual(nameof(TestType), testTypeInfo.Name);
      Assert.That.AreEqual(expectedTestTypeId, testTypeReference.Id);
      Assert.That.AreEqual(objectReference.Id, testTypeInfo.BaseTypeId);
      Assert.That.AreEqual(helperTestsReference.Id, testTypeInfo.DeclaringTypeId);
   }

   [TestMethod]
   public void EnsureIdsForAssociatedTypes_WithGenericType_DepositsAssociatedTypes()
   {
      // Arrange
      Type type = typeof(GenericTestType<string>);

      ulong noTypeId = 0;
      ulong expectedObjectId = 1;
      ulong expectedGenericDefinitionId = 2;
      ulong expectedGenericId = 3;
      ulong expectedTestTypeId = 4;

      List<TypeReference> depositedTypeReferences = new List<TypeReference>();

      LogWriteContext writeContext = new LogWriteContext();
      Mock<ILogDataCollector> collectorMock = new Mock<ILogDataCollector>();
      collectorMock.Setup(m => m.Deposit(Capture.In(depositedTypeReferences)));

      // Act
      ulong result = TypeInfoHelper.EnsureIdsForAssociatedTypes(writeContext, collectorMock.Object, type);

      // Assert
      CollectionAssert.That.IsOfSize(depositedTypeReferences, 4);

      // Object Asserts
      TypeReference objectReference = depositedTypeReferences[0];
      ITypeInfo objectInfo = objectReference.TypeInfo;

      Assert.That.AreEqual(nameof(Object), objectInfo.Name);
      Assert.That.AreEqual(expectedObjectId, objectReference.Id);
      Assert.That.AreEqual(noTypeId, objectInfo.BaseTypeId);
      Assert.That.AreEqual(noTypeId, objectInfo.DeclaringTypeId);

      // Generic Definition Asserts
      TypeReference genericDefinitionReference = depositedTypeReferences[1];
      ITypeInfo genericDefinitionInfo = genericDefinitionReference.TypeInfo;

      Assert.That.AreEqual(typeof(GenericTestType<>).Name, genericDefinitionInfo.Name);
      Assert.That.AreEqual(expectedGenericDefinitionId, genericDefinitionReference.Id);
      Assert.That.AreEqual(expectedObjectId, genericDefinitionInfo.BaseTypeId);
      Assert.That.AreEqual(noTypeId, genericDefinitionInfo.DeclaringTypeId);

      // Generic Type Asserts
      TypeReference genericTypeReference = depositedTypeReferences[2];
      ITypeInfo genericTypeInfo = genericTypeReference.TypeInfo;

      Assert.That.AreEqual(nameof(String), genericTypeInfo.Name);
      Assert.That.AreEqual(expectedGenericId, genericTypeReference.Id);
      Assert.That.AreEqual(objectReference.Id, genericTypeInfo.BaseTypeId);
      Assert.That.AreEqual(noTypeId, genericTypeInfo.DeclaringTypeId);

      // TestType Asserts
      TypeReference testTypeReference = depositedTypeReferences[3];
      ITypeInfo testTypeInfo = testTypeReference.TypeInfo;

      Assert.That.AreEqual(typeof(GenericTestType<string>).Name, testTypeInfo.Name);
      Assert.That.AreEqual(expectedTestTypeId, testTypeReference.Id);
      Assert.That.AreEqual(objectReference.Id, testTypeInfo.BaseTypeId);
      Assert.That.AreEqual(noTypeId, testTypeInfo.DeclaringTypeId);
   }
   #endregion

   #region Test Class
   private class TestType { }
   #endregion
}

internal class GenericTestType<T> { }