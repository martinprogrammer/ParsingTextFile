using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingTextFile.Logic
{
    public static class WordCountLogicFactory
    {
        public static IWordCountBaseLogic GetWordCountBaseLogicFor(WordsFoundRequestMessage request)
        {

            long fileSize = request.FileSize;
            IWordCountBaseLogic theLogic;

            if (fileSize < 500)
                theLogic = new SmallBookLogic();
            else
                theLogic = new LargeBookLogic();

            return theLogic;
        }
    }
}
