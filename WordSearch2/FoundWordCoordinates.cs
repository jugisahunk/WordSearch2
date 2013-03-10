using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class FoundWordCoordinates : IEquatable<FoundWordCoordinates>
    {
        Point A {get; private set;}
        Point B { get; private set; }

        public FoundWordCoordinates(Point a, Point b)
        {
            A = a;
            B = b;
        }
        
        private string? _name;
        private string Name{
        	get{
        		if(!_name.HasValue)
        			_name = String.Format("{0}-{1}", A.ToString(), B.ToString());
        			
        		return _name;
        	}
        }
        

        
        public string ToString(){
        	return Name;
        }        
        
		public bool Equals(FoundFoundWordCoordinates other) 
		{
			if (other == null) 
				return false;
				
		  return A == other.A && B == other.B;
		}

		public override bool Equals(Object obj)
		{
		  if (obj == null) 
			 return false;

		  FoundFoundWordCoordinates FoundFoundWordCoordinatesObj = obj as FoundFoundWordCoordinates;
		  if (FoundFoundWordCoordinatesObj == null)
			 return false;
		  else    
			 return Equals(FoundFoundWordCoordinatesObj);   
		}   

		public override int GetHashCode()
		{		   
		  return Name.GetHashCode();
		}

		public static bool operator == (FoundWordCoordinates FoundWordCoordinates1, FoundWordCoordinates FoundWordCoordinates2)
		{
		  if ((object)FoundWordCoordinates1 == null || ((object)FoundWordCoordinates2) == null)
			 return Object.Equals(FoundWordCoordinates1, FoundWordCoordinates2);

		  return FoundWordCoordinates1.Equals(FoundWordCoordinates2);
		}

		public static bool operator != (FoundWordCoordinates FoundWordCoordinates1, FoundWordCoordinates FoundWordCoordinates2)
		{
		  if (FoundWordCoordinates1 == null || FoundWordCoordinates2 == null)
			 return ! Object.Equals(FoundWordCoordinates1, FoundWordCoordinates2);

		  return !(FoundWordCoordinates1.Equals(FoundWordCoordinates2));
		}         
    }
}
