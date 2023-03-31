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

      List<ITypeInfo> depositedTypeInfo = new List<ITypeInfo>();

      LogWriteContext writeContext = new LogWriteContext();
      Mock<ILogDataCollector> collectorMock = new Mock<ILogDataCollector>();
      collectorMock.Setup(m => m.Deposit(Capture.In(depositedTypeInfo)));

      // Act
      ulong result = TypeInfoHelper.EnsureIdsForAssociatedTypes(writeContext, collectorMock.Object, type);

      // Assert
      CollectionAssert.That.IsOfSize(depositedTypeInfo, 3);

      // Object Asserts
      ITypeInfo objectInfo = depositedTypeInfo[0];
      Assert.That.AreEqual(nameof(Object), objectInfo.Name);
      Assert.That.AreEqual(expectedObjectId, objectInfo.Id);
      Assert.That.AreEqual(noTypeId, objectInfo.BaseTypeId);
      Assert.That.AreEqual(noTypeId, objectInfo.DeclaringTypeId);

      // TypeInfoHelperTests Asserts
      ITypeInfo helperTestsInfo = depositedTypeInfo[1];
      Assert.That.AreEqual(nameof(TypeInfoHelperTests), helperTestsInfo.Name);
      Assert.That.AreEqual(expectedTypeInfoHelperTestsId, helperTestsInfo.Id);
      Assert.That.AreEqual(objectInfo.Id, helperTestsInfo.BaseTypeId);
      Assert.That.AreEqual(noTypeId, helperTestsInfo.DeclaringTypeId);

      // TestType Asserts
      ITypeInfo testTypeInfo = depositedTypeInfo[2];
      Assert.That.AreEqual(nameof(TestType), testTypeInfo.Name);
      Assert.That.AreEqual(expectedTestTypeId, testTypeInfo.Id);
      Assert.That.AreEqual(objectInfo.Id, testTypeInfo.BaseTypeId);
      Assert.That.AreEqual(helperTestsInfo.Id, testTypeInfo.DeclaringTypeId);
   }

   [TestMethod]
   public void EnsureIdsForAssociatedTypes_WithGenericType_DepositsAssociatedTypes()
   {
      // Arrange
      Type type = typeof(GenericTestType<string>);

      ulong noTypeId = 0;
      ulong expectedObjectId = 1;
      ulong expectedGenericId = 2;
      ulong expectedTestTypeId = 3;


      List<ITypeInfo> depositedTypeInfo = new List<ITypeInfo>();

      LogWriteContext writeContext = new LogWriteContext();
      Mock<ILogDataCollector> collectorMock = new Mock<ILogDataCollector>();
      collectorMock.Setup(m => m.Deposit(Capture.In(depositedTypeInfo)));

      // Act
      ulong result = TypeInfoHelper.EnsureIdsForAssociatedTypes(writeContext, collectorMock.Object, type);

      // Assert
      CollectionAssert.That.IsOfSize(depositedTypeInfo, 3);

      // Object Asserts
      ITypeInfo objectInfo = depositedTypeInfo[0];
      Assert.That.AreEqual(nameof(Object), objectInfo.Name);
      Assert.That.AreEqual(expectedObjectId, objectInfo.Id);
      Assert.That.AreEqual(noTypeId, objectInfo.BaseTypeId);
      Assert.That.AreEqual(noTypeId, objectInfo.DeclaringTypeId);

      // Generic Type Asserts
      ITypeInfo genericTypeInfo = depositedTypeInfo[1];
      Assert.That.AreEqual(nameof(String), genericTypeInfo.Name);
      Assert.That.AreEqual(expectedGenericId, genericTypeInfo.Id);
      Assert.That.AreEqual(objectInfo.Id, genericTypeInfo.BaseTypeId);
      Assert.That.AreEqual(noTypeId, genericTypeInfo.DeclaringTypeId);

      // TestType Asserts
      ITypeInfo testTypeInfo = depositedTypeInfo[2];
      Assert.That.AreEqual(typeof(GenericTestType<string>).Name, testTypeInfo.Name);
      Assert.That.AreEqual(expectedTestTypeId, testTypeInfo.Id);
      Assert.That.AreEqual(objectInfo.Id, testTypeInfo.BaseTypeId);
      Assert.That.AreEqual(noTypeId, testTypeInfo.DeclaringTypeId);
   }
   #endregion

   #region Test Class
   private class TestType { }
   #endregion
}

internal class GenericTestType<T> { }