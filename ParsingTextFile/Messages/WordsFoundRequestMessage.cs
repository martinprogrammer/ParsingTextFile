using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingTextFile
{
    public class WordsFoundRequestMessage
    {
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public StreamReader FileStream { get; set; }
    }
}
