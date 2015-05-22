using System;
using NumbersGameSolver;

namespace NumbersGameConsole
{
    internal class Program
    {
        private const int NumberOfNumbers = 6;

        private static void Main(string[] args)
        {
            Console.WriteLine("Countdown Numbers Game solver\n");
            while (true)
            {
                Console.WriteLine( "Please enter the six starting numbers.\n" +
                    "Only the values 1 through 10, 25, 75 and 100 are valid. Enter 'Q' to quit.");
                int[] numbers;
                if (!GetNumbersFromConsole(out numbers))
                    return;
                var rand = new Random();
                int target = rand.Next(101, 999);
                Console.WriteLine("And the target is ... {0}", target);
                var solver = new SimpleBruteForceSolver();
                ISolution solution;
                if (solver.GetFirstSolution(numbers, target, out solution))
                {
                    Console.WriteLine("Hurray. Wanna see the solution? Here it is:\n{0}", solution.GetMultilineDisplayString());
                }
                else
                {
                    Console.WriteLine("Can't be solved for tgt={0}", target);
                }
            }
        }

        private static bool GetNumbersFromConsole(out int[] numbers)
        {
            numbers = new int[NumberOfNumbers];
            for (int n = 0; n < NumberOfNumbers; n++)
            {
                Console.Write("Number #{0}? :", n + 1);
                var input = Console.ReadLine();
                if (input != null)
                {
                    int number;
                    if (Int32.TryParse(input, out number))
                    {
                        numbers[n] = number;
                        continue;
                    }

                    if (input.ToUpper() == "Q") return false;

                    Console.WriteLine("That's not a valid number, try again.");
                }
                // Reverse last increment to ask for same number again
                n--;
            }
            return true;
        }

    }
}

