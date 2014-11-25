using ParsingTextFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;
using System.IO;

namespace ParsingTextFile.Logic
{
    public class SmallBookLogic : IWordCountBaseLogic
    {
        private List<string> words;
        private WordsFoundResponseMessage response;

        public SmallBookLogic()
        {
            words = new List<string>();
            response = new WordsFoundResponseMessage();
        }
        public WordsFoundResponseMessage GetWordsAndCounts(BookToParse book)
        {
            var response = new WordsFoundResponseMessage();
            response.WordCounts =  book.GetWordList().GroupBy(p => p).Select(p => new WordCount { Word = p.Key, Count = p.Count() }).OrderBy(p=>p.Count);
            return response;
        }
      
    }
}
