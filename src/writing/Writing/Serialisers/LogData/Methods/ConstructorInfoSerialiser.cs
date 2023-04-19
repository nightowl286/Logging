using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.Methods;

/// <summary>
/// A serialiser for <see cref="IConstructorInfo"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.ConstructorInfo)]
public class ConstructorInfoSerialiser : ISerialiser<IConstructorInfo>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ConstructorInfoSerialiser"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public ConstructorInfoSerialiser(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
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
         _serialiser.Serialise(writer, parameterInfo);
   }

   /// <inheritdoc/>
   public ulong Count(IConstructorInfo data)
   {
      int nameSize = BinaryWriterSizeHelper.StringSize(data.Name);
      int size = sizeof(ulong);

      int parameterInfosCountSize = BinaryWriterSizeHelper.Encoded7BitIntSize(data.ParameterInfos.Count);

      ulong parameterInfosSize = 0;
      foreach (IParameterInfo parameterInfo in data.ParameterInfos)
         parameterInfosSize += _serialiser.Count(parameterInfo);

      return (ulong)(nameSize + size + parameterInfosCountSize) + parameterInfosSize;
   }
   #endregion
}
