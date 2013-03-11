using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class RLFoundWord : FoundWord
    {
        public RLFoundWord(string wordText, int startingIndex, CharacterGrid characterGrid)
            : base(wordText, characterGrid)
        {
            Coordinates = new FoundWordCoordinates(
                new Point(startingIndex & Grid.ColumnCount, startingIndex / Grid.ColumnCount),
                new Point((startingIndex & Grid.ColumnCount) - Length + 1, startingIndex / Grid.ColumnCount));

            GetPointHandler = (FoundWord foundWord, int index) => { return new Point(foundWord.Coordinates.A.X - index, foundWord.Coordinates.A.Y); };
        }

        #region FoundWord Overrides

        public override FoundWordOrientation Orientation
        {
            get { return FoundWordOrientation.RightToLeft; }
        }
        #endregion
    }
}
