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

    public class Chain<T> : GraphTypeHelper<T>
        where T : struct
    {
        public Chain(IVertexFactory<T> VertexFactory, IEdgeFactory<T> EdgeFactory, IIdentityProvider<T> IdentityProvider)
            : base(VertexFactory, EdgeFactory, IdentityProvider)
        { }

        public Graph<T> Create(int ChainLength, bool Directed)
        {
            if (ChainLength < 2)
            {
                throw new ArgumentOutOfRangeException("ChainLength", "Chain length must be 2 or greater");
            }

            Graph<T> chain = new Graph<T>(this.IdentityProvider);

            //Create vertices
            List<Vertex<T>> vertices = new List<Vertex<T>>();
            for (int i = 0; i < ChainLength; i++)
            {
                Vertex<T> v = this.VertexFactory.NewVertex(this.IdentityProvider);
                vertices.Add(v);
                chain.AddVertex(v);
            }

            //Add edges between (0, 1), (1, 2), ..., (N-1, N)
            int k = ChainLength-1;
            for (int i = 0; i < k - 2; i++)
            {
                Vertex<T> src = vertices[i];
                Vertex<T> dest = vertices[i + 1];
                chain.AddEdge(this.EdgeFactory.NewEdge(src, dest, this.IdentityProvider), Directed);
            }

            return chain;
        }
    }
}
