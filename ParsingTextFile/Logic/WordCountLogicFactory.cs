using ParsingTextFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingTextFile.Logic
{
    public static class WordCountLogicFactory
    {
        public static IWordCountBaseLogic GetWordCountBaseLogicFor(BookToParse book)
        {
            long fileSize = 0;

            if (book.DoesFileExist())
                fileSize = book.GetFileSize();

            IWordCountBaseLogic theLogic;
            if (fileSize == 0)
                theLogic = new NoBookLogic();
            else if (fileSize < 15500)
                theLogic = new LargeBookLogic();
            else
                theLogic = new SmallBookLogic();


            return theLogic;
        }
    }
}
