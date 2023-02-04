using TNO.DependencyInjection;
using TNO.DependencyInjection.Abstractions;
using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
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

   /// <inheritdoc/>
   public DataVersionMap GetVersionMap()
   {
      IEnumerable<IVersioned> versioned = GetAllVersioned();

      DataVersionMap map = new DataVersionMap();
      foreach (IVersioned version in versioned)
      {
         IEnumerable<VersionedDataKind> kinds = version
            .GetType()
            .GetDataKinds();

         foreach (VersionedDataKind kind in kinds)
            map.Add(kind, version.Version);
      }

      return map;
   }
   #endregion

   #region Helpers
   private IEnumerable<IVersioned> GetAllVersioned()
   {
      IEnumerable<IVersioned> collection = _serviceFacade.GetAll<IVersioned>()
         .Where(v => v is ISerialiser);

      return collection;
   }
   private static void RegisterSerialisers(IServiceFacade facade)
   {
      RegisterComponentSerialisers(facade);

      VersionedSingleton<IEntrySerialiser, EntrySerialiser>(facade);

      facade.Singleton<IDataVersionMapSerialiser, DataVersionMapSerialiser>();
   }
   private static void RegisterComponentSerialisers(IServiceFacade facade)
   {
      VersionedSingleton<IMessageComponentSerialiser, MessageComponentSerialiser>(facade);

      facade.Singleton<IComponentSerialiserDispatcher, ComponentSerialiserDispatcher>();
   }
   private static void VersionedSingleton<TService, TType>(IServiceFacade facade)
      where TService : notnull
      where TType : notnull, TService, IVersioned
   {
      TType instance = facade.Build<TType>();
      facade.Instance<IVersioned>(instance, AppendValueMode.Append);
      facade.Instance<TService>(instance);
   }
   #endregion
}
