using ParsingTextFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingTextFile.Logic
{
    public class NoBookLogic : IWordCountBaseLogic
    {
        public WordsFoundResponseMessage GetWordsAndCounts(BookToParse book)
        {
            List<WordCount> Words = new List<WordCount>();
            Words.Add(new WordCount{Word="No file found", Count =0});
            WordsFoundResponseMessage response = new WordsFoundResponseMessage();
            response.WordCounts = Words;
            return response;
        }
    }
}
