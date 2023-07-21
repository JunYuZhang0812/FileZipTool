using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace FileZipTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("压缩文件/解压文件？ y/n");
            bool isCompress = true;
            int zipType = 0;
            var key = Console.ReadKey();
            if (key.Key == ConsoleKey.Y)
            {
                isCompress = true;
            }
            else
            {
                isCompress = false;
            }
            Console.WriteLine("\n选择压缩算法： 1 GZipStream  2 Lz4  3 Zstd(推荐)");
            key = Console.ReadKey();
            if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
            {
                zipType = 1;
            }
            else if (key.Key == ConsoleKey.D2 ||  key.Key == ConsoleKey.NumPad2)
            {
                zipType = 2;
            }
            else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
            {
                zipType = 3;
            }
            string path;
            if (args.Length > 0 )
            {
                path = args[0];
            }
            else
            {
                var exePath = Assembly.GetExecutingAssembly().Location;
                if(isCompress)
                {
                    path = Path.GetDirectoryName(exePath) + "\\Test.txt";
                }
                else
                {
                    path = Path.GetDirectoryName(exePath) + "\\Test.bin";
                }
            }
            if(isCompress)
            {
                if(zipType == 1)
                {
                    GZip.Instance.CompressedFile(path);
                }
                else if( zipType == 2)
                {
                    Lz4Zip.Instance.CompressedFile(path );
                }
                else if (zipType == 3)
                {
                    ZstdZip.Instance.CompressedFile(path);
                }
            }
            else
            {
                if (zipType == 1)
                {
                    GZip.Instance.DecompressFile(path);
                }
                else if (zipType == 2)
                {
                    Lz4Zip.Instance.DecompressFile(path);
                }
                else if (zipType == 3)
                {
                    ZstdZip.Instance.DecompressFile(path);
                }
            }
            Console.WriteLine("\n按任意键继续...");
            Console.ReadKey();
        }
    }
}
