using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class TypeComponentReadWriteTests : BinaryReadWriteTestsBase<TypeComponentSerialiser, TypeComponentDeserialiserLatest, ITypeComponent>
{
   #region Methods
   protected override IEnumerable<ITypeComponent> CreateData()
   {
      ulong typeId = 5;
      TypeComponent component = new TypeComponent(typeId);

      yield return component;
   }
   protected override void Verify(ITypeComponent expected, ITypeComponent result)
   {
      Assert.That.AreEqual(expected.TypeId, result.TypeId);
   }
   #endregion
}