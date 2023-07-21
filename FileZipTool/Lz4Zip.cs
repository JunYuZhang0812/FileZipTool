using LZ4;
using System.IO;

namespace FileZipTool
{
    public class Lz4Zip: IJarZip
    {
        private static Lz4Zip _instance;
        public static Lz4Zip Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Lz4Zip();
                return _instance;
            }
        }
        //压缩文件
        public void CompressedFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var ex = Path.GetExtension(path);
            var newPath = path.Replace(ex, ".bin");
            byte[] compressedData;
            using (var ms = new MemoryStream())
            {
                using (var gzip = new LZ4Stream(ms,LZ4StreamMode.Compress ))
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
            using (var ms = new MemoryStream(bytes))
            {
                using (var dms = new MemoryStream())
                {
                    using (var gzip = new LZ4Stream(ms, LZ4StreamMode.Decompress))
                    {
                        gzip.CopyTo(dms);
                    }
                    compressedData = dms.ToArray();
                }
            }
            File.WriteAllBytes(newPath, compressedData);
        }
    }
}
