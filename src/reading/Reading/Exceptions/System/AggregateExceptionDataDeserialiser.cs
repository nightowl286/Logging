using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Exceptions.Abstractions;
using TNO.Logging.Common.Exceptions.Abstractions.System;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Exceptions;
#if !NET6_0_OR_GREATER
using TNO.Logging.Reading.Deserialisers;
#endif

namespace TNO.Logging.Reading.Exceptions.System;

/// <summary>
/// A deserialiser for <see cref="IAggregateExceptionData"/>.
/// </summary>
[Guid(ExceptionGroups.System.AggregateException)]
public sealed class AggregateExceptionDataDeserialiser : IExceptionDataDeserialiser<IAggregateExceptionData>
{
   #region Fields
   private readonly IDeserialiser _deserialiser;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="AggregateExceptionDataDeserialiser"/>.</summary>
   /// <param name="deserialiser">The general <see cref="IDeserialiser"/> to use.</param>
   public AggregateExceptionDataDeserialiser(IDeserialiser deserialiser)
   {
      _deserialiser = deserialiser;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IAggregateExceptionData Deserialise(BinaryReader reader)
   {
      Collection<IExceptionInfo> innerExceptions = new Collection<IExceptionInfo>();

      int count = reader.Read7BitEncodedInt();
      for (int i = 0; i < count; i++)
      {
         _deserialiser.Deserialise(reader, out IExceptionInfo exceptionInfo);
         innerExceptions.Add(exceptionInfo);
      }

      return new AggregateExceptionData(innerExceptions);
   }
   #endregion
}
