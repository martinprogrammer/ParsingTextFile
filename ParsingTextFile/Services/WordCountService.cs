using ParsingTextFile.Exceptions;
using ParsingTextFile.Logic;
using ParsingTextFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingTextFile.Services
{
    public class WordCountService : IWordCountService
    {
        private string _path;
        private BookToParse book;
       
        public WordCountService()
            : this(@"~..\..\..\..\Books\MobyDick.txt")
        {
        }

        public WordCountService(string path)
        {
            _path = path;
             book = new BookToParse(_path);
        }

        public WordsFoundResponseMessage GetWordsAndCounts()
        {
            return book.logic.GetWordsAndCounts(book);
        }
    }
}
