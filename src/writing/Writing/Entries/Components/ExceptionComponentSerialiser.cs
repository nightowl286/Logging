using TNO.Logging.Common.Abstractions.DataKinds;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Common.Abstractions.Versioning;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Entries.Components;

/// <summary>
/// A serialiser for <see cref="IExceptionComponent"/>.
/// </summary>
[Version(0)]
[VersionedDataKind(VersionedDataKind.Exception)]
public sealed class ExceptionComponentSerialiser : ISerialiser<IExceptionComponent>
{
   #region Fields
   private readonly ISerialiser _serialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="ExceptionComponentSerialiser"/>.</summary>
   /// <param name="serialiser">The general <see cref="ISerialiser"/> to use.</param>
   public ExceptionComponentSerialiser(ISerialiser serialiser)
   {
      _serialiser = serialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IExceptionComponent data)
   {
      _serialiser.Serialise(writer, data.ExceptionInfo);
   }

   /// <inheritdoc/>
   public int Count(IExceptionComponent data)
   {
      return _serialiser.Count(data.ExceptionInfo);
   }
   #endregion
}