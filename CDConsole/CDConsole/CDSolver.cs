using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDConsole
{
    public class CDSolver
    {
        private List<WorkingList> wip = new List<WorkingList>(60); // work in progress

        public CDSolver(int[] initialValues)
        {
            if (initialValues == null)
                throw new ArgumentNullException("Null array passed to CDSolver ctor");

            if ( initialValues.Length < 2 )
            {
                throw new ArgumentOutOfRangeException("There must be at least two values");
            }
            var iwl = new WorkingList(initialValues);
            wip.Add(iwl);
           
        }


        internal bool Solve(int tgt)
        {
            var t0 = System.DateTime.Now;

            while (wip.Any())
            {
                //Console.WriteLine("Attempting to solve using {0} working lists of length {1}",
                    //wip.Count, wip[0].Length);
                // Process exactly the lists we had from the last iteration first
                int lists = wip.Count;
                for (int idx = 0; idx < lists; ++idx)
                {
                    WorkingList w = wip[idx];
                    if (w.Contains(tgt))
                    {
                        w.DumpHistory();
                        return true;
                    }
                    else if ( w.HasDerivatives )
                    {
                        var derivatives = w.GenerateIntermediates();
                        foreach (var v in derivatives)
                        {
                            wip.Add(v);
                        }
                    }
                }
                // We haven't found any. We need to erase (0,lists] now
                // RemoveRange takes a count though
                wip.RemoveRange(0, lists );
            }
            var t1 = System.DateTime.Now;
            var dt = t1 - t0;
            //Console.WriteLine("Exhausting the possibilities took {0}.{1} seconds", dt.Seconds, dt.Milliseconds);
            return false;
        }
    }
}
