using Evolve2.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    class Program
    {
        static void Main(string[] args)
        {
            Util.IIdentityProvider<Guid> identProv = new Util.GuidIdentityProvider();
            Graph<Guid> superGraph = new Graph<Guid>(identProv);


            List<Vertex<Guid>> vertices = new List<Vertex<Guid>>();
            for (int i = 0; i < 49; i++)
            {
                vertices.Add(new Vertex<Guid>(identProv));
            }
            vertices.Add(new Vertex<Guid>(identProv, State.MUTANT));

            for (int i = 0; i <= 49; i++)
            {
                Vertex<Guid> v1 = vertices[i];
                List<Vertex<Guid>> remaining = vertices.Skip(i+1).ToList();

                foreach (Vertex<Guid> v2 in remaining)
                {
                    superGraph.AddEdge(new Edge<Guid>(v1, v2, identProv), false);
                }
            }

            SimulationRunner runner = new SimulationRunner(new StateSelector(), new VertexSelector(), new VictimSelector());
            SimulationResult result = runner.RunOn(superGraph, 50, 1000);

            Console.WriteLine("Result");
            Console.WriteLine("\t Reps " + result.RepetitionsPerformed);
            Console.WriteLine("\t Fixations " + result.Fixations);
            Console.WriteLine("\t Extinctions " + result.Extinctions);
            Console.WriteLine("\t Timeouts" + result.Timeout);
            Console.WriteLine("\t Fix % " + result.FixationProbability);
            Console.WriteLine("\t Ext % " + result.ExtinctionProbability);
            Console.WriteLine("\t TO % " + result.TimeoutProbability);
            Console.ReadLine();
        }
    }
}