using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Text;
using TNO.Logging.Writer.Abstractions;

namespace TNO.Logging.Writer.Loggers.Writers;

internal class FileSystemWriter : BaseWriter
{
   #region Fields
   private static readonly Encoding Encoding = Encoding.UTF8;
   private readonly string _directoryPath;

   private BinaryWriter _fileRefTable;
   private BinaryWriter _assemblyRefTable;
   private BinaryWriter _typeRefTable;
   private BinaryWriter _tagRefTable;
   private BinaryWriter _entryLinksTable;

   private BinaryWriter _entryOffetsTable;
   private BinaryWriter _contextHierarchyTable;

   private BinaryWriter _entriesFile;
   #endregion
   public FileSystemWriter(string directoryPath)
   {
      _directoryPath = directoryPath;
      Directory.CreateDirectory(directoryPath);

      ThrowIfDirectoryIsInvalid(directoryPath);

      SetupTables();
      SetupEntriesFile();
   }

   #region Methods
   public override void Close()
   {
      base.Close();

      _fileRefTable.Dispose();
      _assemblyRefTable.Dispose();
      _typeRefTable.Dispose();
      _tagRefTable.Dispose();

      _entryOffetsTable.Dispose();
      _contextHierarchyTable.Dispose();
      _entryLinksTable.Dispose();

      _entriesFile.Dispose();

      ZipLogDirectory();
      Directory.Delete(_directoryPath, true);
   }
   #endregion

   #region Write Methods
   protected override void Write(Context context) => Serialise(_contextHierarchyTable, context);
   protected override void Write(Tag tag) => Serialise(_tagRefTable, tag);
   protected override void Write(Links links) => Serialise(_entryLinksTable, links);
   protected override void Write(FileReference fileReference) => Serialise(_fileRefTable, fileReference);
   protected override void Write(ILogEntry entry)
   {
      _entryOffetsTable.Write(_entriesFile.BaseStream.Position);
      Serialise(_entriesFile, entry);
   }
   #endregion

   #region Setup Methods
   private void ZipLogDirectory()
   {
      string name = Path.GetFileName(_directoryPath);
      string parentFolder = Path.GetDirectoryName(_directoryPath)!;
      string zipPath = Path.Combine(parentFolder, name + ".zip");

      using (FileStream fs = new FileStream(zipPath, FileMode.CreateNew, FileAccess.Write))
      {
         using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Create))
         {
            foreach (string file in Directory.EnumerateFiles(_directoryPath, "*", SearchOption.AllDirectories))
            {
               string entryName = file.Substring(_directoryPath.Length);
               archive.CreateEntryFromFile(file, entryName, CompressionLevel.SmallestSize);
            }
         }
      }
   }

   [MemberNotNull(nameof(_fileRefTable))]
   [MemberNotNull(nameof(_assemblyRefTable))]
   [MemberNotNull(nameof(_typeRefTable))]
   [MemberNotNull(nameof(_tagRefTable))]
   [MemberNotNull(nameof(_entryOffetsTable))]
   [MemberNotNull(nameof(_contextHierarchyTable))]
   [MemberNotNull(nameof(_entryLinksTable))]
   private void SetupTables()
   {
      string tablesPath = Path.Combine(_directoryPath, "tables");

      _fileRefTable = OpenForWrite(Path.Combine(tablesPath, "file_refs"));
      _assemblyRefTable = OpenForWrite(Path.Combine(tablesPath, "assembly_refs"));
      _typeRefTable = OpenForWrite(Path.Combine(tablesPath, "type_refs"));
      _tagRefTable = OpenForWrite(Path.Combine(tablesPath, "tag_refs"));

      _entryLinksTable = OpenForWrite(Path.Combine(tablesPath, "entry_links"));
      _entryOffetsTable = OpenForWrite(Path.Combine(tablesPath, "entry_offsets"));
      _contextHierarchyTable = OpenForWrite(Path.Combine(tablesPath, "context_hierarchy"));
   }

   [MemberNotNull(nameof(_entriesFile))]
   private void SetupEntriesFile()
   {
      _entriesFile = OpenForWrite("entries");
   }
   private static BinaryWriter OpenForWriteCompressed(string path)
   {
      FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
      DeflateStream ds = new DeflateStream(fs, CompressionLevel.SmallestSize);
      BinaryWriter writer = new BinaryWriter(ds, Encoding);

      return writer;
   }
   private static BinaryWriter OpenForWrite(string path)
   {
      FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
      BinaryWriter writer = new BinaryWriter(fs, Encoding);

      return writer;
   }
   private static void ThrowIfDirectoryIsInvalid(string directoryPath)
   {
      bool notEmpty = Directory
         .EnumerateDirectories(directoryPath)
         .Concat(Directory.EnumerateFiles(directoryPath))
         .Any();

      if (notEmpty)
         throw new ArgumentException($"The given directory ({directoryPath}) was not empty.", nameof(directoryPath));

      bool isRoot = Path.GetDirectoryName(directoryPath) == null;

      if (isRoot)
         throw new ArgumentException($"The given directory ({directoryPath}) is a root directory.", nameof(directoryPath));
   }
   #endregion
}
