using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class Graph : ICloneable
    {
        internal Guid _ident;
        public Guid Identity
        {
            get
            {
                return _ident;
            }
        }
        public Graph()
        {
            _vertices = new Dictionary<Guid, Vertex>();
            _edges = new List<Edge>();
            _subGraphs = new Dictionary<Guid, Graph>();
            _subGraphEdges = new Dictionary<Guid, List<SubgraphEdge>>();
            _ident = Guid.NewGuid();
        }

        internal Dictionary<Guid, Vertex> _vertices;
        public IEnumerable<Vertex> Vertices
        {
            get
            {
                return _vertices.Values.Union(_subGraphs.Values.SelectMany(g => g.Vertices));
            }
        }
        public IEnumerable<Guid> VertexIdentities
        {
            get
            {
                return _vertices.Keys.Union(_subGraphs.Values.SelectMany(g => g.VertexIdentities));
            }
        }
        public Vertex FindVertex(Guid Identity)
        {
            if (!_vertices.ContainsKey(Identity))
            {
                return null;
            }

            return _vertices[Identity];
        }

        internal ICollection<Edge> _edges;
        public IEnumerable<Edge> Edges
        {
            get
            {
                return _edges;
            }
        }

        internal Dictionary<Guid, Graph> _subGraphs;
        public IEnumerable<Graph> Subgraphs
        {
            get
            {
                return _subGraphs.Values;
            }
        }
        public IEnumerable<Guid> SubgraphIdentities
        {
            get
            {
                return _subGraphs.Keys;
            }
        }

        internal Dictionary<Guid, List<SubgraphEdge>> _subGraphEdges;
        public IEnumerable<SubgraphEdge> SubgraphEdgesForGraph(Guid GraphIdentity)
        {
            if (!_subGraphEdges.ContainsKey(GraphIdentity))
            {
                throw new ArgumentException("GraphIdentity", "No subgraph edges for the supplied graph identity");
            }

            return _subGraphEdges[GraphIdentity];
        }

        public void AddVertex(Vertex V)
        {
            if (!_vertices.ContainsKey(V.Identity))
            {
                _vertices.Add(V.Identity, V);
            }
        }

        internal bool hasVertex(Guid V)
        {
            return _vertices.ContainsKey(V);
        }

        public void AddEdge(Edge E, bool Directed)
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
                AddEdge(new Edge(E._destination, E._source), true);
            }
        }

        public void AddSubgraph(Vertex Source, Graph G, Func<Graph, Vertex> VertexProducer)
        {
            if (!hasVertex(Source.Identity))
            {
                throw new ArgumentException("Source", "Source vertex must be a part of the supergraph");
            }

            if (this.Identity == G.Identity)
            {
                throw new ArgumentException("G", "Subgraph cannot be the supergraph");
            }

            if(_subGraphs.ContainsKey(G.Identity))
            {
                throw new ArgumentException("G", "Subgraph already exists in supergraph");
            }

            Vertex d = VertexProducer(G);
            if (!G.hasVertex(d.Identity))
            {
                throw new ArgumentException("VertexProducer", "Vertex returned from VertexProducer must be part of the subgraph");
            }

            _subGraphs.Add(G.Identity, G);

            if (!_subGraphEdges.ContainsKey(G.Identity))
            {
                _subGraphEdges.Add(G.Identity, new List<SubgraphEdge>());
            }

            _subGraphEdges[G.Identity].Add(new SubgraphEdge(Source, G, VertexProducer));
        }

        public IEnumerable<Vertex> VerticesByIdentity(IEnumerable<Guid> Identities)
        {
            return _vertices.Where(kv => Identities.Contains(kv.Key))
                            .Select(v => v.Value);
        }

        public IEnumerable<Guid> VerticesConnectedToVertex(Guid VertexIdentity)
        {
            IEnumerable<SubgraphEdge> allSubgraphEdges = _subGraphEdges.Values.SelectMany(e => e);
            IEnumerable<Edge> allEdges = Edges.Union(allSubgraphEdges);

            return allEdges.Where(e => e.Source == VertexIdentity)
                           .Select(e => e.Destination);
        }

        public object Clone()
        {
            Graph G = new Graph();
            G._ident = this._ident;

            var newVertices = this._vertices.Select(v =>
            {
                Vertex vert = (Vertex)v.Value.Clone();
                KeyValuePair<Guid, Vertex> kv = new KeyValuePair<Guid, Vertex>(vert.Identity, vert);
                return kv;
            }).ToDictionary(kv => kv.Key, kv => kv.Value);
            G._vertices = newVertices;

            var newEdges = this._edges.Select(e => (Edge)e.Clone()).ToList();
            G._edges = new List<Edge>(newEdges);

            var newSubgraphs = this._subGraphs.Select(g =>
            {
                Graph _g = (Graph)g.Value.Clone();
                KeyValuePair<Guid, Graph> kv = new KeyValuePair<Guid, Graph>(_g.Identity, _g);
                return kv;
            }).ToDictionary(kv => kv.Key, kv => kv.Value);
            G._subGraphs = newSubgraphs;

            var newSubgraphEdges = this._subGraphEdges.Select(sge =>
            {
                var g = sge.Key;
                List<SubgraphEdge> _sge = new List<SubgraphEdge>(sge.Value.Select(e => (SubgraphEdge)e.Clone()));
                KeyValuePair<Guid, List<SubgraphEdge>> kv = new KeyValuePair<Guid, List<SubgraphEdge>>(g, _sge);
                return kv;
            }).ToDictionary(k => k.Key, v => v.Value);
            G._subGraphEdges = newSubgraphEdges;

            return G;
        }
    }
}
