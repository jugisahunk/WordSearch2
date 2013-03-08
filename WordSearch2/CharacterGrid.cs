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
        #endregion

        #region Methods       

        private bool ArrayEquals(char[] A, char[] B){
            if (A.Length != B.Length) return false;

            for (int i = 0; i < A.Length; i++)
                if(A[i] != B[i])
                    return false;

            return true;
        }

        internal FoundWord LookHorizontally(Word word, int charIndex){
            int 
                charactersToLeft = ColumnCount % charIndex,
                charactersToRight = ColumnCount - charactersToLeft - 1;

            if (charactersToRight <= word.Length)
            {
                char[] possibleWord = new char[word.Length];
                Characters.CopyTo(possibleWord,charIndex);

                if(ArrayEquals(possibleWord, word.Text.ToCharArray()))
                {
                    int 
                        x1 = charIndex % ColumnCount, 
                        x2 = x1 + word.Length,
                        y1, 
                        y2;
                    return new FoundWord(word,new Point(x1,y1), new Point(x2, y2));
                }
            }


            return null;
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
        #endregion
    }
}