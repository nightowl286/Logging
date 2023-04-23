using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.Methods;

/// <summary>
/// Represents a <see cref="ISerialiser{T}"/> dispatcher that will serialise
/// a given <see cref="IMethodBaseInfo"/> based on its <see cref="MethodKind"/>.
/// </summary>
public class MethodBaseInfoSerialiserDispatcher : ISerialiser<IMethodBaseInfo>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="MethodBaseInfoSerialiserDispatcher"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public MethodBaseInfoSerialiserDispatcher(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IMethodBaseInfo data)
   {
      if (data is IMethodInfo methodInfo)
      {
         writer.Write((byte)MethodKind.Method);
         _serialiser.Serialise(writer, methodInfo);
      }
      else if (data is IConstructorInfo constructorInfo)
      {
         writer.Write((byte)MethodKind.Constructor);
         _serialiser.Serialise(writer, constructorInfo);
      }
      else
         throw new ArgumentException($"Unknown method type ({data.GetType()}).", nameof(data));
   }
   /// <inheritdoc/>
   public int Count(IMethodBaseInfo data)
   {
      if (data is IMethodInfo methodInfo)
         return _serialiser.Count(methodInfo) + sizeof(byte);

      if (data is IConstructorInfo constructorInfo)
         return _serialiser.Count(constructorInfo) + sizeof(byte);

      throw new ArgumentException($"Unknown method type ({data.GetType()}).", nameof(data));
   }
   #endregion
}
