using TNO.DependencyInjection.Abstractions.Components;
using TNO.Logging.Common.Abstractions;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.FileReferences;
using TNO.Logging.Reading.Deserialisers;
using TNO.Logging.Reading.FileReferences.Versions;

namespace TNO.Logging.Reading.FileReferences;

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
