using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZipTool
{
    public interface IJarZip
    {
        void CompressedFile(string path);
        void DecompressFile(string path);
    }
}
