using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Reading.Abstractions.LogData.Methods;
using TNO.Logging.Reading.Abstractions.LogData.StackTraces.StackFrameInfos;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.Methods.StackFrameInfos;

namespace TNO.Logging.Reading.LogData.StackTraces.StackFrameInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IStackFrameInfo"/>, version #0.
/// </summary>
public sealed class StackFrameInfoDeserialiser0 : IStackFrameInfoDeserialiser
{
   #region Fields
   private readonly IMethodBaseInfoDeserialiserDispatcher _methodBaseInfoDeserialiser;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackFrameInfoDeserialiser0"/>.</summary>
   /// <param name="methodBaseInfoDeserialiser">The <see cref="IMethodBaseInfoDeserialiserDispatcher"/> to use.</param>
   public StackFrameInfoDeserialiser0(IMethodBaseInfoDeserialiserDispatcher methodBaseInfoDeserialiser)
   {
      _methodBaseInfoDeserialiser = methodBaseInfoDeserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IStackFrameInfo Deserialise(BinaryReader reader)
   {
      IMethodBaseInfo ReadMethodBaseInfo() => _methodBaseInfoDeserialiser.Deserialise(reader);

      ulong fileId = reader.ReadUInt64();
      uint line = reader.ReadUInt32();
      uint column = reader.ReadUInt32();

      IMethodBaseInfo mainMethod = ReadMethodBaseInfo();
      IMethodBaseInfo? secondaryMethod = reader.TryReadNullable(ReadMethodBaseInfo);

      return StackFrameInfoFactory.Version0(
         fileId,
         line,
         column,
         mainMethod,
         secondaryMethod);
   }
   #endregion
}
