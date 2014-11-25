using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ParsingTextFile.Logic;

namespace ParsingTextFile.Models
{
    public class BookToParse
    {
        private string _path;
        public IWordCountBaseLogic logic { get; set; }
        public BookToParse(string path)
        {
            _path = path;
            logic = WordCountLogicFactory.GetWordCountBaseLogicFor(this);
        }

        public string Path
        {
            get { return _path; }
        }
    }
}
