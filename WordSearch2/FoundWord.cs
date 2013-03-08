using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class FoundWord
    {
        public FoundWord(Word word, Point a, Point b)
        {
            Word = word;
            A = a;
            B = b;
        }

        public Word Word { get; set; }
        public Point A { get; set; }
        public Point B { get; set; }
    }
}
