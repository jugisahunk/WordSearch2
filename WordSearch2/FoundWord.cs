using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class FoundWord : IEquatable<FoundWord>
    {
        public FoundWord(Word word, FoundWordCoordinates coordinates)
        {
            Word = word;
            Coordinates = coordinates;
            
            if(Coordinates.A.Y == Coordinates.B.Y && Coordinates.A.X == Coordinates.B.X)
            	Orientation = FoundWordOrientation.Point;
            else if(Coordinates.A.Y == Coordinates.B.Y)
            	Orientation = FoundWordOrientation.Horizontal;
            else if(Coordinates.A.X == Coordinates.B.X)
            	Orientation = FoundWordOrientation.Vertical;
            else
            	Orientation = FoundWordOrientation.Diagonal;
            	
            if(Coordinates.A.Y < Coordinates.B.Y)
            	VerticalDirection = FoundWordVerticalDirection.TopToBottom;
            else(Coordinates.A.Y > Coordinates.B.Y)
            	VerticalDirection = FoundWordVerticalDirection.BottomToTop;
            else
            	VerticalDirection = FoundWordVerticalDirection.None;
            	
            if(Coordinates.A.X < Coordinates.B.X)
            	HorizontalDirection = FoundWordHorizontalDirection.LeftToRight;
            else if(Coordinates.A.X > Coordinates.B.X)
            	HorizontalDirection = FoundWordHorizontalDirection.RightToLeft;
            else
            	HorizontalDirection = FoundWordHorizontalDirection.None;
            	
            if(Orientation == FoundWordOrientation.Diagonal)
            	switch(HorizontalDirection){
            		case FoundWordHorizontalDirection.LeftToRight):
            			break;
            		case FoundWordHorizontalDirection.RightToLeft):
            			break;
            		case FoundWordHorizontalDirection.None):
            			break;
            		default:
            			throw new ArgumentOutOfRangeException();
            	}
            }
            else
            	DiagonalDirection = FoundWordDiagonalDirection.None;
            	
        }
        
        public Enum FoundWordOrientation{
        	Horizontal,
        	Vertical,
        	Diagonal,
        	Point
        }
        
        public Enum FoundWordVerticalDirection{
        	TopToBottom,
        	BottomToTop,
        	None
        }
        
        public Enum FoundWordHorizontalDirection{
        	LeftToRight,
        	RightToLeft,
        	None
        }
        
        public Enum FoundWordDiagonalDirection{
        	TopToBottomLeftToRight,
        	TopToBottomRightToLeft,
        	BottomToTopLeftToRight,
        	BottomToTopRightToLeft,
        	None
        }

        public Word Word { get; set; }
        public FoundWordCoordinates Coordinates { get; private set; }
        public FoundWordOrientation Orientation { get; private set;}
        public FoundWordVerticalDirection VerticalDirection { get; private set;}
        public FoundWordHorizontalDirection HorizontalDirection { get; private set;}
        public FoundWordDiagonalDirection DiagonalDirection { get; private set; }
        
        private delegate Point GetPoint(FoundWord foundWord, int index);
        
        private string? _name;
        private string Name{
        	get{
        		if(!_name.HasValue)
        			_name = String.Format("{0}: {1}-{2}",Word.Text, Coordinates.A.ToString(), Coordinates.B.ToString());
        			
        		return _name;
        	}
        }
        
        private Point[] GetPoints(FoundWord foundWord){
        	Point[] foundWordPoints = new Point[foundWord.Word.Length];
        	
        	for(int i=0; i < foundWordPoints.Length; i++){
        		if(foundWord.IsHorizontal){
					if(foundWord.IsLeftToRight)
						//foundWordPoints[i] = new Point(foundWord.Coordinates.A.X + i, foundWord.Coordinates.A.Y);
					else
						//foundWordPoints[i] = new Point(foundWord.Coordinates.A.X - i, foundWord.Coordinates.A.Y);
				}
				else if(foundWord.IsVertical){
					if(foundWord.IsTopToBottom)
						//foundWordPoints[i] = new Point(foundWord.Coordinates.A.X, foundWord.Coordinates.A.Y + i);
					else
						//foundWordPoints[i] = new Point(foundWord.Coordinates.A.X, foundWord.Coordinates.A.Y + i);				
				}
				else if(foundWord.IsDiagonal){
					if(foundWord.IsLeftToRight && foundWord.IsTopToBottom)
						foundWordPoints[i] = new Point(foundWord.Coordinates.A.X + i, foundWord.Coordinates.A.Y + i);
					else if(foundWord.IsRightToLeft && foundWord.IsTopToBottom)        			
						foundWordPoints[i] = new Point(foundWord.Coordinates.A.X - 1, foundWord.Coordinates.A.Y + i);
					else if(foundWord.IsRightToLeft && foundWord.IsBottomToTop)
						foundWordPoints[i] = new Point(foundWord.Coordinates.A.X - 1, foundWord.Coordinates.A.Y - 1);
					else //Left to right bottom to top
						foundWordPoints[i] = new Point(foundWord.Coordinates.A.X + 1, foundWord.Coordinates.A.Y - 1)				
				}
				else				
					throw new Exception("Something is wrong; a line must be horizontal, vertical, or diagonal.");
        	}
        	
        	
        	
        	for(int i=0; i < foundWordPoints.Length; i++){
        		switch(foundWord.Orientation){
        			case FoundWordOrientation.Horizontal:
        				switch(foundWord.HorizontalDirection){
        					case FoundWordHorizontalDirection.LeftToRight:
        						foundWordPoints[i] = new Point(foundWord.Coordinates.A.X + i, foundWord.Coordinates.A.Y);
        						break;
        					case FoundWordHorizontalDirection.RightToLeft:
        						foundWordPoints[i] = new Point(foundWord.Coordinates.A.X - i, foundWord.Coordinates.A.Y);
        						break;
        					case FoundWordHorizontalDirection.None:
        						foundWordPoints[i] = foundWord.Coordinates.A;
        						break;
        				}
        				break;
        			case FoundWordOrientation.Vertical:
        				switch(foundWord.VerticalDirection){
        					case FoundWordVerticalDirection.TopToBottom:
        						foundWordPoints[i] = new Point(foundWord.Coordinates.A.X, foundWord.Coordinates.A.Y + i);
        						break;
        					case FoundWordVerticalDirection.BottomToTop:
        						foundWordPoints[i] = new Point(foundWord.Coordinates.A.X, foundWord.Coordinates.A.Y + i);				
        						break;
        					case FoundWordVerticalDirection.None:
        						foundWordPoints[i] = foundWord.Coordinates.A;
        						break;
        				}
        				break;
        			case FoundWordOrientation.Diagonal:
        				switch(foundWord.VerticalDirection){
        					case FoundWordVerticalDirection.TopToBottom:
        						foundWordPoints[i] = new Point(foundWord.Coordinates.A.X, foundWord.Coordinates.A.Y + i);
        						break;
        					case FoundWordVerticalDirection.BottomToTop:
        						foundWordPoints[i] = new Point(foundWord.Coordinates.A.X, foundWord.Coordinates.A.Y + i);				
        						break;
        					case FoundWordVerticalDirection.None:
        						foundWordPoints[i] = foundWord.Coordinates.A;
        						break;
        				}
        				break;        				
        		}
        	}
        		
        	return foundWordPoints;
        }
        
        private GetPoint GetPointHandler(FoundWord foundWord) {
        	GetPoint getPointDelegate;        	
        		switch(foundWord.Orientation){
        			case FoundWordOrientation.Horizontal:
        				switch(foundWord.HorizontalDirection){
        					case FoundWordHorizontalDirection.LeftToRight:
        						return delegate(FoundWord foundWord, int index){ return new Point(foundWord.Coordinates.A.X + index, foundWord.Coordinates.A.Y);};
        						break;
        					case FoundWordHorizontalDirection.RightToLeft:
        						return delegate(FoundWord foundWord, int index){ return new Point(foundWord.Coordinates.A.X - index, foundWord.Coordinates.A.Y);};
        						break;
        					case FoundWordHorizontalDirection.None:
        						return delegate(FoundWord foundWord, int index){ return foundWord.Coordinates.A; };
        						break;
        					default:
        						throw new ArgumentOutOfRangeException();
        				}
        				break;
        			case FoundWordOrientation.Vertical:
        				switch(foundWord.VerticalDirection){
        					case FoundWordVerticalDirection.TopToBottom:
								return delegate(FoundWord foundWord, int index){ return new Point(foundWord.Coordinates.A.X, foundWord.Coordinates.A.Y + index);};
        						break;
        					case FoundWordVerticalDirection.BottomToTop:
        						return delegate(FoundWord foundWord, int index){ return new Point(foundWord.Coordinates.A.X, foundWord.Coordinates.A.Y - index);};
        						break;
        					case FoundWordVerticalDirection.None:
        						return delegate(FoundWord foundWord, int index){ return foundWord.Coordinates.A;};
        						break;
        					default:        					
        						throw new ArgumentOutOfRangeException();
        				}
        				break;
        			case FoundWordOrientation.Diagonal:
        				
        				break;
        			default:
        				throw new ArgumentOutOfRangeException();
        		}
        	}        
        }
        
        public static bool MultiIntersect(FoundWord foundWordA, FoundWord foundWordB){
        	//Y = MX + B
        	//Y = M(X -Px) + Py
        	//M = 
        	
        	if(	!(foundWordA.IsHorizontal && foundWordB.IsHorizontal) 
        		|| !(foundWordA.IsVertical && foundWordB.IsVertical) 
        		|| !(foundWordA.IsDiagonal && foundWordB.IsDiagonal))
        		return false;
        	
        	return MultiIntersect(new List(GetPoints(this)),new List(GetPoints(foundWord)));
        }
        
        private bool MultiIntersect(List<Point> listA, List<Point> listB){
			int duplicates = 0
			for(int i=0; i < thisPoints.Length; i++){
				if(inputPoints.Contains(thisPoints[1])){
					duplicates++;
					if(duplidates == 2)
						return true;
				}
			}        
			return false;
        }
        
        public string ToString(){
        	return Name;
        }        
        
		public bool Equals(FoundWord other) 
		{
			if (other == null) 
				return false;
				
		  return Word == other.Word && Coordinates == other.Coordinates;
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
    }
}