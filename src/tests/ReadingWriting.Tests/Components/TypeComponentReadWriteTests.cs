using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class TypeComponentReadWriteTests : ReadWriteTestsBase<TypeComponentSerialiser, TypeComponentDeserialiserLatest, ITypeComponent>
{
   #region Methods
   protected override ITypeComponent CreateData()
   {
      ulong typeId = 5;
      TypeComponent component = new TypeComponent(typeId);

      return component;
   }
   protected override void Verify(ITypeComponent expected, ITypeComponent result)
   {
      Assert.That.AreEqual(expected.TypeId, result.TypeId);
   }
   #endregion
}