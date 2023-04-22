using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Exceptions;

namespace TNO.Logging.Reading.Abstractions;

/// <summary>
/// Denotes a log reader configurator.
/// </summary>
public interface ILogReaderConfigurator
{
   #region Methods
   /// <summary>Uses the given <paramref name="registrant"/> to register extra deserialisers.</summary>
   /// <param name="registrant">The registrant that will register extra deserialisers.</param>
   /// <returns>The current log reader configurator.</returns>
   ILogReaderConfigurator WithRegistrant(IDeserialiserRegistrant registrant);

   /// <summary>Uses the given <paramref name="registrant"/> to register extra exception data deserialisers.</summary>
   /// <param name="registrant">The registrant that will register extra exception data deserialisers.</param>
   /// <returns>The current log reader configurator.</returns>
   ILogReaderConfigurator WithRegistrant(IExceptionDataDeserialiserRegistrant registrant);

   /// <summary>Gets an <see cref="IServiceScope"/> that can be passed into different log readers.</summary>
   /// <returns>The created <see cref="IServiceScope"/>.</returns>
   IServiceScope GetScope();
   #endregion
}
