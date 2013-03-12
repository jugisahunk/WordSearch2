using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch2
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamWriter outfile = new StreamWriter(args[1]);
            Stopwatch stopWatch = new Stopwatch();

            stopWatch.Start();

            Puzzle puzzle = new Puzzle();
            puzzle.Process(args[0], outfile, args[2]);

            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("Time elapsed in Milliseconds: {0}", ts.TotalMilliseconds);
            outfile.Write(elapsedTime);
            outfile.Flush();
            Console.WriteLine("RuntTime " + elapsedTime);
            Console.ReadLine();
        }
    }
}
