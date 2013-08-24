using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Evolve2;
using Evolve2.GraphTypeHelpers;
using Evolve2.Util;
using Evolve2.Util.Factories;

namespace Evolve2.Examples
{
    public static class Graph
    {
        public static void AddingAVertex()
        {
            //Construct the graph with the same identity provider as will be used for the vertices
            IIdentityProvider<Guid> identityProvider = new DefaultIdentityProvider();
            Graph<Guid> graph = new Graph<Guid>(identityProvider);

            //Create a vertex, ensuring the graph and the vertex have the same identity provider
            Vertex<Guid> v = new Vertex<Guid>(identityProvider);

            //Add the vertex
            graph.AddVertex(v);
        }

        public static void AddingAnEdge()
        {
            //Construct the graph with the same identity provider as will be used for the vertices and edges
            IIdentityProvider<Guid> identityProvider = new DefaultIdentityProvider();
            Graph<Guid> graph = new Graph<Guid>(identityProvider);

            Vertex<Guid> v1 = new Vertex<Guid>(identityProvider);
            Vertex<Guid> v2 = new Vertex<Guid>(identityProvider);

            //Create an edge, providing the same identity provider as the vertices and graph
            Edge<Guid> edge = new Edge<Guid>(v1, v2, identityProvider);

            //Add it to the graph. 
            //If Directed is false, the graph adds an edge the opposite way (destination to source)
            graph.AddEdge(edge, false);
        }

        public static void AddingASubgraph()
        {
            //Construct the graph with the same identity provider as will be used for the vertices, edge and subgraphs
            IIdentityProvider<Guid> identityProvider = new DefaultIdentityProvider();
            Graph<Guid> graph = new Graph<Guid>(identityProvider);          

            //There must be at least one vertex in the graph before adding a subgraph,
            //so the subgraph has something to connect to
            Vertex<Guid> graph_v1 = new Vertex<Guid>(identityProvider);
            graph.AddVertex(graph_v1);

            //Create a second graph which will become the subgraph, providing the same identity provider
            Chain<Guid> chainHelper = new Chain<Guid>(new DefaultVertexFactory(), new DefaultEdgeFactory(), identityProvider);
            Graph<Guid> subGraph = chainHelper.Create(10, false).Graph;

            //Add the subgraph
            //The source is the vertex in the main graph that the subgraph will connect to
            //The vertex producer should return the vertex in the subgraph which will be connected in to the main graph
            graph.AddSubgraph(graph_v1, subGraph, (g) => g.Vertices.First());
        }
    }
}
