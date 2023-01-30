using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Writing.Abstractions;
using TNO.Logging.Writing.Abstractions.Entries;
using TNO.Logging.Writing.Abstractions.Entries.Components;
using TNO.Logging.Writing.Abstractions.Serialisers;
using TNO.Logging.Writing.Entries;
using TNO.Logging.Writing.Entries.Components;

namespace TNO.Logging.Writing;

/// <summary>
/// Represents a facade for the log writing system.
/// </summary>
public class LogWriterFacade : ILogWriterFacade
{
   #region Fields
   private readonly ServiceFacade _serviceFacade = new ServiceFacade();
   #endregion

   #region Constructors
   /// <summary>Creates a new instance of the <see cref="LogWriterFacade"/>.</summary>
   public LogWriterFacade()
   {
      RegisterSerialisers(_serviceFacade);
   }
   #endregion

   #region Methods
   /// <inheritdoc/>
   public T GetSerialiser<T>() where T : notnull, ISerialiser => _serviceFacade.Get<T>();
   #endregion

   #region Helpers
   private static void RegisterSerialisers(IServiceRegistrar registrar)
   {
      RegisterComponentSerialisers(registrar);

      registrar.Singleton<IEntrySerialiser, EntrySerialiser>();
   }
   private static void RegisterComponentSerialisers(IServiceRegistrar registrar)
   {
      registrar.Singleton<IMessageComponentSerialiser, MessageComponentSerialiser>();

      registrar.Singleton<IComponentSerialiserDispatcher, ComponentSerialiserDispatcher>();
   }
   #endregion
}
