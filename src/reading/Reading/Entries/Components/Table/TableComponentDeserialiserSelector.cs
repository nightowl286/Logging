using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Entries.Components.Table;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.Entries.Components.Table.Versions;

namespace TNO.Logging.Reading.LogData.Tables;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="ITableComponentDeserialiser"/>.
/// </summary>
internal class TableComponentDeserialiserSelector : DeserialiserSelectorBase<ITableComponentDeserialiser>, ITableComponentDeserialiserSelector
{
   public TableComponentDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<TableComponentDeserialiser0>(0);
   }
}