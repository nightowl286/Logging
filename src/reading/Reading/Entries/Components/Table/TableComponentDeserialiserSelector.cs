using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.Entries.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Table.Versions;

namespace TNO.Logging.Reading.LogData.Tables;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="ITableComponent"/>.
/// </summary>
internal class TableComponentDeserialiserSelector : DeserialiserSelectorBase<ITableComponent>
{
   public TableComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<TableComponentDeserialiser0>(0);
   }
}