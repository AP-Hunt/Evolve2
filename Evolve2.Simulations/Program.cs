using Evolve2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moran = Evolve2.Simulations.ModifiedMoranProcess;

namespace Evolve2.Simulations
{
    class Program
    {
        static void Main(string[] args)
        {
            Util.IIdentityProvider<Guid> identProv = new Util.GuidIdentityProvider();
            Graph<Guid> superGraph = new Graph<Guid>(identProv);


            List<StatefulVertex<Guid, Moran.VertexState>> vertices = new List<StatefulVertex<Guid, Moran.VertexState>>();
            for (int i = 0; i < 49; i++)
            {
                vertices.Add(new StatefulVertex<Guid, Moran.VertexState>(Moran.VertexState.HEALTHY, identProv));
            }
            vertices.Add(new StatefulVertex<Guid, Moran.VertexState>(Moran.VertexState.MUTANT, identProv));

            for (int i = 0; i <= 49; i++)
            {
                StatefulVertex<Guid, Moran.VertexState> v1 = vertices[i];
                List<StatefulVertex<Guid, Moran.VertexState>> remaining = vertices.Skip(i + 1).ToList();

                foreach (Vertex<Guid> v2 in remaining)
                {
                    superGraph.AddEdge(new Edge<Guid>(v1, v2, identProv), false);
                }
            }

            Moran.SimulationRunner runner = new Moran.SimulationRunner(new Moran.StateSelector(), new Moran.VertexSelector(), new Moran.VictimSelector());
            Moran.SimulationResult result = runner.RunOn(superGraph, 50, 1000);

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