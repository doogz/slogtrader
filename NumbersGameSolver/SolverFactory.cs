using System;
using System.Collections.Generic;
using System.Linq;

namespace ScottLogic.NumbersGame
{
    public static class SolverFactory
    {
        // This would do for any (generic) factory; indeed the interface return type could be the second type for a generic Factory<T,I>
        private static readonly List<Type> _solvers = new List<Type>();


        private static readonly List<string> _algorithmNames = new List<string>();
        public static void Register(Type t)
        {
            _solvers.Add(t);
        }

        public static IGameSolver CreateSolver()
        {
            return !_solvers.Any() ? null : CreateSolver(_solvers[0]);
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
