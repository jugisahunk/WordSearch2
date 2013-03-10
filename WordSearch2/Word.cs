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

            Text = text;
        }

        public int Length { get { return Text.Length; } }

        public string Text { get; private set; }

        private string _lowerText;
        public string LowerText {
        	get{
        		if(!String.IsNullOrEmpty(_lowerText))
        			_lowerText = Text.ToLower();
        			
        		return _lowerText;
        	}
        }

        private char _firstCharacter;
        public char FirstCharacter {
            get 
            {
                if (_firstCharacter == '\0')
                    _firstCharacter = Text.Substring(0, 1)[0];

                return _firstCharacter;
            }
        }        
    }
}
