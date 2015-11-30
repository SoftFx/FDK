using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;

namespace SoftFX.Extended.Zip
{
    internal enum CompressionMethod
    {
        Store = 0,
        Deflate = 8
    };

    internal class ZipArchive
    {
        #region Private Variables

        private static readonly uint LOCAL_FILE_HEADER_SIGNATURE = 0x04034b50;
        private static readonly uint CENTRAL_FILE_HEADER_SIGNATURE = 0x02014b50;
        private static readonly uint END_OF_CENTRAL_DIR_SIGNATURE = 0x06054b50;
        private static readonly ushort VERSION_NEEDED_TO_EXTRACT = 0x0014;
        private static readonly ushort COMPRESSION_STORE = 0;
        private static readonly ushort COMPRESSION_DEFLATE = 8;
        private static readonly ushort UTF8_FILE_NAME_ENCODING_FLAG = 1 << 11;

        private static readonly int STREAM_EXCHANGE_BUFFER_SIZE = 0x1000;
        private static readonly string ARCHIVE_FORMAT_NOT_SUPPORTED_STRING = "Archive format is not supported.";

        private List<ZipEntry> entries;

        #endregion

        #region Constructors

        public ZipArchive()
        {
            entries = new List<ZipEntry>();
        }

        #endregion

        #region Public Properties

        public List<ZipEntry> Entries
        {
            get { return entries; }
            set { entries = value; }
        }

        #endregion

        #region Dll Imports

        [DllImport("kernel32.dll")]
        static extern uint GetOEMCP();

        #endregion

        #region Public Methods

        public static void Extract(Stream zipStream, string targetDirectory)
        {
            ZipArchive zipArchive = new ZipArchive();
            zipArchive.ReadFrom(zipStream);
            foreach (ZipEntry entry in zipArchive.Entries)
            {
                if (entry is ZipFileEntry)
                {
                    ZipFileEntry fileEntry = entry as ZipFileEntry;
                    string absoluteFilePath = Path.Combine(targetDirectory, fileEntry.FileName);
                    string directoryName = Path.GetDirectoryName(absoluteFilePath);
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    if (File.Exists(absoluteFilePath))
                    {
                        File.Delete(absoluteFilePath);
                    }
                    FileStream fileStream = new FileStream(absoluteFilePath, FileMode.CreateNew);
                    if (fileEntry.Data != null && fileEntry.Data.Length != 0)
                    {
                        WriteStream(fileEntry.Data, fileStream);
                    }
                    fileStream.Close();
                }
                else if (entry is ZipDirectoryEntry)
                {
                    ZipDirectoryEntry directoryEntry = entry as ZipDirectoryEntry;
                    string directoryName = Path.Combine(targetDirectory, directoryEntry.DirectoryName);
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                }
            }
        }

        public static void CreateFromFile(string sourceFile, Stream outputZipStream)
        {
            if (File.Exists(sourceFile))
            {
                ZipArchive archive = new ZipArchive();
                string fileName = Path.GetFileName(sourceFile);
                Stream fileData = new FileStream(sourceFile, FileMode.Open);
                ZipFileEntry fileEntry = new ZipFileEntry(fileName, fileData);
                archive.Entries.Add(fileEntry);
                archive.WriteTo(outputZipStream);
            }
        }

        public static void CreateFromDirectory(string sourceDirectory, Stream outputZipStream)
        {
            CreateFromDirectory(sourceDirectory, outputZipStream, true);
        }

        public static void CreateFromDirectory(string sourceDirectory, Stream outputZipStream, bool includeRootDirectoryName)
        {
            if (Directory.Exists(sourceDirectory))
            {
                DirectoryInfo rootDirectoryInfo = new DirectoryInfo(sourceDirectory);
                ZipArchive archive = new ZipArchive();
                string currentDirectory = "";
                if (includeRootDirectoryName)
                {
                    currentDirectory = rootDirectoryInfo.Name;
                    rootDirectoryInfo = rootDirectoryInfo.Parent;
                }

                bool directoryHasEntries = AddDirectory(archive, rootDirectoryInfo, currentDirectory);
                archive.WriteTo(outputZipStream);

            }
        }

        public void ReadFrom(Stream zipStream)
        {
            while (zipStream.Position < zipStream.Length)
            {
                BinaryReader reader = new BinaryReader(zipStream, Encoding.ASCII);
                uint localFileHeaderSignature = reader.ReadUInt32();
                if (localFileHeaderSignature != LOCAL_FILE_HEADER_SIGNATURE)
                {
                    // Local file entries are finished, next records are central directory records
                    break;
                }
                ushort versionNeededToExtract = reader.ReadUInt16();
                ushort generalPurposeBitFlag = reader.ReadUInt16();
                ushort compressionMethod = reader.ReadUInt16();
                if (compressionMethod != COMPRESSION_DEFLATE && compressionMethod != COMPRESSION_STORE)
                {
                    throw new NotSupportedException(ARCHIVE_FORMAT_NOT_SUPPORTED_STRING);
                }
                uint lastModifiedDateTime = reader.ReadUInt32();
                uint crc32 = reader.ReadUInt32();
                uint compressedSize = reader.ReadUInt32();
                uint uncompressedSize = reader.ReadUInt32();
                ushort fileNameLength = reader.ReadUInt16();
                ushort extraFieldLength = reader.ReadUInt16();
                if (extraFieldLength != 0)
                {
                    throw new NotSupportedException(ARCHIVE_FORMAT_NOT_SUPPORTED_STRING);
                }

                byte[] fileNameBytes = reader.ReadBytes(fileNameLength);
                Encoding fileNameEncoding;
                if ((generalPurposeBitFlag & UTF8_FILE_NAME_ENCODING_FLAG) != 0)
                {
                    fileNameEncoding = new UTF8Encoding();
                }
                else
                {
                    fileNameEncoding = Encoding.GetEncoding((int)GetOEMCP());
                }
                string fileName = new string(fileNameEncoding.GetChars(fileNameBytes));

                // According to ZIP specification, ZIP archives generally 
                // contain forward slashes so make Windows file name
                fileName = fileName.Replace('/', Path.DirectorySeparatorChar);

                ZipEntry entry = null;
                if (uncompressedSize != 0)
                {
                    byte[] fileData = reader.ReadBytes((int)compressedSize);
                    MemoryStream fileDataStream = new MemoryStream(fileData);
                    ZipFileEntry fileEntry = new ZipFileEntry(fileName, fileDataStream);
                    if (compressionMethod == COMPRESSION_DEFLATE)
                    {
                        fileDataStream.Position = 0;
                        DeflateStream deflateStream = new DeflateStream(fileDataStream, CompressionMode.Decompress);

                        byte[] uncompressedFileData = new byte[uncompressedSize];

                        deflateStream.Read(uncompressedFileData, 0, (int)uncompressedSize);
                        fileEntry.Data = new MemoryStream(uncompressedFileData);
                    }
                    entry = fileEntry;
                }
                else if (fileName.EndsWith(@"\"))
                {
                    entry = new ZipDirectoryEntry(fileName);
                }
                else
                {
                    entry = new ZipFileEntry(fileName, null);
                }
                entry.SetLastModifiedDateTime(lastModifiedDateTime);
                entries.Add(entry);
            }

        }

        public void WriteTo(Stream zipStream)
        {
            WriteTo(zipStream, CompressionMethod.Deflate);
        }

        public void WriteTo(Stream zipStream, CompressionMethod compressionMethod)
        {
            WriteTo(zipStream, CompressionMethod.Deflate, false);
        }

        public void WriteTo(Stream zipStream, CompressionMethod compressionMethod, bool encodeFileNamesInUTF8)
        {
            MemoryStream centralDirectory = new MemoryStream();
            BinaryWriter centralDirectoryWriter = new BinaryWriter(centralDirectory);
            BinaryWriter zipStreamWriter = new BinaryWriter(zipStream);
            foreach (ZipEntry entry in entries)
            {
                Encoding fileNameEncoding = encodeFileNamesInUTF8
                    ? new UTF8Encoding()
                    : Encoding.GetEncoding((int)GetOEMCP());

                uint crc32 = 0;
                uint compressedSize = 0;
                uint uncompressedSize = 0;
                string fileName = null;
                Stream compressedData = null;

                if (entry is ZipFileEntry)
                {
                    ZipFileEntry fileEntry = entry as ZipFileEntry;
                    fileName = fileEntry.FileName;

                    if (fileEntry.Data != null && fileEntry.Data.Length != 0)
                    {
                        compressedData = fileEntry.Data;
                        if (compressionMethod == CompressionMethod.Deflate)
                        {
                            MemoryStream compressedStream = new MemoryStream();
                            DeflateStream deflateStream = new DeflateStream(compressedStream, CompressionMode.Compress, true);
                            WriteStream(fileEntry.Data, deflateStream);
                            deflateStream.Close();
                            compressedData = compressedStream;
                        }
                        crc32 = fileEntry.Crc;
                        compressedSize = (uint)compressedData.Length;
                        uncompressedSize = (uint)fileEntry.Data.Length;
                    }
                    else
                    {
                        crc32 = 0;
                        compressedSize = 0;
                        uncompressedSize = 0;
                    }
                }
                else
                {
                    ZipDirectoryEntry directoryEntry = entry as ZipDirectoryEntry;
                    fileName = directoryEntry.DirectoryName;
                }
                // According to ZIP specification, file names should use only forward slashes
                // to be compatible with Unix file systems
                fileName = fileName.Replace(Path.DirectorySeparatorChar, '/');
                if (entry is ZipDirectoryEntry && !fileName.EndsWith("/"))
                {
                    // Directories should contain slash at the end
                    fileName += "/";
                }
                byte[] fileNameBytes = fileNameEncoding.GetBytes(fileName);

                ushort versionNeededToExtract = VERSION_NEEDED_TO_EXTRACT;
                ushort generalPurposeBitFlag = encodeFileNamesInUTF8 ? UTF8_FILE_NAME_ENCODING_FLAG : (ushort) 0;
                uint lastModifiedDateTime = entry.GetLastModifiedDateTime();
                ushort fileNameLength = (ushort)fileNameBytes.Length;
                ushort extraFieldLength = 0;

                uint localHeaderPosition = (uint)zipStream.Position;
                zipStreamWriter.Write(LOCAL_FILE_HEADER_SIGNATURE);
                zipStreamWriter.Write(versionNeededToExtract);
                zipStreamWriter.Write(generalPurposeBitFlag);
                zipStreamWriter.Write((ushort)compressionMethod);
                zipStreamWriter.Write(lastModifiedDateTime);
                zipStreamWriter.Write(crc32);
                zipStreamWriter.Write(compressedSize);
                zipStreamWriter.Write(uncompressedSize);
                zipStreamWriter.Write(fileNameLength);
                zipStreamWriter.Write(extraFieldLength);
                zipStreamWriter.Write(fileNameBytes, 0, fileNameBytes.Length);

                if (compressedData != null)
                {
                    WriteStream(compressedData, zipStream);
                }

                centralDirectoryWriter.Write(CENTRAL_FILE_HEADER_SIGNATURE);
                centralDirectoryWriter.Write(VERSION_NEEDED_TO_EXTRACT);
                centralDirectoryWriter.Write(VERSION_NEEDED_TO_EXTRACT);
                centralDirectoryWriter.Write(generalPurposeBitFlag);
                centralDirectoryWriter.Write((ushort)compressionMethod);
                centralDirectoryWriter.Write(lastModifiedDateTime);
                centralDirectoryWriter.Write(crc32);
                centralDirectoryWriter.Write(compressedSize);
                centralDirectoryWriter.Write(uncompressedSize);
                centralDirectoryWriter.Write(fileNameLength);
                centralDirectoryWriter.Write(extraFieldLength);
                centralDirectoryWriter.Write((ushort)0); // file comment length
                centralDirectoryWriter.Write((ushort)0); // disk number start
                centralDirectoryWriter.Write((ushort)0); // internal file attributes
                centralDirectoryWriter.Write((uint)0); // external file attributes
                centralDirectoryWriter.Write(localHeaderPosition); // relative offset of local header
                centralDirectoryWriter.Write(fileNameBytes, 0, fileNameBytes.Length);
            }
            uint centralDirectorySize = (uint)centralDirectory.Length;
            uint centralDirectoryOffset = (uint)zipStream.Position;
            centralDirectoryWriter.Write(END_OF_CENTRAL_DIR_SIGNATURE);
            centralDirectoryWriter.Write((ushort)0); // number of this disk
            centralDirectoryWriter.Write((ushort)0); // number of the disk with the start of the central directory
            centralDirectoryWriter.Write((ushort)entries.Count); // total number of entries in the central directory on this disk
            centralDirectoryWriter.Write((ushort)entries.Count); // total number of entries in the central directory
            centralDirectoryWriter.Write((uint)centralDirectorySize); // size of the central directory
            centralDirectoryWriter.Write((uint)centralDirectoryOffset); // offset of start of central directory with respect to the starting disk number 
            centralDirectoryWriter.Write((ushort)0); // .ZIP file comment length
            WriteStream(centralDirectory, zipStream);
        }

        #endregion

        #region Private Methods

        private static void WriteStream(Stream sourceStream, Stream targetStream)
        {
            sourceStream.Position = 0;
            byte[] buffer = new byte[STREAM_EXCHANGE_BUFFER_SIZE];
            while (true)
            {
                int bytesRead = sourceStream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    break;
                }
                targetStream.Write(buffer, 0, bytesRead);
            }
        }

        private static bool AddDirectory(ZipArchive zipArchive, DirectoryInfo rootDirectoryInfo, string currentDirectory)
        {
            string fullDirectoryPath = Path.Combine(rootDirectoryInfo.FullName, currentDirectory);
            DirectoryInfo directoryInfo = new DirectoryInfo(fullDirectoryPath);
            FileSystemInfo[] fileSystemInfos = directoryInfo.GetFileSystemInfos();
            foreach (FileSystemInfo fileSystemInfo in fileSystemInfos)
            {
                if (fileSystemInfo is FileInfo)
                {
                    FileInfo childFileInfo = fileSystemInfo as FileInfo;
                    string fileName = Path.Combine(currentDirectory, childFileInfo.Name);
                    FileStream fileData = new FileStream(childFileInfo.FullName, FileMode.Open);
                    ZipFileEntry fileEntry = new ZipFileEntry(fileName, fileData);
                    zipArchive.Entries.Add(fileEntry);
                }
                else if (fileSystemInfo is DirectoryInfo)
                {
                    DirectoryInfo childDirectoryInfo = fileSystemInfo as DirectoryInfo;
                    string directoryName = Path.Combine(currentDirectory, childDirectoryInfo.Name);
                    bool directoryHasEntries = AddDirectory(zipArchive, rootDirectoryInfo, directoryName);
                    if (!directoryHasEntries)
                    {
                        ZipDirectoryEntry directoryEntry = new ZipDirectoryEntry(directoryName);
                        zipArchive.Entries.Add(directoryEntry);
                    }
                }
            }
            return (fileSystemInfos.Length != 0);
        }

        #endregion
    }
}
