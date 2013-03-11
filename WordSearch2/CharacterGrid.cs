using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class CharacterGrid
    {
        #region .ctor
        public CharacterGrid(int rowCount, int columnCount, char[] characters)
        {
            if (characters.Length % columnCount != 0)
                throw new ArgumentException("Column count and character array do not match a square or rectangular grid.");

            RowCount = rowCount;
            ColumnCount = columnCount;
            Characters = characters;
        }
        #endregion

        #region Properties
        public char[] Characters { get; private set; }
        public int RowCount { get; private set; }
        public int ColumnCount { get; private set; }
        public List<FoundWord> FoundWords { get; set; }
        #endregion

        #region Helper Methods

        private bool ArrayEquals(char[] A, char[] B)
        {
            if (A.Length != B.Length) return false;

            for (int i = 0; i < A.Length; i++)
                if (A[i] != B[i])
                    return false;

            return true;
        }

        private bool ConfirmWord(FoundWord possibleFoundWord)
        {
            foreach (FoundWord foundWord in FoundWords)
                if (FoundWord.MultiCharIntersect(possibleFoundWord, foundWord))
                    return false;

            return true;
        }

        #endregion

        #region Methods  

        private FoundWord ExtractFoundWordVerticalBottomToTop(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = startIndex / ColumnCount;

            for (int i = 0; i < word.Length; i++)
                possibleWord[i] = Characters[startIndex - ColumnCount * i];

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1,
                    y1 = startingRowIndex,
                    y2 = y1 + word.Length - 1;

                return new BTFoundWord(word.Text, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
            }

            return null;
        }

        private FoundWord ExtractFoundWordVerticalTopToBottom(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = startIndex / ColumnCount;
                        
            for (int i = 0; i < word.Length; i++)
                possibleWord[i] = Characters[startIndex + ColumnCount * i];

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1,
                    y1 = startingRowIndex,
                    y2 = y1 + word.Length - 1;

                return new TBFoundWord(word.Text, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
            }

            return null;
        }
        
        private FoundWord ExtractFoundWordDiagonalBottomToTopLeftToRight(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = startIndex / ColumnCount;

            possibleWord[0] = Characters[startIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[startIndex - ColumnCount * i + 1];

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1 + word.Length -1,
                    y1 = startingRowIndex,
                    y2 = y1 - word.Length + 1;

                return new BTLRFoundWord(word.Text, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
            }

            return null;
        }
        
        private FoundWord ExtractFoundWordDiagonalTopToBottomRightToLeft(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = startIndex / ColumnCount;

            possibleWord[0] = Characters[startIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[startIndex + ColumnCount * i - 1];

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1 - word.Length + 1,
                    y1 = startingRowIndex,
                    y2 = y1 + word.Length - 1;

                return new TBRLFoundWord(word.Text, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
            }

            return null;
        }

        private FoundWord ExtractFoundWordDiagonalBottomToTopRightToLeft(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = startIndex / ColumnCount;

            possibleWord[0] = Characters[startIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[startIndex - ColumnCount * i - 1];

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1 + word.Length -1,
                    y1 = startingRowIndex,
                    y2 = y1 + word.Length -1;

                return new BTRLFoundWord(word.Text, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
            }

            return null;
        }
        
        private FoundWord ExtractFoundWordDiagonalTopToBottomLeftToRight(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = startIndex / ColumnCount;

            possibleWord[0] = Characters[startIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[startIndex + ColumnCount * i + 1];

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1 + word.Length - 1,
                    y1 = startingRowIndex,
                    y2 = y1 + word.Length - 1;

                return new TBLRFoundWord(word.Text, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
            }

            return null;
        }

        private FoundWord ExtractFoundWordHorizontalRightToLeft(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];

            Characters.CopyTo(possibleWord, startIndex - word.Length);

            if (ArrayEquals(possibleWord, word.Text.ToCharArray())){
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1 - word.Length + 1,
                    y1 = startIndex / ColumnCount,
                    y2 = y1;
                return new RLFoundWord(word.Text,new FoundWordCoordinates(new Point(x1,y1), new Point(x2, y2)));
            }

            return null;
        }

        private FoundWord ExtractFoundWordHorizontalLeftToRight(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];

            for (int i = 0; i < possibleWord.Length; i++)
                possibleWord[i] = Characters[startIndex + i];

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1 + word.Length - 1,
                    y1 = startIndex / ColumnCount,
                    y2 = y1;
                return new LRFoundWord(word.Text, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
            }

            return null;
        }

        internal FoundWord LookHorizontally(Word word, int charIndex){
            // ColumnCount 10, charIndex 5, char to left 5, char to right 4
            int
                charactersToLeft = charIndex % ColumnCount,
                charactersToRight = ColumnCount - charactersToLeft - 1;

			FoundWord foundWord = null;

            if (charactersToRight >= word.Length)
				foundWord = ExtractFoundWordHorizontalLeftToRight(word, charIndex);
            
            if(foundWord == null && charactersToLeft >= word.Length)
				return 	ExtractFoundWordHorizontalRightToLeft(word, charIndex);

            return foundWord;
        }
        
        internal FoundWord LookVertically(Word word, int charIndex){
        	//ColumnCount 20, RowCount 10, charIndex 15, rowIndex 0, charactersAbove 0, charactersBelow 9
        	int
        		rowIndex = charIndex / ColumnCount,
        		charactersAbove = rowIndex,
        		charactersBelow = RowCount - rowIndex -1;
        		
        	FoundWord foundWord = null;
        		
        	if(charactersAbove >= word.Length)
                foundWord = ExtractFoundWordVerticalBottomToTop(word, charIndex);
        	
        	if(foundWord == null && charactersBelow >= word.Length)
                foundWord = ExtractFoundWordVerticalTopToBottom(word, charIndex);

            return foundWord; 	
        }
        
        internal FoundWord LookDiagonally(Word word, int charIndex){
        	//ColumnCount 20, RowCount 10, charIndex 15, rowIndex 0, charactersAbove 0, charactersBelow 9
        	int
        		rowIndex = charIndex / ColumnCount,
        		charactersAbove = rowIndex,
        		charactersBelow = RowCount - rowIndex -1,
                charactersToLeft = charIndex % ColumnCount,
                charactersToRight = ColumnCount - charactersToLeft - 1;

            FoundWord foundWord = null;

            if (charactersAbove >= word.Length)
            {
                if (charactersToRight >= word.Length)
                    foundWord = ExtractFoundWordDiagonalTopToBottomLeftToRight(word, charIndex);
                if (charactersToLeft >= word.Length)
                    foundWord = ExtractFoundWordDiagonalTopToBottomRightToLeft(word, charIndex);
            }
            else
            {
                if (charactersToRight >= word.Length)
                    foundWord = ExtractFoundWordDiagonalBottomToTopLeftToRight(word, charIndex);
                if (charactersToLeft >= word.Length)
                    foundWord = ExtractFoundWordDiagonalBottomToTopRightToLeft(word, charIndex);
            }

            return foundWord;
        }

        internal FoundWord LookForWord(Word word, int currentIndex)
        {
            FoundWord foundWord = null;

            //look horizontally
            foundWord = LookHorizontally(word, currentIndex);

            //look vertically
            if (foundWord == null)
                foundWord = LookVertically(word, currentIndex);

            //look diagonally
            if (foundWord == null)
                foundWord = LookDiagonally(word, currentIndex);

            return foundWord;
        }

        public List<FoundWord> FindWords(List<Word> words)
        {
            List<FoundWord> foundWords = new List<FoundWord>();

            for (int i = 0; i < Characters.Length; i++)
            {
                char currentCharacter = Characters[i];

                IEnumerable<Word> startsWithCurrent = words.Where(x => x.FirstCharacter == currentCharacter);

                foreach (Word word in startsWithCurrent)
                {
                    FoundWord foundWord = LookForWord(word, i);
                    if (foundWord != null)
                        foundWords.Add(foundWord);
                }

            }

            //remove any duplicates

            return foundWords;
        }
        
        #endregion
    }
}