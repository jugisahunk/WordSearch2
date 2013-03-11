using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class TBLRFoundWord : FoundWord
    {
        public TBLRFoundWord(string text, int startingIndex, CharacterGrid characterGrid)
            : base(text, characterGrid)
        {
            Coordinates = new FoundWordCoordinates(
                new Point(startingIndex & Grid.ColumnCount, startingIndex / Grid.ColumnCount),
                new Point((startingIndex & Grid.ColumnCount) + Length - 1, (startingIndex / Grid.ColumnCount) + Length - 1));

            GetPointHandler = (FoundWord foundWord, int index) => { return new Point(foundWord.Coordinates.A.X + index, foundWord.Coordinates.A.Y + index); };
        }

        #region FoundWord Overrides
        public override FoundWordOrientation Orientation
        {
            get { return FoundWordOrientation.TopToBottomLeftToRight; }
        }
        #endregion
    }
}
