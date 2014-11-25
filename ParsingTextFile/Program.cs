using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ParsingTextFile.Services;
using ParsingTextFile.Models;

namespace ParsingTextFile
{
    public class MainClass
    {
        public static void Main(string[] args)
        {

            WordCountService myService = new WordCountService();
            WordsFoundResponseMessage myMessage =  myService.GetWordsAndCounts();

           
            foreach(WordCount x in myMessage.WordCounts)
            {
                Console.WriteLine("{0} {1}", x.Word, x.Count);
            }

            Console.Read();

        
        }
    }
}
