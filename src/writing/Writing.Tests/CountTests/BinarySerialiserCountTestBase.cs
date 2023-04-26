using System.Reflection;
using System.Text;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Writing.Tests.BinarySerialiserCountTests;

[TestCategory(Category.Serialisation)]
[TestCategory(Category.Counting)]
public abstract class SerialiserCountTestBase<TWriter, TData>
{
   #region Properties
   public static Encoding Encoding { get; } = Encoding.UTF8;
   #endregion

   #region Test Methods
   protected void CountTestBase(TData data)
   {
      // Arrange
      TWriter writer = Setup();
      int expectedResult = GetActualSize(writer, data);

      // Act
      int result = Count(writer, data);

      // Assert
      Assert.That.AreEqual(expectedResult, result);
   }

   private int GetActualSize(TWriter writer, TData data)
   {
      using (MemoryStream memoryStream = new MemoryStream())
      {
         using (BinaryWriter bw = new BinaryWriter(memoryStream, Encoding, true))
            Serialise(writer, bw, data);

         return (int)memoryStream.Length;
      }
   }

   protected abstract int Count(TWriter writer, TData data);
   protected abstract void Serialise(TWriter writer, BinaryWriter binaryWriter, TData data);
   #endregion

   #region Methods
   protected virtual TWriter Setup()
   {
      Type type = typeof(TWriter);
      if (type.GetConstructors().Any(CanInjectGeneralSerialiser))
      {
         object?[]? parameters = new[] { GeneralSerialiser.Instance };
         object instance = Activator.CreateInstance(typeof(TWriter), parameters) ?? throw new NullReferenceException($"Setup failed when trying to creates an instance of {typeof(TWriter)}."); ;
         return (TWriter)instance;
      }

      return Activator.CreateInstance<TWriter>();
   }
   private static bool CanInjectGeneralSerialiser(ConstructorInfo constructorInfo)
   {
      ParameterInfo[] parameters = constructorInfo.GetParameters();
      if (parameters.Length == 1)
      {
         ParameterInfo param = parameters[0];
         return param.ParameterType == typeof(ISerialiser);
      }

      return false;
   }
   #endregion
}

public abstract class BinarySerialiserCountTestBase<TWriter, TData> : SerialiserCountTestBase<TWriter, TData>
   where TWriter : ISerialiser<TData>
{
   #region Methods
   protected override int Count(TWriter writer, TData data) => writer.Count(data);
   protected override void Serialise(TWriter writer, BinaryWriter binaryWriter, TData data) => writer.Serialise(binaryWriter, data);
   #endregion
}