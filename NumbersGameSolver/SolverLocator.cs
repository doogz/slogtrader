using System;
using System.IO;
using System.Reflection;

namespace ScottLogic.NumbersGame
{
    /// <summary>
    /// This class's sole responsibility is to examine the assemblies in a given directory for potential 'solver' algorithms.
    /// Specifically, it uses reflection to find types that implement IGameSolver.
    /// </summary>
    /// 
    /// <remarks>
    /// THIS IS JUST A PLACEHOLDER - WORK IN PROGRESS
    /// </remarks>
    public class SolverLocator
    {
        public string SdkFolderUri
        {
            get
            {
                // GetExecutingAssembly means THIS one
                string uriPath = Assembly.GetExecutingAssembly().GetName().CodeBase;
                return Path.GetDirectoryName(uriPath);
            }
        }

        public string ExeFolderUri
        {
            get
            {
                // GetEntryAssembly means the one with the EXE in it
                string uriPath = Assembly.GetEntryAssembly().GetName().CodeBase;
                return Path.GetDirectoryName(uriPath);
            }
        }

        public string ExeFolder
        {
            get { return new Uri(ExeFolderUri).LocalPath; }
        }

        public void FindSolvers()
        {
            var di = new DirectoryInfo(ExeFolder);
            var fileInfo = di.EnumerateFiles("*.dll");

            foreach (var f in fileInfo)
            {
                // Attempt to load as assembly
                try
                {
                    Assembly asm = Assembly.LoadFile(f.FullName);
                    Console.WriteLine("Loaded {0}", f.Name);
                    var types = asm.GetExportedTypes();
                    foreach (var t in types)
                        if (t.IsClass && !t.IsAbstract)
                        {
                            var interfaces = t.GetInterfaces();
                            foreach (var i in interfaces)
                                if (i == typeof (IGameSolver))
                                    Console.WriteLine("{0} SUPPORTS IGameSolver", t.Name);
                        }
                }
                catch (Exception)
                {
                    // Fair enough, probably not a .NET assembly then
                }
                
              

            }

        }
    }
}
