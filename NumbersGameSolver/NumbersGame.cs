using System;
using System.Collections.Generic;
using System.Linq;

namespace NumbersGameSdk
{
    /// <summary>
    /// NumbersGame represents one particular state of a game, including its historic context (i.e. "the working out")
    /// </summary>
    public class NumbersGame
    {
        private readonly List<int> _numbers = new List<int>();
        private readonly List<IOperation> _history = new List<IOperation>();
        
        // Cache the Operation enum values once, rather than repeatedly needing to use typeof and GetValues
        // during our time-critical number crunching.
        private static readonly Operator[] OperationValues = (Operator[])Enum.GetValues(typeof (Operator));
        

        // Construct from any enumerable collection of ints, with optional target
        public NumbersGame(IEnumerable<int> initVals, int target=0)
        {
            foreach (var v in initVals)
            {
                _numbers.Add(v);
            }
            Target = target;
        }
        // Copy constructor (deep)
        public NumbersGame(NumbersGame game)
        {
            _numbers.AddRange(game._numbers);
            _history.AddRange(game._history);
            Target = game.Target;
        }

        public IEnumerable<IOperation> History { get { return _history; } }

        public int NumberCount
        {
            get { return _numbers.Count; }
        }
        public bool HasDerivatives
        {
            get { return NumberCount > 1; }
        }

        public bool IsSolved
        {
            get { return _numbers.Contains<int>(Target); }
        }

        public int Target { get; set; }

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
                            game._history.Add(new Operation(i1, i2, op));

                            ret.Add(game);
                        }
                    }
                }
            }
            return ret;
        }

        public bool TryOperation(int idx1, int idx2, Operator op, out int lhs, out int rhs)
        {
            int result = 0;
            lhs = _numbers[idx1];
            rhs = _numbers[idx2];
            if (lhs == 0 || rhs == 0) return false;
            int n1 = Math.Max(lhs, rhs);
            int n2 = Math.Min(lhs, rhs);
            switch (op)
            {
                case Operator.Addition:
                    result = n1 + n2;
                    break;

                case Operator.Subtraction:
                    if (lhs == rhs) return false;
                    result = n1 - n2;
                    break;

                case Operator.Multiplication:
                    if (n2 == 1) return false;
                    result = n1*n2;
                    break;

                case Operator.Division:
                    if (n2 == 1) return false;
                    if (n1%n2 != 0) return false;
                    result = n1/n2;
                    break;

                default:
                    return false;
            }
            _numbers[idx1] = result;
            _numbers.RemoveAt(idx2);
            AddHistory(lhs, rhs, op);
            return true;
        }

        public void UndoOperation(int idx1, int idx2, int lhs, int rhs)
        {
            _history.RemoveAt(_history.Count - 1); // Remove last element of history
            _numbers[idx1] = lhs;                   
            _numbers.Insert(idx2, rhs);
        }

        private void AddHistory(int lhs, int rhs, Operator op)
        {
            _history.Add(new Operation(lhs, rhs, op));
        }
    }
}

