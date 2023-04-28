using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using TNO.Logging.Common.Abstractions.LogData.Exceptions;
using TNO.Logging.Common.Exceptions.Abstractions;
using TNO.Logging.Common.Exceptions.Abstractions.System;
using TNO.Logging.Common.Exceptions.System;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Serialisers;

namespace TNO.Logging.Writing.Exceptions.System;

/// <summary>
/// Converts and serialises exceptions of the <see cref="AggregateException"/> type.
/// </summary>
[Guid(ExceptionGroups.System.AggregateException)]
public sealed class AggregateExceptionHandler : IExceptionDataHandler<AggregateException, IAggregateExceptionData>
{
   #region Fields
   private readonly IExceptionInfoHandler _exceptionInfoHandler;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="AggregateExceptionHandler"/>.</summary>
   /// <param name="exceptionInfoHandler">The <see cref="IExceptionInfoHandler"/> to use.</param>
   public AggregateExceptionHandler(IExceptionInfoHandler exceptionInfoHandler)
   {
      _exceptionInfoHandler = exceptionInfoHandler;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public IAggregateExceptionData Convert(AggregateException exception)
   {
      Collection<IExceptionInfo> innerExceptions = new Collection<IExceptionInfo>();
      foreach (Exception inner in exception.InnerExceptions)
      {
         IExceptionInfo converted = _exceptionInfoHandler.Convert(inner, null);
         innerExceptions.Add(converted);
      }

      return new AggregateExceptionData(innerExceptions);
   }

   /// <inheritdoc/>
   public void Serialise(BinaryWriter writer, IAggregateExceptionData data)
   {
      int count = data.InnerExceptions.Count;
      writer.Write7BitEncodedInt(count);

      foreach (IExceptionInfo exceptionInfo in data.InnerExceptions)
         _exceptionInfoHandler.Serialise(writer, exceptionInfo);
   }

   /// <inheritdoc/>
   public int Count(IAggregateExceptionData data)
   {
      int countSize = BinaryWriterSizeHelper.Encoded7BitIntSize(data.InnerExceptions.Count);
      int total = countSize;

      foreach (IExceptionInfo exceptionInfo in data.InnerExceptions)
         total += _exceptionInfoHandler.Count(exceptionInfo);

      return total;
   }
   #endregion
}
