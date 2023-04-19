using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.Methods.StackFrameInfos;

namespace TNO.Logging.Reading.LogData.StackTraces.StackFrameInfos.Versions;

/// <summary>
/// A deserialiser for <see cref="IStackFrameInfo"/>, version #0.
/// </summary>
[Version(0)]
public sealed class StackFrameInfoDeserialiser0 : IDeserialiser<IStackFrameInfo>
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackFrameInfoDeserialiser0"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>

   public StackFrameInfoDeserialiser0(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IStackFrameInfo Deserialise(BinaryReader reader)
   {
      IMethodBaseInfo ReadMethodBaseInfo() => _deserialiser.Deserialise<IMethodBaseInfo>(reader);

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
