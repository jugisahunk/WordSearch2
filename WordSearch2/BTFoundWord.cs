using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class BTFoundWord : FoundWord
    {
        public BTFoundWord(string wordText, FoundWordCoordinates coordinates)
            : base(wordText, coordinates)
        {
            GetPointHandler = (FoundWord foundWord, int index) => { return new Point(foundWord.Coordinates.A.X, foundWord.Coordinates.A.Y - index); };
        }

        #region FoundWord Overrides

        public override FoundWordOrientation Orientation
        {
            get { return FoundWordOrientation.BottomToTop; }
        }
        #endregion
    }
}
