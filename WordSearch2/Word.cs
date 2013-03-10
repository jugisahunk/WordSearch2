using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class Word : IEquatable<Word>
    {
        public int Length { get { return Text.Length; } }

        public string Text { get; private set; }
        
        private string? _lowerText 
        public string LowerText {
        	get{
        		if(!_lowerText.HasValue)
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

        private string? _name;
        private string Name{
        	get{
        		if(!_name.HasValue)
        			_name = Text;
        			
        		return _name;
        	}
        }        

        public Word(string text)
        {
            if(String.IsNullOrEmpty(text))
                throw new ArgumentException("Text cannot be null");

            Text = text;
        }
        
        public string ToString(){
        	return Name;
        }        
        
		public bool Equals(FoundWord other) 
		{
			if (other == null) 
				return false;
				
		  return String.CompareOrdinal(LowerText, other.LowerText) == 0;
		}

		public override bool Equals(Object obj)
		{
		  if (obj == null) 
			 return false;

		  Word WordObj = obj as Word;
		  if (WordObj == null)
			 return false;
		  else    
			 return Equals(WordObj);   
		}   

		public override int GetHashCode()
		{		   
		  return Name.GetHashCode();
		}

		public static bool operator == (Word Word1, Word Word2)
		{
		  if ((object)Word1 == null || ((object)Word2) == null)
			 return Object.Equals(Word1, Word2);

		  return Word1.Equals(Word2);
		}

		public static bool operator != (Word Word1, Word Word2)
		{
		  if (Word1 == null || Word2 == null)
			 return ! Object.Equals(Word1, Word2);

		  return !(Word1.Equals(Word2));
		}        
    }
}
