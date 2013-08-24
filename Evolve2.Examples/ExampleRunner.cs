using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Examples
{
    /// <summary>
    /// Runs all the examples in this project
    /// </summary>
    static class ExampleRunner
    {
        static void Main(string[] args)
        {
            ModifiedMoranProcess.UsingAGraphTypeHelper();
            ModifiedMoranProcess.UsingAManuallyBuiltGraph();

            Console.WriteLine("Press any key to end");
            Console.ReadLine();
        }
    }
}
