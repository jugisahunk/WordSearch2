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

        public FoundWordList() { }
        
        public FoundWordList(IEnumerable<FoundWord> foundWords) 
        {
            base.AddRange(foundWords);
        }
        #endregion

        #region new IList<FoundWord> Members

        public new bool Add(FoundWord item)
        {
            if (item != null)
            {
                FoundWord multiCharIntersect = FindMultiCharIntersect(item);
                if (multiCharIntersect == null){
                    base.Add(item);
                    return true;
                }                
            }
            return false;
        }

        #endregion

        #region Methods
        private FoundWord FindMultiCharIntersect(FoundWord item)
        {
            foreach (FoundWord foundWord in this)
                if (item.MultiCharIntersect(foundWord))
                    return foundWord;

            return null;
        }
        #endregion
    }
}
