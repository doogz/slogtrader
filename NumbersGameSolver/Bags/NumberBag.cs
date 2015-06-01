using System;
using System.Collections.Generic;
using System.Linq;

namespace ScottLogic.NumbersGame.Bags
{
    /// <summary>
    /// Abstract base class for number bags.
    /// </summary>
    public abstract class NumberBag : INumberBag
    {
        private readonly List<int> _numbers = new List<int>();

        protected NumberBag(IEnumerable<int> initValues)
        {
            _numbers.AddRange(initValues);
        }

        public int TakeNumber()
        {
            var rnd = new Random();
            int idx = rnd.Next(_numbers.Count);
            int val = _numbers[idx];
            _numbers.RemoveAt(idx);
            return val;
        }

        public abstract string Description { get; }

        public int Count
        {
            get { return _numbers.Count; }
        }

        public bool IsEmpty
        {
            get { return !_numbers.Any(); }
        }
    }
}