using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class FoundWord
    {
        public FoundWord(Word word, FoundWordCoordinates coordinates)
        {
            Word = word;
            Coordinates = coordinates;
        }

        public Word Word { get; set; }
        public FoundWordCoordinates Coordinates { get; private set; }
    }
}
