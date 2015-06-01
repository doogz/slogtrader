using System;
using System.Collections.Generic;

namespace ScottLogic.NumbersGame
{
    
    public static class GameGenerator
    {

        public static bool GenerateCountdownGame(int bigNumbers, out int[] initialNumbers , out int target)
        {
            target = new Random().Next(101,1000);
            initialNumbers = new int[6];

            if (bigNumbers < 0 || bigNumbers > 4)
                return false;

            var bags = new Bags.CountdownNumberBags();

            for (int n = 0; n < bigNumbers; ++n)
                initialNumbers[n] = bags.Bag(0).TakeNumber();
            for (int n = bigNumbers; n < 6; ++n)
                initialNumbers[n] = bags.Bag(1).TakeNumber();

            return true;
        }

    }
}
