using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class AssemblyComponentReadWriteTests : BinaryReadWriteTestsBase<AssemblyComponentSerialiser, AssemblyComponentDeserialiserLatest, IAssemblyComponent>
{
   #region Methods
   protected override IEnumerable<Annotated<IAssemblyComponent>> CreateData()
   {
      ulong assemblyId = 5;
      AssemblyComponent component = new AssemblyComponent(assemblyId);

      yield return new(component);
   }
   protected override void Verify(IAssemblyComponent expected, IAssemblyComponent result)
   {
      Assert.That.AreEqual(expected.AssemblyId, result.AssemblyId);
   }
   #endregion
}