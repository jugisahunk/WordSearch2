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
        public FoundWordList FoundWords { get; set; }
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

        #endregion

        #region Methods

        #region Look Helpers

        internal bool LookToRight(Word word, int charIndex)
        {
            char[] possibleWord = new char[word.Length];

            for (int i = 0; i < possibleWord.Length; i++)
                possibleWord[i] = Characters[charIndex + i];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }

        internal bool LookToLeft(Word word, int charIndex)
        {
            char[] possibleWord = new char[word.Length];

            for (int i = 0; i < possibleWord.Length; i++)
                possibleWord[i] = Characters[charIndex - i];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }

        internal bool LookUp(Word word, int charIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = charIndex / ColumnCount;

            for (int i = 0; i < word.Length; i++)
                possibleWord[i] = Characters[charIndex - ColumnCount * i];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }

        internal bool LookDown(Word word, int charIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = charIndex / ColumnCount;

            for (int i = 0; i < word.Length; i++)
                possibleWord[i] = Characters[charIndex + ColumnCount * i];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }

        internal bool LookUpAndRight(Word word, int charIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = charIndex / ColumnCount;

            possibleWord[0] = Characters[charIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[charIndex - ColumnCount * i + 1];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }

        internal bool LookUpAndLeft(Word word, int charIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = charIndex / ColumnCount;

            possibleWord[0] = Characters[charIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[charIndex - ColumnCount * i - 1];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }

        internal bool LookDownAndRight(Word word, int charIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = charIndex / ColumnCount;

            possibleWord[0] = Characters[charIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[charIndex + ColumnCount * i + 1];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }

        internal bool LookDownAndLeft(Word word, int charIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = charIndex / ColumnCount;

            possibleWord[0] = Characters[charIndex];
            for (int i = 1; i < word.Length; i++)
                possibleWord[i] = Characters[charIndex + ColumnCount * i - 1];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }
        #endregion

        #region Look

        internal FoundWord LookHorizontally(Word word, int charIndex)
        {
            // ColumnCount 10, charIndex 5, char to left 5, char to right 4
            int
                charactersToLeft = charIndex % ColumnCount,
                charactersToRight = ColumnCount - charactersToLeft - 1;

            if (charactersToRight >= word.Length && LookToRight(word, charIndex))
                return new LRFoundWord(word.Text, charIndex, this);

            if (charactersToLeft >= word.Length && LookToLeft(word, charIndex))
                return new RLFoundWord(word.Text, charIndex, this);

            return null;
        }

        internal FoundWord LookVertically(Word word, int charIndex)
        {
            //ColumnCount 20, RowCount 10, charIndex 15, rowIndex 0, charactersAbove 0, charactersBelow 9
            int
                rowIndex = charIndex / ColumnCount,
                charactersAbove = rowIndex,
                charactersBelow = RowCount - rowIndex - 1;

            if (charactersAbove >= word.Length && LookUp(word, charIndex))
                return new BTFoundWord(word.Text, charIndex, this);

            if (charactersBelow >= word.Length && LookDown(word, charIndex))
                return new TBFoundWord(word.Text, charIndex, this);

            return null;
        }

        internal FoundWord LookDiagonally(Word word, int charIndex)
        {
            //ColumnCount 20, RowCount 10, charIndex 15, rowIndex 0, charactersAbove 0, charactersBelow 9
            int
                rowIndex = charIndex / ColumnCount,
                charactersAbove = rowIndex,
                charactersBelow = RowCount - rowIndex - 1,
                charactersToLeft = charIndex % ColumnCount,
                charactersToRight = ColumnCount - charactersToLeft - 1;

            if (charactersAbove >= word.Length)
            {
                if (charactersToRight >= word.Length && LookUpAndRight(word, charIndex))
                    return new BTLRFoundWord(word.Text, charIndex, this);
                if (charactersToLeft >= word.Length && LookUpAndLeft(word, charIndex))
                    return new BTRLFoundWord(word.Text, charIndex, this);
            }

            if (charactersBelow <= word.Length)
            {
                if (charactersToRight >= word.Length && LookDownAndRight(word, charIndex))
                    return new TBLRFoundWord(word.Text, charIndex, this);
                if (charactersToLeft >= word.Length && LookDownAndLeft(word, charIndex))
                    return new TBRLFoundWord(word.Text, charIndex, this);
            }

            return null;
        }

        internal FoundWord FindWord(Word word, int currentIndex)
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
        #endregion

        public FoundWordList FindWords(List<Word> words)
        {
            FoundWordList foundWords = new FoundWordList();

            for (int i = 0; i < Characters.Length; i++)
            {
                char currentCharacter = Characters[i];

                IEnumerable<Word> startsWithCurrent = words.Where(x => x.FirstCharacter == currentCharacter);

                foreach (Word word in startsWithCurrent)
                {
                    FoundWord foundWord = FindWord(word, i);
                    if (foundWord != null)
                        foundWords.Add(foundWord); //TODO creat FoundWordCollection and overload the add to keep out found words that intersect existing found words.
                }

            }            

            return foundWords;
        }

        #endregion
    }
}