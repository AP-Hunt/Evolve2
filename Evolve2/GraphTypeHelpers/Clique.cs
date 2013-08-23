using Evolve2.Util;
using Evolve2.Util.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.GraphTypeHelpers
{
    public class Clique : Clique<Guid>
    {
        public Clique()
            : base(new DefaultVertexFactory(), new DefaultEdgeFactory(), new DefaultIdentityProvider())
        { }
    }

    public class Clique<T> : GraphTypeHelper<T>
        where T : struct
    {
        public Clique(IVertexFactory<T> VertexFactory, IEdgeFactory<T> EdgeFactory, IIdentityProvider<T> IdentityProvider)
            : base(VertexFactory, EdgeFactory, IdentityProvider)
        { }

        public CliqueInfo<T> Create(int CliqueSize)
        {
            if (CliqueSize < 2)
            {
                throw new ArgumentOutOfRangeException("CliqueSize", "Clique size must be at least 2");
            }

            Graph<T> clique = new Graph<T>(this.IdentityProvider);

            List<Vertex<T>> vertices = new List<Vertex<T>>();
            for (int i = 0; i < CliqueSize; i++)
            {
                Vertex<T> v = this.VertexFactory.NewVertex(this.IdentityProvider);
                vertices.Add(v);
                clique.AddVertex(v);
            }

            for (int i = 0; i <= CliqueSize-1; i++)
            {
                Vertex<T> v1 = vertices[i];
                List<Vertex<T>> remaining = vertices.Skip(i + 1).ToList();

                foreach (Vertex<T> v2 in remaining)
                {
                    clique.AddEdge(this.EdgeFactory.NewEdge(v1, v2, this.IdentityProvider), false);
                }
            }

            return new CliqueInfo<T>(clique);
        }
    }
}
