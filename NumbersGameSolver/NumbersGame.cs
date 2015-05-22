using System;
using System.Collections.Generic;
using System.Linq;

namespace NumbersGameSolver
{
    /// <summary>
    /// NumbersGame represents one particular state of a game, including its historic context (i.e. "the working out")
    /// </summary>
    class NumbersGame
    {
        private readonly List<int> _numbers = new List<int>();
        private readonly List<IOperation> _history = new List<IOperation>();
        
        // Design decision: Cache the Operation enum values once, rather than repeatedly needing to use typeof and GetValues
        // during our time-critical number crunching.
        private static readonly Operator[] OperationValues = (Operator[])Enum.GetValues(typeof (Operator));
        

        // Construct from any enumerable collection of ints
        public NumbersGame(IEnumerable<int> initVals)
        {
            foreach (var v in initVals)
            {
                _numbers.Add(v);
            }
        }
        // Copy constructor (deep)
        public NumbersGame(NumbersGame game)
        {
            _numbers.AddRange(game._numbers);
            _history.AddRange(game._history); 
        }

        public IEnumerable<IOperation> History { get { return _history; } }

        public int Length
        {
            get { return _numbers.Count; }
        }
        public bool HasDerivatives
        {
            get { return Length > 1; }
        }

        public bool Contains(int value)
        {
            return _numbers.Contains<int>(value);
        }

        public IEnumerable<NumbersGame> GenerateIntermediates()
        {
            var ret = new List<NumbersGame>();
            int maxIndex = _numbers.Count-1;
            // Take each possible pairing in turn, and apply all possible operations
            for (int ix1 = 0; ix1 < maxIndex; ++ix1)
            {
                int i1 = _numbers[ix1];
                for (int ix2 = ix1 + 1; ix2 <= maxIndex; ++ix2)
                {
                    int i2 = _numbers[ix2];
                    foreach (Operator op in OperationValues)
                    {
                        int? v = Operation.Apply(i1, i2, op);
                        if (v != null)
                        {
                            var game = new NumbersGame(this); // Deep copy
                            game._numbers.RemoveAt(ix2);// Remove the latter ix2 element
                            game._numbers[ix1] = v.Value; // Overwrite the earlier ix1 element with the result
                            // WOW. If we maintain the history using class implementation, we quadruple the processing time(!)
                            game._history.Add(new Operation(i1, i2, op));

                            ret.Add(game);
                        }
                    }
                }
            }
            return ret;
        }

        public NumbersGame Apply(int i1, int i2, Operator op)
        {
            int maxIndex = Length - 1;
            if (i1 <0 || i1 >= maxIndex-1 )
                throw new ArgumentOutOfRangeException("i1", i1, "First index value must be >= 0, and < max_index");
            if( i2<=i1 || i2 > maxIndex)
                throw new ArgumentOutOfRangeException("i2", i2, "Second index value must be > first, and <= max_index");

            var wl = new NumbersGame(this);
            

            return wl;
        }
      
    }
}

