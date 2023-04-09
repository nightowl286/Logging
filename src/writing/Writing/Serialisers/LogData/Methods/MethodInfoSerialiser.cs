using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Methods;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Parameters;

namespace TNO.Logging.Writing.Serialisers.LogData.Methods;

/// <summary>
/// A serialiser for <see cref="IMethodInfo"/>.
/// </summary>
public class MethodInfoSerialiser : IMethodInfoSerialiser
{
   #region Fields
   private readonly IParameterInfoSerialiser _parameterInfoSerialiser;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="MethodInfoSerialiser"/>.</summary>
   /// <param name="parameterInfoSerialiser">The <see cref="IParameterInfoSerialiser"/> to use.</param>
   public MethodInfoSerialiser(IParameterInfoSerialiser parameterInfoSerialiser)
   {
      _parameterInfoSerialiser = parameterInfoSerialiser;
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
         _parameterInfoSerialiser.Serialise(writer, parameterInfo);
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
         parameterInfosSize += _parameterInfoSerialiser.Count(parameterInfo);

      return (ulong)(nameSize + size + genericTypeIdsCountSize + parameterInfosCountSize) + parameterInfosSize;
   }
   #endregion
}
