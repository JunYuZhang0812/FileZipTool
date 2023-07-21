using System.IO;
using ZstdNet;

namespace FileZipTool
{
    public class ZstdZip : IJarZip
    {
        private static ZstdZip _instance;
        public static ZstdZip Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ZstdZip();
                return _instance;
            }
        }
        public void CompressedFile(string path)
        {
            var bytes = File.ReadAllBytes(path);
            var ex = Path.GetExtension(path);
            var newPath = path.Replace(ex, ".bin");
            byte[] compressedData;
            using (var ms = new MemoryStream())
            {
                using (var compressor = new CompressionStream(ms))
                {
                    compressor.Write(bytes, 0, bytes.Length);
                }
                compressedData = ms.ToArray();
            }
            File.WriteAllBytes(newPath, compressedData);
        }

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
                    using (var decompressor = new DecompressionStream(ms))
                    {
                        decompressor.CopyTo(dms);
                    }
                    compressedData = dms.ToArray();
                }
            }
            File.WriteAllBytes(newPath, compressedData);
        }
    }
}
