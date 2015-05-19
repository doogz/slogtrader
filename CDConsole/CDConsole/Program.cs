using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const int numTiles = 6;
            var num = new int[] { 1, 2, 3, 4, 5, 6 };
            Random rnd = new Random();

            Console.WriteLine("CDConsole");
            /*
            for (int n=0; n<numTiles; ++n)
            {
                Console.Write( "Value for tile #{0} :", n+1);
                string s = Console.ReadLine();
                try
                {
                    num[n] = Int32.Parse(s);
                }
                catch (Exception)
                {
                    // We expect this e.g. empty string
                }
                finally 
                {
                    num[n] = 0;
                }
            }
            */

            // upto 270 is ok so far!

            for (int tgt = 7; tgt < 999; ++tgt)
            {
                //int tgt = rnd.Next(101, 1000);

                Console.WriteLine("Attempting target: {0}", tgt);
                CDSolver solver = new CDSolver(num);
                if (!solver.Solve(tgt))
                {
                    Console.WriteLine("Can't be solved for tgt={0}", tgt);
                }
            }
            Console.WriteLine("We're done");
            Console.ReadKey(true);
        }
    }
}
