using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public struct Point : IEquatable<Point>
    {
        public int X, Y;

        public Point(int ex, int wai)
        {
            X = ex;
            Y = wai;

            _name = String.Format("{0}-{1}", Y, X);
        }
        
        private string _name;
        private string Name{ get{ return _name; } }
        
        public override string ToString(){
        	return Name;
        }        
        
		public bool Equals(Point other) 
		{
			if (other == null) 
				return false;
				
		  return X == other.X && Y == other.Y;
		}

		public override bool Equals(Object obj)
		{
		  if (obj == null) 
			 return false;

		  if (obj.GetType() != typeof(Point))
			 return false;
		  else    
			 return Equals((Point)obj);   
		}   

		public override int GetHashCode()
		{		   
		  return Name.GetHashCode();
		}

		public static bool operator == (Point Point1, Point Point2)
		{
		  if ((object)Point1 == null || ((object)Point2) == null)
			 return Object.Equals(Point1, Point2);

		  return Point1.Equals(Point2);
		}

		public static bool operator != (Point Point1, Point Point2)
		{
		  if (Point1 == null || Point2 == null)
			 return ! Object.Equals(Point1, Point2);

		  return !(Point1.Equals(Point2));
		}
    }
}
