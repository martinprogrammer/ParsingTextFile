using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingTextFile.Logic
{
    public interface IWordCountBaseLogic
    {
        WordsFoundResponseMessage GetWordsAndCounts();
    }
}
