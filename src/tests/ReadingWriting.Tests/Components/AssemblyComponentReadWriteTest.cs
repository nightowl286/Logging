using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Entries.Components;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.ReadingWriting.Tests.Components;

[TestClass]
[TestCategory(Category.Components)]
public class AssemblyComponentReadWriteTest : ReadWriteTestBase<AssemblyComponentSerialiser, AssemblyComponentDeserialiserLatest, IAssemblyComponent>
{
   #region Methods
   protected override IAssemblyComponent CreateData()
   {
      ulong assemblyId = 5;
      AssemblyComponent component = new AssemblyComponent(assemblyId);

      return component;
   }
   protected override void Verify(IAssemblyComponent expected, IAssemblyComponent result)
   {
      Assert.That.AreEqual(expected.AssemblyId, result.AssemblyId);
   }
   #endregion
}