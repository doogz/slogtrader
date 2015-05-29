using System;
using System.Collections.Generic;
using ScottLogic.NumbersGame;

namespace NumbersGameConsole
{
    internal class Program
    {
        private const int NumberOfNumbers = 6;
        private static readonly Random _random = new Random();

        private static void Main(string[] args)
        {
            SolverLocator sl = new SolverLocator();
            sl.FindSolvers();

            Console.WriteLine("*** Numbers Game solver ***\n");
            Console.WriteLine("Enter your numbers, one at a time, or together separated by commas.");
            Console.WriteLine("You can also specify the target after '=', or enter it separately.");
          
            while (true)
            {

                int[] numbers;
                int target;
                if (GetNumbersFromConsole(out numbers, out target))
                {
                    Console.Write("The numbers are: [ ");
                    for(int idx=0; idx<numbers.Length; ++idx)
                    {
                        Console.Write("{0}", numbers[idx]);
                        if (idx < numbers.Length-1)
                            Console.Write(", ");
                    }
                    Console.WriteLine(" ]");
                    Console.WriteLine("Target: {0}", target);
                    var solver = SolverFactory.Create(); // new DeepRecursiveUndoingSolver(); //ProgressiveRecursiveBruteForceSolver();
                    ISolution solution;
                    if (solver.GetFirstSolution(numbers, target, out solution))
                    {
                        Console.WriteLine("Solved in {0}s, as follows:\r\n{1}",
                            solution.ExecutionTime.TotalSeconds,
                            solution.GetMultilineDisplayString());
                    }
                    else
                    {
                        Console.WriteLine("Not solvable, exhausted possibilities in {0}s",
                            solution.ExecutionTime.TotalSeconds);

                    }

                }

            }
        }

        //TODO: Refactor - overly long method
        private static bool GetNumbersFromConsole(out int[] numbers, out int target)
        {
            target = 0;
            var currentNumbers = new List<int>();
            bool numbersEntered = false;
            while (!numbersEntered)
            {
                Console.Write("Number(s):");
                var input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    if (currentNumbers.Count == 0)
                    {
                        AutoGenerateNumbers(currentNumbers);
                    }
                    numbersEntered = true;
                    continue; // Individual numbers entry is terminated with a blank entry
                }

                int number;
                while (input.Contains(","))
                {
                    numbersEntered = true; // We assume they're all on the one line here
                    var commaPos = input.IndexOf(",", StringComparison.Ordinal);
                    var numText = input.Substring(0, commaPos);
                    if (Int32.TryParse(numText, out number))
                        currentNumbers.Add(number);

                    input = input.Substring(commaPos + 1);
                }
                // We may have = Target bolted on to the string
                if (input.Contains("="))
                {
                    numbersEntered = true; // We assume they're all on the one line here
                    var equalsPos = input.IndexOf("=", StringComparison.Ordinal);

                    var targetText = input.Substring(equalsPos + 1);
                    input = input.Substring(0, equalsPos);
                    Int32.TryParse(targetText, out target);
                }
                if (Int32.TryParse(input, out number))
                {
                    currentNumbers.Add(number);
                }
            }

            if (target == 0)
            {
                Console.WriteLine("Enter target, or hit enter to generate a random target:");
                var input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                    Int32.TryParse(input, out target);
                else target = _random.Next(101, 1000);
            }

            numbers = currentNumbers.ToArray();
            return true;
        }





        private static void AutoGenerateNumbers(ICollection<int> numbers, int count = 6)
        {
            if (count < 1 || count > 10)
                throw new ArgumentOutOfRangeException("Count must be between 1 and 10");

            // Uses 6 by default
            for (int n = 0; n < count; ++n)
            {
                numbers.Add(NextRandomNumber(n));
            }

        }

        private static int NextRandomNumber(int hintIndex)
        {
            // The index passed in is used as a hint regarding whether we should pick a big number or a small number
            if (hintIndex < 4)
            {
                if (_random.Next(1, 100) > 66)
                {
                    return 25*_random.Next(1, 5);
                }
            }
            return _random.Next(1, 11);
        }
    }

}

