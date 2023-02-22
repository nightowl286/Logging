using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions.LogData;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.LogData.FileReferences;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.LogData.FileReferences.Versions;

namespace TNO.Logging.Reading.LogData.FileReferences;

/// <summary>
/// An <see cref="IDeserialiserSelector{T}"/> for versions of the <see cref="FileReference"/>.
/// </summary>
internal class FileReferenceDeserialiserSelector : DeserialiserSelectorBase<IFileReferenceDeserialiser>, IFileReferenceDeserialiserSelector
{
   public FileReferenceDeserialiserSelector(IServiceBuilder serviceBuilder) : base(serviceBuilder)
   {
      With<FileReferenceDeserialiser0>(0);
   }
}