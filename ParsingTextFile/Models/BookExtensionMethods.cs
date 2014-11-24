using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingTextFile.Models
{
    public static class BookExtensionMethods
    {
        public static long GetFileSize(this BookToParse book)
        {
            return new FileInfo(book.Path).Length;
        }

        public static StreamReader GetStreamReader(this BookToParse book)
        {

            using (FileStream fileStream = File.Open(book.Path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bufferedStream = new BufferedStream(fileStream))
            using (StreamReader streamReader = new StreamReader(bufferedStream))
            {
                return streamReader;
            }
        }

        public static bool DoesFileExist(this BookToParse book)
        {
            return File.Exists(book.Path);
        }

    }
}
