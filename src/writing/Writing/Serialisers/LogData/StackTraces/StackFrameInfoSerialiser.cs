using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Serialisers.LogData.StackTraces;

/// <summary>
/// A serialiser for <see cref="IStackFrameInfo"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.StackFrameInfo)]
public class StackFrameInfoSerialiser : ISerialiser<IStackFrameInfo>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackFrameInfoSerialiser"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public StackFrameInfoSerialiser(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IStackFrameInfo data)
   {
      ulong fileId = data.FileId;
      uint line = data.LineInFile;
      uint column = data.ColumnInLine;
      IMethodBaseInfo mainMethod = data.MainMethod;
      IMethodBaseInfo? secondaryMethod = data.SecondaryMethod;

      writer.Write(fileId);
      writer.Write(line);
      writer.Write(column);

      _serialiser.Serialise(writer, mainMethod);

      if (writer.TryWriteNullable(secondaryMethod))
         _serialiser.Serialise(writer, secondaryMethod);
   }

   /// <inheritdoc/>
   public int Count(IStackFrameInfo data)
   {
      int size =
         sizeof(bool) +
         (sizeof(uint) * 2) +
         sizeof(ulong);

      int mainMethodSize = data.MainMethod is null ? 0 : _serialiser.Count(data.MainMethod);
      int secondaryMethodSize = data.SecondaryMethod is null ? 0 : _serialiser.Count(data.SecondaryMethod);

      return mainMethodSize + secondaryMethodSize + size;
   }
   #endregion
}
