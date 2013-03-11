using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class BTRLFoundWord : FoundWord
    {
        public BTRLFoundWord(string text, FoundWordCoordinates coordinates)
            : base(text, coordinates)
        {
            GetPointHandler = (FoundWord foundWord, int index) => { return new Point(foundWord.Coordinates.A.X - index, foundWord.Coordinates.A.Y - index); };
        }

        #region FoundWord Overrides

        public override FoundWordOrientation Orientation
        {
            get { return FoundWordOrientation.BottomToTopRightToLeft; }
        }
        #endregion
    }
}
