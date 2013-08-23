using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class Edge<TIdentity> : GraphElement<TIdentity>, ICloneable
        where TIdentity : struct
    {
        internal Vertex<TIdentity> _source;
        internal Vertex<TIdentity> _destination;
        public TIdentity Source { get; private set; }
        public virtual TIdentity Destination { get; private set; }
        public Edge(Vertex<TIdentity> Source, Vertex<TIdentity> Destination, Util.IIdentityProvider<TIdentity> IdentityProvider) : base(IdentityProvider)
        {
            this.Source = Source.Identity;
            this.Destination = Destination.Identity;

            this._source = Source;
            this._destination = Destination;
        }

        public object Clone()
        {
            return new Edge<TIdentity>((Vertex<TIdentity>)_source.Clone(), (Vertex<TIdentity>)_destination.Clone(), _identProvider);
        }
    }

    public class SubgraphEdge<TIdentity> : Edge<TIdentity>
        where TIdentity : struct
    {
        public SubgraphEdge(Vertex<TIdentity> Source, Graph<TIdentity> G, Func<Graph<TIdentity>, Vertex<TIdentity>> VertexProducer, Util.IIdentityProvider<TIdentity> IdentityProvider)
            : base(Source, VertexProducer(G), IdentityProvider)
        { }
    }
}
