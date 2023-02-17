using System.Text;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Writing.Tests.BinarySerialiserCountTests;

[TestCategory(Category.Serialisation)]
[TestCategory(Category.Counting)]
public abstract class BinarySerialiserCountTestBase<TWriter, TData>
   where TWriter : IBinarySerialiser<TData>
{
   #region Properties
   public static Encoding Encoding { get; } = Encoding.UTF8;
   #endregion

   #region Test Methods
   protected void CountTestBase(TData data)
   {
      // Arrange
      TWriter writer = Setup();
      ulong expectedResult = GetActualSize(writer, data);

      // Act
      ulong result = writer.Count(data);

      // Assert
      Assert.AreEqual(expectedResult, result);
   }

   private static ulong GetActualSize(TWriter writer, TData data)
   {
      using (MemoryStream memoryStream = new MemoryStream())
      {
         using (BinaryWriter bw = new BinaryWriter(memoryStream, Encoding, true))
            writer.Serialise(bw, data);

         return (ulong)memoryStream.Length;
      }
   }
   #endregion

   #region Methods
   protected virtual TWriter Setup()
      => Activator.CreateInstance<TWriter>();
   #endregion
}
