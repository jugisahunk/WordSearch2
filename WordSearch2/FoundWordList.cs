using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class FoundWordList : List<FoundWord>
    {

        #region .ctor
        public FoundWordList() : this(null) { }

        public FoundWordList(IEnumerable<FoundWord> foundWords)
        {
            Items = new List<FoundWord>(foundWords);
        }
        #endregion

        List<FoundWord> Items;

        #region new IList<FoundWord> Members

        public new void Insert(int index, FoundWord item)
        {
            throw new NotImplementedException();
        }

        public new void Add(FoundWord item)
        {
            FoundWord multiCharIntersect = FindMultiCharIntersect(item);
            if (multiCharIntersect == null)
                Items.Add(item);
            else if (item.Length > multiCharIntersect.Length)
            {
                Items.Remove(multiCharIntersect);
                Items.Add(item);
            }
        }
        #endregion

        #region Methods
        private FoundWord FindMultiCharIntersect(FoundWord item)
        {
            foreach (FoundWord foundWord in Items)
                if (item.MultiCharIntersect(foundWord))
                    return foundWord;

            return null;
        }
        #endregion
    }
}
