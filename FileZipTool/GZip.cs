using System.IO;
using System.IO.Compression;

namespace FileZipTool
{
    public class GZip: IJarZip
    {
        private static GZip _instance;
        public static GZip Instance
        {
            get
            {
                if( _instance == null )
                    _instance = new GZip();
                return _instance;
            }
        }
        //压缩文件
        public void CompressedFile( string path )
        {
            var bytes = File.ReadAllBytes( path );
            var ex = Path.GetExtension( path );
            var newPath = path.Replace(ex, ".bin");
            byte[] compressedData;
            using( var ms = new MemoryStream() )
            {
                using( var gzip = new GZipStream(ms , CompressionMode.Compress))
                {
                    gzip.Write(bytes, 0, bytes.Length);
                }
                compressedData = ms.ToArray();
            }
            File.WriteAllBytes(newPath, compressedData);
        }
        //解压文件
        public void DecompressFile(string path)
        {
            var ex = Path.GetExtension(path);
            var newPath = path.Replace(ex, ".txt");
            byte[] compressedData;
            var bytes = File.ReadAllBytes(path);
            using( var ms = new MemoryStream(bytes) )
            {
                using (var dms = new MemoryStream())
                {
                    using (var gzip = new GZipStream(ms, CompressionMode.Decompress))
                    {
                        gzip.CopyTo(dms);
                    }
                    compressedData = dms.ToArray();
                }
            }
            File.WriteAllBytes(newPath, compressedData);
        }
        //压缩文件
        public static void CompressFile(string inputFilePath, string outputFilePath)
        {
            using (FileStream inputFile = File.OpenRead(inputFilePath))
            {
                using (FileStream outputFile = File.Create(outputFilePath))
                {
                    using (GZipStream gzipStream = new GZipStream(outputFile, CompressionMode.Compress))
                    {
                        inputFile.CopyTo(gzipStream);
                    }
                }
            }
        }
        //解压文件
        public static void DecompressFile(string inputFilePath, string outputFilePath)
        {
            using (FileStream inputFile = File.OpenRead(inputFilePath))
            {
                using (FileStream outputFile = File.Create(outputFilePath))
                {
                    using (GZipStream gzipStream = new GZipStream(inputFile, CompressionMode.Decompress))
                    {
                        gzipStream.CopyTo(outputFile);
                    }
                }
            }
        }
    }
}
