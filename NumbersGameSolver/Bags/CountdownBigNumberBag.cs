using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottLogic.NumbersGame.Bags
{
    public class CountdownBigNumberBag : NumberBag
    {
        public CountdownBigNumberBag()
            : base(new [] {25, 50, 75, 100})
        {
        }

        public override string Description
        {
            get { return "Big numbers (25, 50, 75 and 100)"; }
        }
    }
}
