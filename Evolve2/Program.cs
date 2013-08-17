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
            Graph superGraph = new Graph();
            Vertex v1 = new Vertex();
            Vertex v2 = new Vertex();
            Vertex v3 = new Vertex();
            Vertex v4 = new Vertex(State.MUTANT);
            superGraph.AddEdge(new Edge(v1, v2), false);
            superGraph.AddEdge(new Edge(v1, v3), false);
            superGraph.AddEdge(new Edge(v1, v4), false);
            superGraph.AddEdge(new Edge(v2, v3), false);
            superGraph.AddEdge(new Edge(v2, v4), false);
            superGraph.AddEdge(new Edge(v3, v4), false);


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

        static void WriteGraph(Graph G)
        {
            Console.WriteLine("Graph " + G.Identity);
            foreach(Guid v in G.VertexIdentities)

            {
                Console.WriteLine("\t " + v);
            }

            foreach (Edge e in G.Edges)
            {
                Console.WriteLine("\t " + e.Source + " -> " + e.Destination);
            }
        }

        static void WriteGraphs(Graph superGraph, Graph subGraph)
        {
            Console.WriteLine("Supergraph");
            Console.WriteLine("\t Vertices");
            foreach (KeyValuePair<Guid, Vertex> V in superGraph._vertices)
            {
                Console.WriteLine("\t\t " + V.Key);
            }

            Console.WriteLine("\t Edges");
            foreach (Edge E in superGraph._edges)
            {
                Console.WriteLine("\t\t " + E.Source + " -> " + E.Destination);
            }

            Console.WriteLine("\t Subgraphs");
            foreach (KeyValuePair<Guid, Graph> sg in superGraph._subGraphs)
            {
                Console.WriteLine("\t\t " + sg.Key);

                Console.WriteLine("\t\t\t Vertices");
                foreach (KeyValuePair<Guid, Vertex> V in sg.Value._vertices)
                {
                    Console.WriteLine("\t\t\t\t " + V.Key);
                }

                Console.WriteLine("\t\t\t Edges");
                foreach (Edge E in sg.Value._edges)
                {
                    Console.WriteLine("\t\t\t\t " + E.Source + " -> " + E.Destination);
                }
            }

            Console.WriteLine("\t Supergraph Edges");
            foreach (KeyValuePair<Guid, List<SubgraphEdge>> sge in superGraph._subGraphEdges)
            {
                Console.WriteLine("\t\t To " + sge.Key);
                foreach (SubgraphEdge e in sge.Value)
                {
                    Console.WriteLine("\t\t\t " + e.Source + " -> " + e.Destination);
                }
            }
        }

    }
}