using TNO.Logging.Common.Abstractions.LogData.Methods;
using TNO.Logging.Common.Abstractions.LogData.StackTraces;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers.LogData.StackTraces;

namespace TNO.Logging.Writing.Serialisers.LogData.StackTraces;

/// <summary>
/// A serialiser for <see cref="IStackFrameInfo"/>.
/// </summary>
public class StackFrameInfoSerialiser : IStackFrameInfoSerialiser
{
   #region Fields
   private readonly IMethodBaseInfoSerialiserDispatcher _methodBaseInfoSerialiser;
   #endregion

   #region Properties
   /// <inheritdoc/>
   public uint Version => 0;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="StackFrameInfoSerialiser"/>.</summary>
   /// <param name="methodBaseInfoSerialiser">The <see cref="IMethodBaseInfoSerialiserDispatcher"/> to use.</param>
   public StackFrameInfoSerialiser(IMethodBaseInfoSerialiserDispatcher methodBaseInfoSerialiser)
   {
      _methodBaseInfoSerialiser = methodBaseInfoSerialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IStackFrameInfo data)
   {
      ulong fileId = data.FileId;
      uint line = data.LineInFile;
      uint column = data.ColumnInLine;
      IMethodBaseInfo? mainMethod = data.MainMethod;
      IMethodBaseInfo? secondaryMethod = data.SecondaryMethod;

      writer.Write(fileId);
      writer.Write(line);
      writer.Write(column);

      if (writer.TryWriteNullable(mainMethod))
         _methodBaseInfoSerialiser.Serialise(writer, mainMethod);

      if (writer.TryWriteNullable(secondaryMethod))
         _methodBaseInfoSerialiser.Serialise(writer, secondaryMethod);
   }

   /// <inheritdoc/>
   public ulong Count(IStackFrameInfo data)
   {
      int size =
         (sizeof(bool) * 2) +
         (sizeof(uint) * 2) +
         sizeof(ulong);

      ulong mainMethodSize = data.MainMethod is null ? 0 : _methodBaseInfoSerialiser.Count(data.MainMethod);
      ulong secondaryMethodSize = data.SecondaryMethod is null ? 0 : _methodBaseInfoSerialiser.Count(data.SecondaryMethod);

      return mainMethodSize + secondaryMethodSize + (ulong)size;
   }
   #endregion
}
