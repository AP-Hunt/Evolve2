using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class Edge<T> : GraphElement<T>, ICloneable
        where T : struct
    {
        internal Vertex<T> _source;
        internal Vertex<T> _destination;
        public T Source { get; private set; }
        public virtual T Destination { get; private set; }
        public Edge(Vertex<T> Source, Vertex<T> Destination, Util.IIdentityProvider<T> IdentityProvider) : base(IdentityProvider)
        {
            this.Source = Source.Identity;
            this.Destination = Destination.Identity;

            this._source = Source;
            this._destination = Destination;
        }

        public object Clone()
        {
            return new Edge<T>((Vertex<T>)_source.Clone(), (Vertex<T>)_destination.Clone(), _identProvider);
        }
    }

    public class SubgraphEdge<T> : Edge<T>
        where T : struct
    {
        public SubgraphEdge(Vertex<T> Source, Graph<T> G, Func<Graph<T>, Vertex<T>> VertexProducer, Util.IIdentityProvider<T> IdentityProvider)
            : base(Source, VertexProducer(G), IdentityProvider)
        { }
    }
}
