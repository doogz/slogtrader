using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDConsole
{
    // WorkingList represents a collection of initial and/or intermediate values
    // using WLType = System.Int32;

    public class WorkingList
    {
        private string history = "";
        private List<int> list = new List<int>();

        public enum Operation
        {
            [Description(" + ")]
            Addition=0,
            [Description(" - ")]
            Subtraction,
            [Description(" X ")]
            Multiplication,
            [Description(" \x00F7 ")]
            Division,
            EndOfOperations
        }
        public WorkingList(IEnumerable<int> initVals)
        {
            foreach (var v in initVals)
            {
                list.Add(v);
            }
        }
        // The following copy constructor does a deep copy
        public WorkingList(WorkingList wl)
        {
            foreach (var v in wl.list)
            {
                list.Add(v);
            }
            history = wl.history; 
        }


        public int Length
        {
            get
            {
                return list.Count;
            }
        }
        public bool HasDerivatives
        {
            get { return Length > 1; }
        }
        /*
        public List<int> UniqueValues
        {
            get 
            {
                List<int> ret = new List<WLValueType>();
                foreach (WLValueType v in list)
                {
                    if (!ret.Contains(v))
                        ret.Add(v);
                }

                return ret;
            
            }
        }
         * */
        public bool Contains(int value)
        {
            return list.Contains<int>(value);
        }
        public void DumpHistory()
        {
            Console.WriteLine(history);
        }

        public IEnumerable<WorkingList> GenerateIntermediates()
        {
            var ret = new List< WorkingList >();
            int maxIndex = list.Count-1;
            // Take each possible pairing in turn, and apply all possible operations
            // i1 indicates the index of the first item, i2 the second
            for (int ix1 = 0; ix1 < maxIndex; ++ix1)
            {
                int i1 = list[ix1]; // intermediates[ix1];
                for (int ix2 = ix1 + 1; ix2 <= maxIndex; ++ix2)
                {
                    int i2 = list[ix2];
                    for (Operation op = Operation.Addition; op < Operation.EndOfOperations; op++)
                    {
                        int? v = ApplyOperation(i1, i2, op);
                        if (v != null)
                        {
                            var intermediates = new List<int>(list); // Takes a copy of current
                            intermediates.RemoveAt(ix2);
                            intermediates[ix1] = v.Value;
                            var w = new WorkingList(intermediates);
                            w.history = history + op.ToString() + "(" + i1 + "," + i2 + ")\r\n";
                            ret.Add(w);
                        }
                    }
                }
            }
            return ret;
        }

        public WorkingList Apply(int i1, int i2, Operation op)
        {
            int maxIndex = Length - 1;
            if (i1 <0 || i1 >= maxIndex-1 )
                throw new ArgumentOutOfRangeException("i1", i1, "First index value must be >= 0, and < max_index");
            if( i2<=i1 || i2 > maxIndex)
                throw new ArgumentOutOfRangeException("i2", i2, "Second index value must be > first, and <= max_index");

            var wl = new WorkingList(this);
            

            return wl;
        }

        public static int? ApplyOperation(int n1, int n2, Operation op)
        {
            switch (op)
            {
                case Operation.Addition:
                    return n1 + n2;

                case Operation.Subtraction:
                    return ( n1>=n2 ? n1-n2 : n2-n1);

                case Operation.Multiplication:
                    return n1 * n2;
                
                case Operation.Division:
                    int numerator = Math.Max(n1, n2);
                    int denominator = Math.Min(n1, n2);
                    if ( denominator == 0 || (numerator%denominator) != 0 )
                    {
                        return null;
                    }
                    return numerator/denominator;

                default:
                    return null;
            }
                    
        }
    }
}
