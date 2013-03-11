using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public abstract class FoundWord : Word, IEquatable<FoundWord>
    {
        #region .ctor
        public FoundWord(string text, CharacterGrid characterGrid) : base(text)
        {
            Grid = characterGrid;
        }
        #endregion

        #region Abstract Members
        
        protected static GetPoint GetPointHandler;

        public abstract FoundWordOrientation Orientation { get; }

        #endregion

        #region IEquatable Members

        public bool Equals(FoundWord other)
        {
            if (other == null)
                return false;

            return Text == other.Text && Coordinates == other.Coordinates;
        }

        #endregion

        #region Fields and Properties

        public FoundWordCoordinates Coordinates { get; protected set; }
        
        protected delegate Point GetPoint(FoundWord foundWord, int index);
        
        private string _name;
        private string Name{
        	get{
        		if(!String.IsNullOrEmpty(_name))
        			_name = String.Format("\"{0}\": {1}",this.Text, Coordinates.ToString());
        			
        		return _name;
        	}
        }

        internal CharacterGrid Grid { get; private set; }
        
        #endregion

        #region Methods

        public string ToString(){
        	return Name;
        }        

		public override bool Equals(Object obj)
		{
		  if (obj == null) 
			 return false;

		  FoundWord FoundWordObj = obj as FoundWord;
		  if (FoundWordObj == null)
			 return false;
		  else    
			 return Equals(FoundWordObj);   
		}   

		public override int GetHashCode()
		{		   
		  return Name.GetHashCode();
		}

		public static bool operator == (FoundWord FoundWord1, FoundWord FoundWord2)
		{
		  if ((object)FoundWord1 == null || ((object)FoundWord2) == null)
			 return Object.Equals(FoundWord1, FoundWord2);

		  return FoundWord1.Equals(FoundWord2);
		}

		public static bool operator != (FoundWord FoundWord1, FoundWord FoundWord2)
		{
		  if (FoundWord1 == null || FoundWord2 == null)
			 return ! Object.Equals(FoundWord1, FoundWord2);

		  return !(FoundWord1.Equals(FoundWord2));
        }

        #endregion

        #region Static Methods
        public static bool MultiCharIntersect(FoundWord foundWordA, FoundWord foundWordB)
        {
            if (foundWordA.Orientation != foundWordB.Orientation)
                return false;

            List<Point> 
                listA = new List<Point>(GetPoints(foundWordA)),
                listB = new List<Point>(GetPoints(foundWordB));

            int duplicates = 0;
            for (int i = 0; i < listA.Count; i++)
            {
                if (listB.Contains(listA[1]))
                {
                    duplicates++;
                    if (duplicates == 2)
                        return true;
                }
            }

            return false;
        }

        private static Point[] GetPoints(FoundWord foundWord)
        {
            Point[] points = new Point[foundWord.Length];
            for (int i = 0; i < foundWord.Length; i++)
                points[i] = GetPointHandler(foundWord, i);

            return points;
        }
        #endregion
    }
}