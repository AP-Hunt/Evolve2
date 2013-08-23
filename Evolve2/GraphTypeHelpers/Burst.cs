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
        public Burst()
            : base(new DefaultVertexFactory(), new DefaultEdgeFactory(), new DefaultIdentityProvider())
        { }
    }

    public class Burst<T> : GraphTypeHelper<T>
        where T : struct
    {
        public Burst(IVertexFactory<T> VertexFactory, IEdgeFactory<T> EdgeFactory, IIdentityProvider<T> IdentityProvider)
            : base(VertexFactory, EdgeFactory, IdentityProvider)
        { }

        public BurstInfo<T> Create(int BurstSize, bool Directed)
        {
            if (BurstSize < 2)
            {
                throw new ArgumentOutOfRangeException("BurstSize", "Burst size must be at least 2");
            }

            Graph<T> burst = new Graph<T>(this.IdentityProvider);

            Vertex<T> centre = this.VertexFactory.NewVertex(this.IdentityProvider);
            burst.AddVertex(centre);

            for (int i = 0; i < BurstSize; i++)
            {
                Vertex<T> v = this.VertexFactory.NewVertex(this.IdentityProvider);
                burst.AddVertex(v);
                burst.AddEdge(this.EdgeFactory.NewEdge(centre, v, this.IdentityProvider), Directed);
            }

            BurstInfo<T> burstInfo = new BurstInfo<T>(burst, centre);
            return burstInfo;
        }
    }
}
