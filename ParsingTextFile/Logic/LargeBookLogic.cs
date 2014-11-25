using ParsingTextFile.Logic.Trier;
using ParsingTextFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingTextFile.Logic
{
    public class LargeBookLogic : IWordCountBaseLogic
    {
        private List<string> words;
        private WordsFoundResponseMessage response;
         public LargeBookLogic()
        {
            words = new List<string>();
            response = new WordsFoundResponseMessage();
        }
        public WordsFoundResponseMessage GetWordsAndCounts(BookToParse book)
        {
            Console.WriteLine();
            Console.WriteLine("Trie method - please be patient ;-)");
            response.WordCounts = TrierMain.GetTrier(book);
            return response;
            
        }
    }
}
