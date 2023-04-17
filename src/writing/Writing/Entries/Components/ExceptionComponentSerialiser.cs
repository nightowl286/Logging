using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Exceptions;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IExceptionComponent"/>.
/// </summary>
[Version(0)]
public sealed class ExceptionComponentSerialiser : IExceptionComponentSerialiser
{
   #region Fields
   private readonly IExceptionInfoSerialiser _exceptionInfoSerialiser;
   #endregion

   #region Constructors
   public ExceptionComponentSerialiser(IExceptionInfoSerialiser exceptionInfoSerialiser)
   {
      _exceptionInfoSerialiser = exceptionInfoSerialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IExceptionComponent data)
   {
      _exceptionInfoSerialiser.Serialise(writer, data.ExceptionInfo);
   }

   /// <inheritdoc/>
   public ulong Count(IExceptionComponent data)
   {
      return _exceptionInfoSerialiser.Count(data.ExceptionInfo);
   }
   #endregion
}