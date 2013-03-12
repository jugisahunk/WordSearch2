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
            FoundWords = new FoundWordList();
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
                if (char.ToUpper(A[i]) != char.ToUpper(B[i]))
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

            for (int i = 0; i < word.Length; i++)
                possibleWord[i] = Characters[charIndex - ColumnCount * i + i];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }

        internal bool LookUpAndLeft(Word word, int charIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = charIndex / ColumnCount;

            for (int i = 0; i < word.Length; i++)
                possibleWord[i] = Characters[charIndex - ColumnCount * i - i];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }

        internal bool LookDownAndRight(Word word, int charIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = charIndex / ColumnCount;

            for (int i = 0; i < word.Length; i++)
                possibleWord[i] = Characters[charIndex + ColumnCount * i + i];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }

        internal bool LookDownAndLeft(Word word, int charIndex)
        {
            char[] possibleWord = new char[word.Length];
            int startingRowIndex = charIndex / ColumnCount;

            for (int i = 0; i < word.Length; i++)
                possibleWord[i] = Characters[charIndex + ColumnCount * i - i];

            return ArrayEquals(possibleWord, word.Text.ToCharArray());
        }
        #endregion

        #region Look

        internal void LookHorizontally(Word word, int charIndex)
        {
            // ColumnCount 10, charIndex 5, char to left 5, char to right 4
            int
                charactersToLeft = charIndex % ColumnCount,
                charactersToRight = ColumnCount - charactersToLeft - 1;
            
            if (charactersToRight >= word.LengthMinusOne && LookToRight(word, charIndex))
                if(FoundWords.Add(new LRFoundWord(word.Text, charIndex, this)))
                    return;

            if (charactersToLeft >= word.LengthMinusOne && LookToLeft(word, charIndex))
                if(FoundWords.Add(new RLFoundWord(word.Text, charIndex, this)))
                    return;
        }

        internal void LookVertically(Word word, int charIndex)
        {
            //ColumnCount 20, RowCount 10, charIndex 15, rowIndex 0, charactersAbove 0, charactersBelow 9
            int
                rowIndex = charIndex / ColumnCount,
                charactersAbove = rowIndex,
                charactersBelow = RowCount - rowIndex - 1;

            if (charactersAbove >= word.LengthMinusOne && LookUp(word, charIndex))
                if (FoundWords.Add(new BTFoundWord(word.Text, charIndex, this)))
                    return;

            if (charactersBelow >= word.LengthMinusOne && LookDown(word, charIndex))
                if(FoundWords.Add(new TBFoundWord(word.Text, charIndex, this)))
                    return;
        }

        internal void LookDiagonally(Word word, int charIndex)
        {
            //ColumnCount 20, RowCount 10, charIndex 15, rowIndex 0, charactersAbove 0, charactersBelow 9
            int
                rowIndex = charIndex / ColumnCount,
                charactersAbove = rowIndex,
                charactersBelow = RowCount - rowIndex - 1,
                charactersToLeft = charIndex % ColumnCount,
                charactersToRight = ColumnCount - charactersToLeft - 1;

            if (charactersAbove >= word.LengthMinusOne)
            {
                if (charactersToRight >= word.LengthMinusOne && LookUpAndRight(word, charIndex))
                    if(FoundWords.Add(new BTLRFoundWord(word.Text, charIndex, this)))
                        return;
                if (charactersToLeft >= word.LengthMinusOne && LookUpAndLeft(word, charIndex))
                    if(FoundWords.Add(new BTRLFoundWord(word.Text, charIndex, this)))
                        return;
            }

            if (charactersBelow >= word.LengthMinusOne)
            {
                if (charactersToRight >= word.LengthMinusOne && LookDownAndRight(word, charIndex))
                    if(FoundWords.Add(new TBLRFoundWord(word.Text, charIndex, this)))
                        return;
                if (charactersToLeft >= word.LengthMinusOne && LookDownAndLeft(word, charIndex))
                    if(FoundWords.Add(new TBRLFoundWord(word.Text, charIndex, this)))
                        return;
            }
        }

        internal void FindWord(Word word, int currentIndex)
        {
            LookHorizontally(word, currentIndex);
            LookVertically(word, currentIndex);
            LookDiagonally(word, currentIndex);
        }
        #endregion

        public void FindWords(IEnumerable<Word> words)
        {
            if (words.Count() == 0)
                return;
            
            foreach (Word currentWord in words)
                for (int i = 0; i < Characters.Length; i++)
                    FindWord(currentWord, i);
        }

        #endregion
    }

}