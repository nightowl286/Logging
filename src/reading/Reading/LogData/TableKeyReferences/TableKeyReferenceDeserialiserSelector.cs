using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData.Tables;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.TableKeyReferences.Versions;

namespace TNO.Logging.Reading.LogData.TableKeyReferences;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="TableKeyReference"/>.
/// </summary>
internal class TableKeyReferenceDeserialiserSelector : DeserialiserSelectorBase<TableKeyReference>
{
   public TableKeyReferenceDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<TableKeyReferenceDeserialiser0>(0);
   }
}