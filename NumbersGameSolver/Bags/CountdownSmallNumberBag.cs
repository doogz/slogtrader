using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScottLogic.NumbersGame.Bags
{
    class CountdownSmallNumberBag : NumberBag
    {
        public CountdownSmallNumberBag()
            : base(new[] {1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7, 8, 8, 9, 9, 10, 10})
        {
            
        }

        public override string Description
        {
            get { return "Small numbers (1 through 10)"; }
        }
    }
}
