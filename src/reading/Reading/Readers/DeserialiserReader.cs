using System.IO.Compression;
using TNO.Logging.Reading.Abstractions.Deserialisers;
using TNO.Logging.Reading.Abstractions.Readers;

namespace TNO.Logging.Reading.Readers;
internal class DeserialiserReader<T> : IReader<T>, IDisposable
{
   #region Fields
   private readonly IDeserialiser<T> _deserialiser;
   private readonly string _directory;

   private BinaryReader? _reader;
   private uint _currentChunk;
   #endregion
   public DeserialiserReader(string directory, IDeserialiser<T> deserialiser)
   {
      _directory = directory;
      _deserialiser = deserialiser;

      string? chunkPath = GetChunkPath(_currentChunk, out bool isCompressed);
      _reader = GetNextReader(chunkPath, isCompressed);
   }

   #region Methods
   /// <inheritdoc/>
   public bool CanRead()
   {
      if (_reader is null)
         return false;

      if (_reader.BaseStream.Position < _reader.BaseStream.Length)
         return true;

      string? nextChunkPath = GetChunkPath(_currentChunk + 1, out _);
      if (nextChunkPath is null)
         return false;

      FileInfo info = new FileInfo(nextChunkPath);
      return info.Length > 0;
   }

   /// <inheritdoc/>
   public T Read()
   {
      if (_reader is null)
         throw new InvalidOperationException($"This reader was not able to read any more data.");

      if (_reader?.BaseStream.Position < _reader?.BaseStream.Length)
         return _deserialiser.Deserialise(_reader);

      string? chunkPath =
         GetChunkPath(_currentChunk + 1, out bool isCompressed) ??
         throw new InvalidOperationException($"This reader was not able to read any more data.");

      _currentChunk++;
      SwitchReader(chunkPath, isCompressed);

      return Read();
   }

   /// <inheritdoc/>
   public void Dispose() => _reader?.Dispose();
   private void SwitchReader(string chunkPath, bool isCompressed)
   {
      _reader?.Dispose();

      _reader = GetNextReader(chunkPath, isCompressed);
   }
   private static BinaryReader? GetNextReader(string? chunkPath, bool isCompressed)
   {
      if (chunkPath is null)
         return null;

      if (isCompressed)
         return OpenCompressedReader(chunkPath);

      return FileSystemLogReader.OpenReader(chunkPath);
   }
   private static BinaryReader OpenCompressedReader(string path)
   {
      MemoryStream memStream = new MemoryStream();
      using (FileStream fs = FileSystemLogReader.OpenStream(path))
      using (DeflateStream compressedStream = new DeflateStream(fs, CompressionMode.Decompress))
         compressedStream.CopyTo(memStream);

      memStream.Position = 0;

      BinaryReader reader = new BinaryReader(memStream);
      return reader;
   }
   #endregion

   #region Helpers
   private string? GetChunkPath(uint chunk, out bool isCompressed)
   {
      string path = Path.Combine(_directory, $"{FileSystemConstants.CompressedName}.{chunk}");
      if (File.Exists(path))
      {
         isCompressed = true;
         return path;
      }

      isCompressed = false;
      path = Path.Combine(_directory, $"{FileSystemConstants.UncompressedName}.{chunk}");
      if (File.Exists(path))
         return path;

      return null;
   }
   #endregion
}