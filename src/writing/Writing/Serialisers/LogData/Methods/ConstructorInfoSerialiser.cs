using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Constructors;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Parameters;

namespace TNO.Logging.Writing.Serialisers.LogData.Constructors;

/// <summary>
/// A serialiser for <see cref="IConstructorInfo"/>.
/// </summary>
public class ConstructorInfoSerialiser : IConstructorInfoSerialiser
{
   #region Fields
   private readonly IParameterInfoSerialiser _parameterInfoSerialiser;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ConstructorInfoSerialiser"/>.</summary>
   /// <param name="parameterInfoSerialiser">The <see cref="IParameterInfoSerialiser"/> to use.</param>
   public ConstructorInfoSerialiser(IParameterInfoSerialiser parameterInfoSerialiser)
   {
      _parameterInfoSerialiser = parameterInfoSerialiser;
   }
   #endregion

   #region Constructors
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IConstructorInfo data)
   {
      string name = data.Name;
      ulong declaringTypeId = data.DeclaringTypeId;
      IReadOnlyList<IParameterInfo> parameterInfos = data.ParameterInfos;

      writer.Write(declaringTypeId);
      writer.Write(name);

      writer.Write7BitEncodedInt(parameterInfos.Count);
      foreach (IParameterInfo parameterInfo in parameterInfos)
         _parameterInfoSerialiser.Serialise(writer, parameterInfo);
   }

   /// <inheritdoc/>
   public ulong Count(IConstructorInfo data)
   {
      int nameSize = BinaryWriterSizeHelper.StringSize(data.Name);
      int size = sizeof(ulong);

      int parameterInfosCountSize = BinaryWriterSizeHelper.Encoded7BitIntSize(data.ParameterInfos.Count);

      ulong parameterInfosSize = 0;
      foreach (IParameterInfo parameterInfo in data.ParameterInfos)
         parameterInfosSize += _parameterInfoSerialiser.Count(parameterInfo);

      return (ulong)(nameSize + size + parameterInfosCountSize) + parameterInfosSize;
   }
   #endregion
}
