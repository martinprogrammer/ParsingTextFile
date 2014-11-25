using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using ParsingTextFile.Models;

namespace ParsingTextFile
{
    public class WordsFoundResponseMessage
    {
        public IEnumerable<WordCount> WordCounts { get; set; }
    }
}
