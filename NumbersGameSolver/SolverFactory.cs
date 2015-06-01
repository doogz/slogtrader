using System;
using System.Collections.Generic;
using System.Linq;

namespace ScottLogic.NumbersGame
{
    public static class SolverFactory
    {
        // This would do for any (generic) factory
        private static readonly List<Type> _solvers = new List<Type>();
        
        private static readonly List<string> _solverDescriptions = new List<string>();

        public static void Register(Type t, string typeDescription)
        {
            _solvers.Add(t);
            _solverDescriptions.Add(typeDescription);
        }

        public static IEnumerable<string> GetSolverDescriptions()
        {
            return _solverDescriptions;
        }

        public static string GetSolverDescription(int index)
        {
            return _solverDescriptions[index];
        }

        public static int RegisteredSolvers
        {
            get { return _solvers.Count; }
        }

        public static IGameSolver CreateSolver(int idx=0)
        {
            return !_solvers.Any() || idx >= _solvers.Count ? null : CreateSolver(_solvers[idx]);
        }

        public static IGameSolver CreateSolver(string algoName)
        {
            throw new NotImplementedException(); //return !_solvers.Any() ? null : CreateSolver(_solvers[0]);
        }


        private static IGameSolver CreateSolver(Type algorithmType)
        {
            if (!_solvers.Any())
                return null;

            return (IGameSolver) Activator.CreateInstance(algorithmType);

        }

    }
}
