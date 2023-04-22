using TNO.Logging.Abstractions.Scopes;
using TNO.Logging.Writing.Abstractions.Collectors;
using TNO.Logging.Writing.Abstractions.Exceptions;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Abstractions;

/// <summary>
/// Denotes a logger configurator.
/// </summary>
public interface ILoggerConfigurator
{
   #region Properties
   /// <summary>The general <see cref="ISerialiser"/> that the created logger will use.</summary>
   ISerialiser Serialiser { get; }

   /// <summary>The distributor that the created logger will use.</summary>
   ILogDataDistributor Distributor { get; }
   #endregion

   #region Methods
   /// <summary>Adds the given <paramref name="collector"/> to the distributor that will be used.</summary>
   /// <param name="collector">The collector to add.</param>
   /// <returns>The current logger configurator.</returns>
   ILoggerConfigurator With(ILogDataCollector collector);

   /// <summary>Toggles whether to use an internal logger, this is <see langword="true"/> by default.</summary>
   /// <returns>The current logger configurator.</returns>
   /// <remarks>Please only use this if you know what you are doing.</remarks>
   ILoggerConfigurator WithInternalLogger(bool includeInternalLogger);

   /// <summary>Uses the given <paramref name="registrant"/> to register extra serialisers.</summary>
   /// <param name="registrant">The registrant that will register extra serialisers.</param>
   /// <returns>The current logger configurator.</returns>
   ILoggerConfigurator WithRegistrant(ISerialiserRegistrant registrant);

   /// <summary>Uses the given <paramref name="registrant"/> to register extra exception data handlers.</summary>
   /// <param name="registrant">The registrant that will register extra exception data handlers.</param>
   /// <returns>The current logger configurator.</returns>
   ILoggerConfigurator WithRegistrant(IExceptionDataHandlerRegistrant registrant);

   /// <summary>Created the final logger that should be used.</summary>
   /// <returns>The logger that should be used.</returns>
   IContextLogger Create();
   #endregion
}
