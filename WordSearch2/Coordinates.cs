using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    public class FoundWordCoordinates
    {
        Point A {get; private set;}
        Point B { get; private set; }

        public FoundWordCoordinates(Point a, Point b)
        {
            A = a;
            B = b;
        }
    }
}
