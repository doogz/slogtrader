using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ScottLogic.NumbersGame
{
    /// <summary>
    /// This class's sole responsibility is to examine the assemblies in a given directory for potential 'solver' algorithms.
    /// Specifically, it uses reflection to find types that implement IGameSolver.
    /// </summary>
    /// 
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

        /// <summary>
        /// Uses reflection to look for a custome Description attribute.
        /// If one doesn't exist, the class name is exploded out from Upper-camel case to words
        /// </summary>
        /// <returns></returns>
        public string GetTypeDescription(Type t)
        {
            var descriptions = t.GetCustomAttributes(typeof (DescriptionAttribute), false);
            if (descriptions.Length == 0)
                return CamelCaseToWords(t.Name);

            return ((DescriptionAttribute)descriptions[0]).Description;
        }

        private string CamelCaseToWords(string input)
        {
            string output = Regex.Replace(
                input,
                "([A-Z])",
                " $1",
                RegexOptions.Compiled).Trim();
            return output;
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
                    //Console.WriteLine("Loaded {0}", f.Name);
                    var types = asm.GetExportedTypes();
                    foreach (var t in types)
                        if (t.IsClass && !t.IsAbstract)
                        {

                            var interfaces = t.GetInterfaces();
                            foreach (var i in interfaces)
                                if (i == typeof (IGameSolver))
                                {
                                    //Console.WriteLine("{0} SUPPORTS IGameSolver", t.Name);
                                    SolverFactory.Register(t, GetTypeDescription(t));
                                }
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
