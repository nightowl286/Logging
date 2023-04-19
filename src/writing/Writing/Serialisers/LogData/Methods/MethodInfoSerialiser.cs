using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.Methods;

/// <summary>
/// A serialiser for <see cref="IMethodInfo"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.MethodInfo)]
public class MethodInfoSerialiser : ISerialiser<IMethodInfo>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="MethodInfoSerialiser"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public MethodInfoSerialiser(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IMethodInfo data)
   {
      ulong declaringTypeId = data.DeclaringTypeId;
      ulong returnTypeId = data.ReturnTypeId;
      string name = data.Name;
      IReadOnlyList<ulong> genericTypeIds = data.GenericTypeIds;
      IReadOnlyList<IParameterInfo> parameterInfos = data.ParameterInfos;

      writer.Write(declaringTypeId);
      writer.Write(returnTypeId);
      writer.Write(name);

      writer.Write7BitEncodedInt(genericTypeIds.Count);
      foreach (ulong genericTypeId in genericTypeIds)
         writer.Write(genericTypeId);

      writer.Write7BitEncodedInt(parameterInfos.Count);
      foreach (IParameterInfo parameterInfo in parameterInfos)
         _serialiser.Serialise(writer, parameterInfo);
   }

   /// <inheritdoc/>
   public ulong Count(IMethodInfo data)
   {
      int nameSize = BinaryWriterSizeHelper.StringSize(data.Name);
      int size =
         (sizeof(ulong) * 2) +
         (sizeof(ulong) * data.GenericTypeIds.Count);

      int genericTypeIdsCountSize = BinaryWriterSizeHelper.Encoded7BitIntSize(data.GenericTypeIds.Count);
      int parameterInfosCountSize = BinaryWriterSizeHelper.Encoded7BitIntSize(data.ParameterInfos.Count);

      ulong parameterInfosSize = 0;
      foreach (IParameterInfo parameterInfo in data.ParameterInfos)
         parameterInfosSize += _serialiser.Count(parameterInfo);

      return (ulong)(nameSize + size + genericTypeIdsCountSize + parameterInfosCountSize) + parameterInfosSize;
   }
   #endregion
}
