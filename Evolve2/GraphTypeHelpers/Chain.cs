using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.Util;
using Evolve2.Util.Factories;

namespace Evolve2.GraphTypeHelpers
{
    public class Chain : Chain<Guid>
    {
        public Chain()
            : base(new DefaultVertexFactory(), new DefaultEdgeFactory(), new DefaultIdentityProvider())
        { }
    }

    public class Chain<TIdentity> : GraphTypeHelper<TIdentity>
        where TIdentity : struct
    {
        public Chain(IVertexFactory<TIdentity> VertexFactory, IEdgeFactory<TIdentity> EdgeFactory, IIdentityProvider<TIdentity> IdentityProvider)
            : base(VertexFactory, EdgeFactory, IdentityProvider)
        { }

        public ChainInfo<TIdentity> Create(int ChainLength, bool Directed)
        {
            if (ChainLength < 2)
            {
                throw new ArgumentOutOfRangeException("ChainLength", "Chain length must be 2 or greater");
            }

            Graph<TIdentity> chain = new Graph<TIdentity>(this.IdentityProvider);

            //Create vertices
            List<Vertex<TIdentity>> vertices = new List<Vertex<TIdentity>>();
            for (int i = 0; i < ChainLength; i++)
            {
                Vertex<TIdentity> v = this.VertexFactory.NewVertex(this.IdentityProvider);
                vertices.Add(v);
                chain.AddVertex(v);

                System.Diagnostics.Debug.WriteLine(v.Identity);
            }

            //Add edges between (0, 1), (1, 2), ..., (N-1, N)
            ChainPart<TIdentity> startOfChain = new ChainPart<TIdentity>(vertices[0]);
            ChainPart<TIdentity> currentPart = startOfChain;
            int highestIndex = vertices.Count() - 1;
            for (int i = 0; i <= highestIndex - 1; i++)
            {
                Vertex<TIdentity> src = vertices[i];
                Vertex<TIdentity> dest = vertices[i + 1];
                chain.AddEdge(this.EdgeFactory.NewEdge(src, dest, this.IdentityProvider), Directed);

                //Get the current and next bit of the chain
                ChainPart<TIdentity> nextPart = new ChainPart<TIdentity>(dest);
                currentPart.NextVertex = nextPart;

                //Set up for the next part
                currentPart = nextPart;
            }

            //After linking them all, ensure the next part is null
            currentPart.NextVertex = null;

            ChainInfo<TIdentity> chainInfo = new ChainInfo<TIdentity>(chain, startOfChain);
            return chainInfo;
        }
    }
}
