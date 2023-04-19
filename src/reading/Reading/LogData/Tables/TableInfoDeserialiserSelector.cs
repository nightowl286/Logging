using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.Tables.Versions;

namespace TNO.Logging.Reading.LogData.Tables;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="ITableInfo"/>.
/// </summary>
internal class TableInfoDeserialiserSelector : DeserialiserSelectorBase<ITableInfo>
{
   public TableInfoDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<TableInfoDeserialiser0>(0);
   }
}