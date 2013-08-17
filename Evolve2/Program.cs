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
            Vertex<Guid> v1 = new Vertex<Guid>(identProv);
            Vertex<Guid> v2 = new Vertex<Guid>(identProv);
            Vertex<Guid> v3 = new Vertex<Guid>(identProv);
            Vertex<Guid> v4 = new Vertex<Guid>(identProv, State.MUTANT);
            superGraph.AddEdge(new Edge<Guid>(v1, v2, identProv), false);
            superGraph.AddEdge(new Edge<Guid>(v1, v3, identProv), false);
            superGraph.AddEdge(new Edge<Guid>(v1, v4, identProv), false);
            superGraph.AddEdge(new Edge<Guid>(v2, v3, identProv), false);
            superGraph.AddEdge(new Edge<Guid>(v2, v4, identProv), false);
            superGraph.AddEdge(new Edge<Guid>(v3, v4, identProv), false);


            SimulationRunner runner = new SimulationRunner(new StateSelector(), new VertexSelector(), new VictimSelector());
            SimulationResult result = runner.RunOn(superGraph, 1000, 1000);

            Console.WriteLine("Result");
            Console.WriteLine("\t Reps " + result.RepetitionsPerformed);
            Console.WriteLine("\t Fixations " + result.Fixations);
            Console.WriteLine("\t Extinctions " + result.Extinctions);
            Console.WriteLine("\t Fix % " + result.FixationProbability);
            Console.WriteLine("\t Ext % " + result.ExtinctionProbability);
            Console.ReadLine();
        }

        static void WriteGraph<T>(Graph<T> G) where T : struct
        {
            Console.WriteLine("Graph " + G.Identity);
            foreach(T v in G.VertexIdentities)

            {
                Console.WriteLine("\t " + v);
            }

            foreach (Edge<T> e in G.Edges)
            {
                Console.WriteLine("\t " + e.Source + " -> " + e.Destination);
            }
        }

        static void WriteGraphs<T>(Graph<T> superGraph, Graph<T> subGraph) where T : struct
        {
            Console.WriteLine("Supergraph");
            Console.WriteLine("\t Vertices");
            foreach (KeyValuePair<T, Vertex<T>> V in superGraph._vertices)
            {
                Console.WriteLine("\t\t " + V.Key);
            }

            Console.WriteLine("\t Edges");
            foreach (Edge<T> E in superGraph._edges)
            {
                Console.WriteLine("\t\t " + E.Source + " -> " + E.Destination);
            }

            Console.WriteLine("\t Subgraphs");
            foreach (KeyValuePair<T, Graph<T>> sg in superGraph._subGraphs)
            {
                Console.WriteLine("\t\t " + sg.Key);

                Console.WriteLine("\t\t\t Vertices");
                foreach (KeyValuePair<T, Vertex<T>> V in sg.Value._vertices)
                {
                    Console.WriteLine("\t\t\t\t " + V.Key);
                }

                Console.WriteLine("\t\t\t Edges");
                foreach (Edge<T> E in sg.Value._edges)
                {
                    Console.WriteLine("\t\t\t\t " + E.Source + " -> " + E.Destination);
                }
            }

            Console.WriteLine("\t Supergraph Edges");
            foreach (KeyValuePair<T, List<SubgraphEdge<T>>> sge in superGraph._subGraphEdges)
            {
                Console.WriteLine("\t\t To " + sge.Key);
                foreach (SubgraphEdge<T> e in sge.Value)
                {
                    Console.WriteLine("\t\t\t " + e.Source + " -> " + e.Destination);
                }
            }
        }

    }
}