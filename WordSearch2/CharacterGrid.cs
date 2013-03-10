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

        #region Methods  
        
        private bool ConfirmWord(Word word){
        	foreach(FoundWord foundWord in FoundWords){
        		
        	}
        }
        
        private FoundWord ExtractFoundWordDiagonalBottomToTopLeftToRight(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = startIndex / ColumnCount;

            possibleWord[0] = Characters[startIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[startIndex - ColumnCount + 1];

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1 + word.Length,
                    y1 = startingRowIndex,
                    y2 = y1 - word.Length;

                return new FoundWord(word, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
            }

            return null;
        }
        
        private FoundWord ExtractFoundWordDiagonalTopToBottomRightToLeft(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = startIndex / ColumnCount;

            possibleWord[0] = Characters[startIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[startIndex + ColumnCount - 1];

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1 - word.Length,
                    y1 = startingRowIndex,
                    y2 = y1 + word.Length;

                return new FoundWord(word, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
            }

            return null;
        }

        private FoundWord ExtractFoundWordDiagonalBottomToTopRightToLeft(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = startIndex / ColumnCount;

            possibleWord[0] = Characters[startIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[startIndex - ColumnCount - 1];

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1 + word.Length,
                    y1 = startingRowIndex,
                    y2 = y1 + word.Length;

                return new FoundWord(word, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
            }

            return null;
        }
        
        private FoundWord ExtractFoundWordDiagonalTopToBottomLeftToRight(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = startIndex / ColumnCount;

            possibleWord[0] = Characters[startIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[startIndex + ColumnCount + 1];

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1 + word.Length,
                    y1 = startingRowIndex,
                    y2 = y1 + word.Length;

                return new FoundWord(word, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
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
                    x2 = x1 - word.Length,
                    y1 = startIndex / ColumnCount,
                    y2 = y1;
                return new FoundWord(word,new FoundWordCoordinates(new Point(x1,y1), new Point(x2, y2)));
            }

            return null;
        }

        private FoundWord ExtractFoundWordHorizontalLeftToRight(Word word, int startIndex)
        {
            char[] possibleWord = new char[word.Length];

            Characters.CopyTo(possibleWord, startIndex);

            if (ArrayEquals(possibleWord, word.Text.ToCharArray()))
            {
                int
                    x1 = startIndex % ColumnCount,
                    x2 = x1 + word.Length,
                    y1 = startIndex / ColumnCount,
                    y2 = y1;
                return new FoundWord(word, new FoundWordCoordinates(new Point(x1, y1), new Point(x2, y2)));
            }

            return null;
        }

        internal FoundWord LookHorizontally(Word word, int charIndex){
            // ColumnCount 10, charIndex 5, char to left 5, char to right 4
            int 
                charactersToLeft = ColumnCount % charIndex,
                charactersToRight = ColumnCount - charactersToLeft - 1;

			FoundWord foundWord;

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
        		
        	FoundWord foundWord;
        		
        	if(charactersAbove >= word.Length)
        	{
        		//extract word vertically from bottom to top
        	}
        	
        	if(foundWord == null && charactersBelow >= word.Length)
        	{
        		//extract word vertically from top to bottom
        	}
        	
        	return foundWord        	
        }
        
        internal FoundWord LookDiagonally(Word word, int charIndex){
        	//ColumnCount 20, RowCount 10, charIndex 15, rowIndex 0, charactersAbove 0, charactersBelow 9
        	int
        		rowIndex = charIndex / ColumnCount,
        		charactersAbove = rowIndex,
        		charactersBelow = RowCount - rowIndex -1,
                charactersToLeft = ColumnCount % charIndex,
                charactersToRight = ColumnCount - charactersToLeft - 1;
			
			
        }

        internal FoundWord FindWord(Word word)
        {   
            for (int i = 0; i < Characters.Length; i++)
            {
                char currentChar = Characters[i];
                if (currentChar == word.FirstCharacter)
                {
                    //look horizontally
                    //look vertically
                    //look diagonally
                }
            }

            return null;
        }

        public List<FoundWord> FindWords(List<Word> words)
        {
            List<FoundWord> foundWords = new List<FoundWord>();
            foreach (Word word in words)
            {
                FoundWord foundWord = FindWord(word);
                if (foundWord != null)
                    foundWords.Add(foundWord);
            }

            return foundWords;
        }
        
        #region Helper Methods
     
	private bool ArrayEquals(char[] A, char[] B){
	    if (A.Length != B.Length) return false;

	    for (int i = 0; i < A.Length; i++)
		if(A[i] != B[i])
		    return false;

	    return true;
	}        
        
        #endregion
        
        #endregion
    }
}