using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2;

namespace Evolve2
{
    public class Graph<TIdentity> : GraphElement<TIdentity>,  ICloneable
        where TIdentity : struct
    {
        public Graph(Util.IIdentityProvider<TIdentity> IdentityProvider) : base (IdentityProvider)
        {
            _vertices = new Dictionary<TIdentity, Vertex<TIdentity>>();
            _edges = new List<Edge<TIdentity>>();
            _subGraphs = new Dictionary<TIdentity, Graph<TIdentity>>();
            _subGraphEdges = new Dictionary<TIdentity, List<SubgraphEdge<TIdentity>>>();
        }

        protected internal Dictionary<TIdentity, Vertex<TIdentity>> _vertices;
        public IEnumerable<Vertex<TIdentity>> Vertices
        {
            get
            {
                return _vertices.Values.Union(_subGraphs.Values.SelectMany(g => g.Vertices));
            }
        }
        public IEnumerable<TIdentity> VertexIdentities
        {
            get
            {
                return _vertices.Keys.Union(_subGraphs.Values.SelectMany(g => g.VertexIdentities));
            }
        }
        public Vertex<TIdentity> FindVertex(TIdentity Identity)
        {
            if (!_vertices.ContainsKey(Identity))
            {
                return null;
            }

            return _vertices[Identity];
        }

        protected internal ICollection<Edge<TIdentity>> _edges;
        public IEnumerable<Edge<TIdentity>> Edges
        {
            get
            {
                return _edges;
            }
        }

        protected internal Dictionary<TIdentity, Graph<TIdentity>> _subGraphs;
        public IEnumerable<Graph<TIdentity>> Subgraphs
        {
            get
            {
                return _subGraphs.Values;
            }
        }
        public IEnumerable<TIdentity> SubgraphIdentities
        {
            get
            {
                return _subGraphs.Keys;
            }
        }

        protected internal Dictionary<TIdentity, List<SubgraphEdge<TIdentity>>> _subGraphEdges;
        public IEnumerable<SubgraphEdge<TIdentity>> SubgraphEdgesForGraph(TIdentity GraphIdentity)
        {
            if (!_subGraphEdges.ContainsKey(GraphIdentity))
            {
                throw new ArgumentException("GraphIdentity", "No subgraph edges for the supplied graph identity");
            }

            return _subGraphEdges[GraphIdentity];
        }

        public void AddVertex(Vertex<TIdentity> V)
        {
            if (!_vertices.ContainsKey(V.Identity))
            {
                _vertices.Add(V.Identity, V);
            }
        }

        protected internal bool hasVertex(TIdentity V)
        {
            return _vertices.ContainsKey(V);
        }

        public void AddEdge(Edge<TIdentity> E, bool Directed)
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
                AddEdge(new Edge<TIdentity>(E._destination, E._source, _identProvider), true);
            }
        }

        public void AddSubgraph(Vertex<TIdentity> Source, Graph<TIdentity> Subgraph, Func<Graph<TIdentity>, Vertex<TIdentity>> VertexProducer)
        {
            if (!hasVertex(Source.Identity))
            {
                throw new ArgumentException("Source", "Source vertex must be a part of the supergraph");
            }

            if (_identProvider.Equals(this.Identity, Subgraph.Identity))
            {
                throw new ArgumentException("G", "Subgraph cannot be the supergraph");
            }

            if(_subGraphs.ContainsKey(Subgraph.Identity))
            {
                throw new ArgumentException("G", "Subgraph already exists in supergraph");
            }

            Vertex<TIdentity> subgraphVertex = VertexProducer(Subgraph);
            if (!Subgraph.hasVertex(subgraphVertex.Identity))
            {
                throw new ArgumentException("VertexProducer", "Vertex returned from VertexProducer must be part of the subgraph");
            }

            _subGraphs.Add(Subgraph.Identity, Subgraph);

            if (!_subGraphEdges.ContainsKey(Subgraph.Identity))
            {
                _subGraphEdges.Add(Subgraph.Identity, new List<SubgraphEdge<TIdentity>>());
            }

            _subGraphEdges[Subgraph.Identity].Add(new SubgraphEdge<TIdentity>(Source, Subgraph, VertexProducer, _identProvider));
        }

        public void AddSubgraphEdge(Vertex<TIdentity> Source, Graph<TIdentity> Subgraph, Func<Graph<TIdentity>, Vertex<TIdentity>> VertexProducer)
        {
            if (!hasVertex(Source.Identity))
            {
                throw new ArgumentException("Source", "Source vertex must be a part of the supergraph");
            }

            if (_identProvider.Equals(this.Identity, Subgraph.Identity))
            {
                throw new ArgumentException("G", "Subgraph cannot be the supergraph");
            }

            Vertex<TIdentity> subgraphVertex = VertexProducer(Subgraph);
            if (!Subgraph.hasVertex(subgraphVertex.Identity))
            {
                throw new ArgumentException("VertexProducer", "Vertex returned from VertexProducer must be part of the subgraph");
            }

            if (!_subGraphEdges.ContainsKey(Subgraph.Identity))
            {
                throw new ArgumentException("Subgraph", string.Format("Graph {0} must be a subgraph of this graph to add an edge. Call AddSubgraph first", Subgraph.Identity));
            }

            _subGraphEdges[Subgraph.Identity].Add(new SubgraphEdge<TIdentity>(Source, Subgraph, VertexProducer, _identProvider));
        }

        public IEnumerable<Vertex<TIdentity>> VerticesByIdentity(IEnumerable<TIdentity> Identities)
        {
            return _vertices.Where(kv => Identities.Contains(kv.Key))
                            .Select(v => v.Value);
        }

        public IEnumerable<TIdentity> VerticesConnectedToVertex(TIdentity VertexIdentity)
        {
            IEnumerable<TIdentity> verticesInSubgraphs = _subGraphs.SelectMany(g => g.Value.VerticesConnectedToVertex(VertexIdentity));
            IEnumerable<TIdentity> verticesInThisGraph = Edges.Where(e => _identProvider.Equals(e.Source, VertexIdentity))
                                                      .Select(e => e.Destination);

            return Enumerable.Union(verticesInThisGraph, verticesInSubgraphs);
        }

        public object Clone()
        {
            Graph<TIdentity> G = new Graph<TIdentity>(_identProvider);
            G._ident = this._ident;

            var newVertices = this._vertices.Select(v =>
            {
                Vertex<TIdentity> vert = (Vertex<TIdentity>)v.Value.Clone();
                KeyValuePair<TIdentity, Vertex<TIdentity>> kv = new KeyValuePair<TIdentity, Vertex<TIdentity>>(vert.Identity, vert);
                return kv;
            }).ToDictionary(kv => kv.Key, kv => kv.Value);
            G._vertices = newVertices;

            var newEdges = this._edges.Select(e => (Edge<TIdentity>)e.Clone()).ToList();
            G._edges = new List<Edge<TIdentity>>(newEdges);

            var newSubgraphs = this._subGraphs.Select(g =>
            {
                Graph<TIdentity> _g = (Graph<TIdentity>)g.Value.Clone();
                KeyValuePair<TIdentity, Graph<TIdentity>> kv = new KeyValuePair<TIdentity, Graph<TIdentity>>(_g.Identity, _g);
                return kv;
            }).ToDictionary(kv => kv.Key, kv => kv.Value);
            G._subGraphs = newSubgraphs;

            var newSubgraphEdges = this._subGraphEdges.Select(sge =>
            {
                var g = sge.Key;
                List<SubgraphEdge<TIdentity>> _sge = new List<SubgraphEdge<TIdentity>>(sge.Value.Select(e => (SubgraphEdge<TIdentity>)e.Clone()));
                KeyValuePair<TIdentity, List<SubgraphEdge<TIdentity>>> kv = new KeyValuePair<TIdentity, List<SubgraphEdge<TIdentity>>>(g, _sge);
                return kv;
            }).ToDictionary(k => k.Key, v => v.Value);
            G._subGraphEdges = newSubgraphEdges;

            return G;
        }
    }
}
