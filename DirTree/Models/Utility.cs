using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DirTree.Models
{
    internal static class Utility
    {
        public static long GetSizeOfFiles(this DirectoryInfo directory)
        {
            try
            {
                long size = 0;
                foreach (var file in directory.GetFiles())
                {
                    size += file.Length;
                }
                return size;
            }
            catch(UnauthorizedAccessException e)
            {
                return 0;
            }
        }
    }
}
