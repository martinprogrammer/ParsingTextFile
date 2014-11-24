using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingTextFile.Models
{
    public class BookToParse
    {
        private string _path;

        public BookToParse(string path)
        {
            _path = path;
        }

        public string Path
        {
            get { return _path; }
        }
    }
}
