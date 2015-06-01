using System;
using System.Collections.Generic;

namespace ScottLogic.NumbersGame.Console
{
    internal class Program
    {
        private const int NumberOfNumbers = 6;
        private static readonly Random _random = new Random();
        private static int _defaultSolver = 0;

        private static void Main(string[] args)
        {
            SolverLocator sl = new SolverLocator();
            sl.FindSolvers();
            
            string command = "";
            do
            {
                System.Console.BackgroundColor = ConsoleColor.Blue;
                System.Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.Clear();
                System.Console.WriteLine("*** Numbers Game Puzzle - Console Application ***\n");
                System.Console.WriteLine("\r\nRegistered solutions:");
                IEnumerable<string> descriptions = SolverFactory.GetSolverDescriptions();
                foreach (var d in descriptions) System.Console.WriteLine("{0}", d);
                System.Console.WriteLine("\r\nMenu:\r\n");
                System.Console.WriteLine("S: Select Solver");
                System.Console.WriteLine("P: Play the game manually");
                System.Console.WriteLine("C: Run the competition");
                System.Console.WriteLine("Q: Quit the console");
                System.Console.Write(">");
                var input=System.Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                {
                    command = input.ToUpper().Substring(0, 1);
                    switch (command)
                    {
                        case "S":
                            SelectSolver();
                            break;
                        case "P":
                            PlayGame();
                            break;
                        case "C":
                            RunCompetition();
                            break;
                    }
                }

            } while (command!="Q");

            System.Console.WriteLine("Goodbye");
            System.Console.ReadLine();
        }

        public static void SelectSolver()
        {
            System.Console.BackgroundColor = ConsoleColor.Red;
            System.Console.ForegroundColor= ConsoleColor.Black;
            System.Console.Clear();
            System.Console.WriteLine("*** Registered Solvers ***");
            System.Console.WriteLine("\r\nThe current default is shown with an asterisk (*)\r\n");
            System.Console.WriteLine("Index Description");
            int numSolvers = SolverFactory.RegisteredSolvers;
            for (int n=0; n<numSolvers; ++n)
            {
                char defaultChar = (n==_defaultSolver) ? '*' : ' ';
                string description = SolverFactory.GetSolverDescription(n);
                System.Console.WriteLine("{0}  {1}  {2}", defaultChar, n, description);
            }
            System.Console.WriteLine("Enter index between {0} and {1} to select the default solver");
            string input = System.Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                int.TryParse(input, out _defaultSolver);

            }
        }
        public static void RunCompetition()
        {
            System.Console.BackgroundColor = ConsoleColor.Blue;
            System.Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.Clear();
            
            System.Console.WriteLine("*** Competition ***");
            for (int n=0; n<3; ++n)
            { 
                System.Console.WriteLine("Round {0} - Standard Numbers Game with {1} big numbers", (n+1), n);
                for (int t = 0; t < 10; ++t)
                {
                    System.Console.WriteLine("Test {0}", t + 1);


                    int[] initialNumbers;
                    int target;
                    if (GameGenerator.GenerateCountdownGame(n, out initialNumbers, out target))
                    {
                        System.Console.Write("Numbers: [");
                        for (int i = 0; i < initialNumbers.Length; ++i)
                        {
                            System.Console.Write("{0}", initialNumbers[i]);
                            if (i < initialNumbers.Length - 1)
                                System.Console.Write(", ");
                        }
                        System.Console.WriteLine("]");
                        System.Console.WriteLine("\r\nTarget: {0}", target);

                    }
                    // Now try our solutions
                    int numSolvers = SolverFactory.RegisteredSolvers;
                    for (int s = 0; s < numSolvers; ++s)
                    {
                        var solver = SolverFactory.CreateSolver(s);
                        ISolution solution;
                        if (solver.GetSolution(initialNumbers, target, out solution))
                        {

                        }
                    }
                }

            }
        }

        public static void PlayGame()
        {
            System.Console.WriteLine("Enter your numbers, one at a time, or together separated by commas.");
            System.Console.WriteLine("You can also specify the target after '=', or enter it separately.");
          
            while (true)
            {

                int[] numbers;
                int target;
                if (GetNumbersFromConsole(out numbers, out target))
                {
                    System.Console.Write("The numbers are: [ ");
                    for (int idx = 0; idx < numbers.Length; ++idx)
                    {
                        System.Console.Write("{0}", numbers[idx]);
                        if (idx < numbers.Length - 1)
                            System.Console.Write(", ");
                    }
                    System.Console.WriteLine(" ]");
                    System.Console.WriteLine("Target: {0}", target);

                    var solver = SolverFactory.CreateSolver(_defaultSolver);

                    ISolution solution;
                    if (solver.GetSolution(numbers, target, out solution))
                    {
                        System.Console.WriteLine("Solved in {0}s, as follows:\r\n{1}",
                            solution.ExecutionTime.TotalSeconds,
                            solution.GetMultilineDisplayString());
                    }
                    else
                    {
                        System.Console.WriteLine("Not solvable, exhausted possibilities in {0}s",
                            solution.ExecutionTime.TotalSeconds);

                    }

                }
                else
                {
                    break;
                }
            }

        }

        //TODO: Refactor - overly long method
        private static bool GetNumbersFromConsole(out int[] numbers, out int target)
        {
            target = 0;
            numbers = null;
            var currentNumbers = new List<int>();
            bool numbersEntered = false;
            while (!numbersEntered)
            {
                System.Console.Write("Number(s) (Q to quit):");
                var input = System.Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    if (currentNumbers.Count == 0)
                    {
                        GameGenerator.GenerateCountdownGame(2, out numbers, out target);
                        return true;
                    }
                    numbersEntered = true;
                    continue; // Individual numbers entry is terminated with a blank entry
                }
                if (input.ToUpper().Substring(0, 1) == "Q")
                    return false;

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
                // We may have '= <Target>' bolted on to the string
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
                System.Console.WriteLine("Enter target, or hit enter to generate a random target (Q to quit):");
                var input = System.Console.ReadLine();
                if (!string.IsNullOrEmpty(input))
                    if (input.ToUpper().Substring(0, 1) == "Q")
                        return false;
                    else 
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

