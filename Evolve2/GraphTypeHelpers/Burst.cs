using Evolve2.Util;
using Evolve2.Util.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.GraphTypeHelpers
{
    public class Burst : Burst<Guid>
    {
        public Burst(IIdentityProvider<Guid> IdentityProvider)
            : base(new DefaultVertexFactory(), new DefaultEdgeFactory(), IdentityProvider)
        { }

        public Burst()
            : base(new DefaultVertexFactory(), new DefaultEdgeFactory(), new DefaultIdentityProvider())
        { }
    }

    public class Burst<TIdentity> : GraphTypeHelper<TIdentity>
        where TIdentity : struct
    {
        public Burst(IVertexFactory<TIdentity> VertexFactory, IEdgeFactory<TIdentity> EdgeFactory, IIdentityProvider<TIdentity> IdentityProvider)
            : base(VertexFactory, EdgeFactory, IdentityProvider)
        { }

        public BurstInfo<TIdentity> Create(int BurstSize, bool Directed)
        {
            if (BurstSize < 2)
            {
                throw new ArgumentOutOfRangeException("BurstSize", "Burst size must be at least 2");
            }

            Graph<TIdentity> burst = new Graph<TIdentity>(this.IdentityProvider);

            Vertex<TIdentity> centre = this.VertexFactory.NewVertex(this.IdentityProvider);
            burst.AddVertex(centre);

            for (int i = 0; i < BurstSize; i++)
            {
                Vertex<TIdentity> v = this.VertexFactory.NewVertex(this.IdentityProvider);
                burst.AddVertex(v);
                burst.AddEdge(this.EdgeFactory.NewEdge(centre, v, this.IdentityProvider), Directed);
            }

            BurstInfo<TIdentity> burstInfo = new BurstInfo<TIdentity>(burst, centre);
            return burstInfo;
        }
    }
}
