using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class Edge : ICloneable
    {
        internal Vertex _source;
        internal Vertex _destination;
        public Guid Source { get; private set; }
        public virtual Guid Destination { get; private set; }
        public Edge(Vertex Source, Vertex Destination)
        {
            this.Source = Source.Identity;
            this.Destination = Destination.Identity;

            this._source = Source;
            this._destination = Destination;
        }

        public object Clone()
        {
            return new Edge((Vertex)_source.Clone(), (Vertex)_destination.Clone());
        }
    }

    public class SubgraphEdge : Edge
    {
        public SubgraphEdge(Vertex Source, Graph G, Func<Graph, Vertex> VertexProducer)
            : base(Source, VertexProducer(G))
        { }
    }
}
