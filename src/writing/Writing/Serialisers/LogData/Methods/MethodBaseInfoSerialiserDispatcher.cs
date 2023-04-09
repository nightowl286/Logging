using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Constructors;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.Methods;

namespace TNO.Logging.Writing.Serialisers.LogData.Methods;

/// <summary>
/// Represents a <see cref="ISerialiser{T}"/> dispatcher that will serialise
/// a given <see cref="IMethodBaseInfo"/> based on its <see cref="MethodKind"/>.
/// </summary>
public class MethodBaseInfoSerialiserDispatcher : IMethodBaseInfoSerialiserDispatcher
{
   #region Fields
   private readonly IMethodInfoSerialiser _methodInfoSerialiser;
   private readonly IConstructorInfoSerialiser _constructorInfoSerialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="MethodBaseInfoSerialiserDispatcher"/>.</summary>
   /// <param name="methodInfoSerialiser">The method info serialiser to use.</param>
   /// <param name="constructorInfoSerialiser">The constructor info serialiser to use.</param>
   public MethodBaseInfoSerialiserDispatcher(
      IMethodInfoSerialiser methodInfoSerialiser,
      IConstructorInfoSerialiser constructorInfoSerialiser)
   {
      _methodInfoSerialiser = methodInfoSerialiser;
      _constructorInfoSerialiser = constructorInfoSerialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IMethodBaseInfo data)
   {
      if (data is IMethodInfo methodInfo)
      {
         writer.Write((byte)MethodKind.Method);
         _methodInfoSerialiser.Serialise(writer, methodInfo);
      }
      else if (data is IConstructorInfo constructorInfo)
      {
         writer.Write((byte)MethodKind.Constructor);
         _constructorInfoSerialiser.Serialise(writer, constructorInfo);
      }
      else
         throw new ArgumentException($"Unknown method type ({data.GetType()}).", nameof(data));
   }
   /// <inheritdoc/>
   public ulong Count(IMethodBaseInfo data)
   {
      if (data is IMethodInfo methodInfo)
         return _methodInfoSerialiser.Count(methodInfo) + sizeof(byte);

      if (data is IConstructorInfo constructorInfo)
         return _constructorInfoSerialiser.Count(constructorInfo) + sizeof(byte);

      throw new ArgumentException($"Unknown method type ({data.GetType()}).", nameof(data));
   }
   #endregion
}
