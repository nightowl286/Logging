using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Serialisers.Bases;
using TNO.Logging.Writing.Builders;
using TNO.Logging.Writing.SerialiserProviders;

namespace TNO.Logging.Writing;

/// <summary>
/// Represents a facade for the log writing system.
/// </summary>
public class LogWriterFacade : ILogWriterFacade
{
   #region Fields
   private readonly IServiceScope _scope = new ServiceFacade().CreateNew();
   private readonly ISerialiserProvider _baseProvider;
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="LogWriterFacade"/>.</summary>
   public LogWriterFacade()
   {
      _baseProvider = new LockingSerialiserProvider(
         new VersionedSerialiserProvider(_scope,
            new NonVersionedSerialiserProvider(_scope)));
   }

   /// <inheritdoc/>
   public ILoggerConfigurator CreateConfigurator()
   {
      LoggerConfigurator builder = new LoggerConfigurator(this, _scope);

      return builder;
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public T GetSerialiser<T>() where T : notnull, ISerialiser => _baseProvider.GetSerialiser<T>();

   /// <inheritdoc/>
   public DataVersionMap GetVersionMap() => _baseProvider.GetVersionMap();
   #endregion
}