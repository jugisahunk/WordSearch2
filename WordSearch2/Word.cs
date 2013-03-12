using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class Word
    {

        public Word(string text)
        {
            if (String.IsNullOrEmpty(text))
                throw new ArgumentException("Text cannot be null");

            OriginalText = text;
            Text = text.ToLower().Replace(" ", String.Empty);
        }

        public int Length { get { return Text.Length; } }

        public string OriginalText { get; private set; }
        public string Text { get; private set; }

        private char _firstCharacter;
        public char FirstCharacter {
            get 
            {
                if (_firstCharacter == '\0')
                    _firstCharacter = Text.Substring(0, 1)[0];

                return _firstCharacter;
            }
        }

        private int? _lengthMinusOne;
        public int LengthMinusOne
        {
            get
            {
                if (!_lengthMinusOne.HasValue)
                    _lengthMinusOne = Length - 1;

                return _lengthMinusOne.Value;
            }
        }
    }

    public class DescendingComparer : IComparer<Word>
    {
        public int Compare(Word a, Word b)
        {
            return string.Compare(a.Text, b.Text) * -1;
        }
    }
}
