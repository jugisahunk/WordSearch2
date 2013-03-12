using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class DescendingComparer : IComparer<string>
    {
        public int Compare(string a, string b)
        {
            return -string.Compare(a, b);
        }
    }

    public class DescendingWordComparer : IComparer<Word>
    {
        public int Compare(Word a, Word b)
        {
            return -string.Compare(a.Text, b.Text);
        }
    }
}