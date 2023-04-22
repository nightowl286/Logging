using System.IO.Compression;
using TNO.Logging.Writing.Abstractions.Serialisers;

namespace TNO.Logging.Writing.Writers;

internal sealed class SerialiserWriter<T> : IDisposable where T : notnull
{
   #region Fields
   private readonly ISerialiser<T> _serialiser;
   private readonly ThreadedQueue<T> _queue;
   private readonly string _directory;
   private readonly long _threshold;

   private BinaryWriter _writer;
   private uint _currentChunk = 0;
   #endregion
   public SerialiserWriter(string directory, ISerialiser<T> serialiser, long threshold, ThreadPriority priority = ThreadPriority.Lowest)
   {
      Directory.CreateDirectory(directory);
      _directory = directory;
      _serialiser = serialiser;
      _threshold = threshold;

      _writer = FileSystemLogWriter.CreateWriter(GetUncompressedPath());

      _queue = new ThreadedQueue<T>(
         $"{nameof(FileSystemLogWriter)}.{nameof(T)}",
         WriteData,
         priority);
   }

   #region Methods
   public void Deposit(T data) => _queue.Enqueue(data);
   private void WriteData(T data)
   {
      _serialiser.Serialise(_writer, data);

      _writer.Flush();
      if (_writer.BaseStream.Length > _threshold)
         SwitchWriter();
   }
   private void SwitchWriter()
   {
      using (FileStream compressedFile = FileSystemLogWriter.OpenStream(GetCompressedPath()))
      using (DeflateStream compressedStream = new DeflateStream(compressedFile, CompressionLevel.Optimal))
      {
         _writer.BaseStream.Position = 0;
         _writer.BaseStream.CopyTo(compressedStream);

         _writer.Close();
      }

      File.Delete(GetUncompressedPath());
      _currentChunk++;

      _writer = FileSystemLogWriter.CreateWriter(GetUncompressedPath());
   }
   public void Dispose()
   {
      _queue.Dispose();
      _writer.Dispose();
   }
   #endregion

   #region Helpers
   private string GetCompressedPath() => Path.Combine(_directory, $"{FileSystemConstants.CompressedName}.{_currentChunk}");
   private string GetUncompressedPath() => Path.Combine(_directory, $"{FileSystemConstants.UncompressedName}.{_currentChunk}");
   #endregion
}
