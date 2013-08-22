using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2;

namespace Evolve2
{
    public class Graph<T> : GraphElement<T>,  ICloneable
        where T : struct
    {
        public Graph(Util.IIdentityProvider<T> IdentityProvider) : base (IdentityProvider)
        {
            _vertices = new Dictionary<T, Vertex<T>>();
            _edges = new List<Edge<T>>();
            _subGraphs = new Dictionary<T, Graph<T>>();
            _subGraphEdges = new Dictionary<T, List<SubgraphEdge<T>>>();
        }

        protected internal Dictionary<T, Vertex<T>> _vertices;
        public IEnumerable<Vertex<T>> Vertices
        {
            get
            {
                return _vertices.Values.Union(_subGraphs.Values.SelectMany(g => g.Vertices));
            }
        }
        public IEnumerable<T> VertexIdentities
        {
            get
            {
                return _vertices.Keys.Union(_subGraphs.Values.SelectMany(g => g.VertexIdentities));
            }
        }
        public Vertex<T> FindVertex(T Identity)
        {
            if (!_vertices.ContainsKey(Identity))
            {
                return null;
            }

            return _vertices[Identity];
        }

        protected internal ICollection<Edge<T>> _edges;
        public IEnumerable<Edge<T>> Edges
        {
            get
            {
                return _edges;
            }
        }

        protected internal Dictionary<T, Graph<T>> _subGraphs;
        public IEnumerable<Graph<T>> Subgraphs
        {
            get
            {
                return _subGraphs.Values;
            }
        }
        public IEnumerable<T> SubgraphIdentities
        {
            get
            {
                return _subGraphs.Keys;
            }
        }

        protected internal Dictionary<T, List<SubgraphEdge<T>>> _subGraphEdges;
        public IEnumerable<SubgraphEdge<T>> SubgraphEdgesForGraph(T GraphIdentity)
        {
            if (!_subGraphEdges.ContainsKey(GraphIdentity))
            {
                throw new ArgumentException("GraphIdentity", "No subgraph edges for the supplied graph identity");
            }

            return _subGraphEdges[GraphIdentity];
        }

        public void AddVertex(Vertex<T> V)
        {
            if (!_vertices.ContainsKey(V.Identity))
            {
                _vertices.Add(V.Identity, V);
            }
        }

        protected internal bool hasVertex(T V)
        {
            return _vertices.ContainsKey(V);
        }

        public void AddEdge(Edge<T> E, bool Directed)
        {
            if(!hasVertex(E.Source))
            {
                AddVertex(E._source);
            }

            if (!hasVertex(E.Destination))
            {
                AddVertex(E._destination);
            }

            _edges.Add(E);

            if (!Directed)
            {
                AddEdge(new Edge<T>(E._destination, E._source, _identProvider), true);
            }
        }

        public void AddSubgraph(Vertex<T> Source, Graph<T> G, Func<Graph<T>, Vertex<T>> VertexProducer)
        {
            if (!hasVertex(Source.Identity))
            {
                throw new ArgumentException("Source", "Source vertex must be a part of the supergraph");
            }

            if (_identProvider.Equals(this.Identity, G.Identity))
            {
                throw new ArgumentException("G", "Subgraph cannot be the supergraph");
            }

            if(_subGraphs.ContainsKey(G.Identity))
            {
                throw new ArgumentException("G", "Subgraph already exists in supergraph");
            }

            Vertex<T> d = VertexProducer(G);
            if (!G.hasVertex(d.Identity))
            {
                throw new ArgumentException("VertexProducer", "Vertex returned from VertexProducer must be part of the subgraph");
            }

            _subGraphs.Add(G.Identity, G);

            if (!_subGraphEdges.ContainsKey(G.Identity))
            {
                _subGraphEdges.Add(G.Identity, new List<SubgraphEdge<T>>());
            }

            _subGraphEdges[G.Identity].Add(new SubgraphEdge<T>(Source, G, VertexProducer, _identProvider));
        }

        public IEnumerable<Vertex<T>> VerticesByIdentity(IEnumerable<T> Identities)
        {
            return _vertices.Where(kv => Identities.Contains(kv.Key))
                            .Select(v => v.Value);
        }

        public IEnumerable<T> VerticesConnectedToVertex(T VertexIdentity)
        {
            IEnumerable<T> verticesInSubgraphs = _subGraphs.SelectMany(g => g.Value.VerticesConnectedToVertex(VertexIdentity));
            IEnumerable<T> verticesInThisGraph = Edges.Where(e => _identProvider.Equals(e.Source, VertexIdentity))
                                                      .Select(e => e.Destination);

            return Enumerable.Union(verticesInThisGraph, verticesInSubgraphs);
        }

        public object Clone()
        {
            Graph<T> G = new Graph<T>(_identProvider);
            G._ident = this._ident;

            var newVertices = this._vertices.Select(v =>
            {
                Vertex<T> vert = (Vertex<T>)v.Value.Clone();
                KeyValuePair<T, Vertex<T>> kv = new KeyValuePair<T, Vertex<T>>(vert.Identity, vert);
                return kv;
            }).ToDictionary(kv => kv.Key, kv => kv.Value);
            G._vertices = newVertices;

            var newEdges = this._edges.Select(e => (Edge<T>)e.Clone()).ToList();
            G._edges = new List<Edge<T>>(newEdges);

            var newSubgraphs = this._subGraphs.Select(g =>
            {
                Graph<T> _g = (Graph<T>)g.Value.Clone();
                KeyValuePair<T, Graph<T>> kv = new KeyValuePair<T, Graph<T>>(_g.Identity, _g);
                return kv;
            }).ToDictionary(kv => kv.Key, kv => kv.Value);
            G._subGraphs = newSubgraphs;

            var newSubgraphEdges = this._subGraphEdges.Select(sge =>
            {
                var g = sge.Key;
                List<SubgraphEdge<T>> _sge = new List<SubgraphEdge<T>>(sge.Value.Select(e => (SubgraphEdge<T>)e.Clone()));
                KeyValuePair<T, List<SubgraphEdge<T>>> kv = new KeyValuePair<T, List<SubgraphEdge<T>>>(g, _sge);
                return kv;
            }).ToDictionary(k => k.Key, v => v.Value);
            G._subGraphEdges = newSubgraphEdges;

            return G;
        }
    }
}
