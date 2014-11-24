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
        private IWordCountBaseLogic WordCountLogic;

        public WordCountService()
            : this(@"c:\MobyDick.txt")
        {
        }

        public WordCountService(string path)
        {
            _path = path;
            BookToParse book = new BookToParse(_path);

            if (book.DoesFileExist())
            {

                WordsFoundRequestMessage request = new WordsFoundRequestMessage
                {
                    FilePath = _path,
                    FileSize = book.GetFileSize(),
                    FileStream = book.GetStreamReader()
                };

                WordCountLogic = WordCountLogicFactory.GetWordCountBaseLogicFor(request);
            }
            else
                throw new FileDoesntExist();

        }

        public WordsFoundResponseMessage GetWordsAndCounts()
        {
            return WordCountLogic.GetWordsAndCounts();
        }
    }
}
