using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottLogic.NumbersGame.Bags
{
    class CountdownNumberBags : INumberBags
    {
        private readonly INumberBag[] _bags = {new CountdownBigNumberBag(), new CountdownSmallNumberBag()};

        public int Bags
        {
            get { return _bags.Length; }
        }

        public INumberBag Bag(int bag)
        {
            return _bags[bag];
        }
    }

}
