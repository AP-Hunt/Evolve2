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
        public Clique(IIdentityProvider<Guid> IdentityProvider)
            : base(new DefaultVertexFactory(), new DefaultEdgeFactory(), IdentityProvider)
        { }

        public Clique()
            : base(new DefaultVertexFactory(), new DefaultEdgeFactory(), new DefaultIdentityProvider())
        { }
    }

    public class Clique<TIdentity> : GraphTypeHelper<TIdentity>
        where TIdentity : struct
    {
        public Clique(IVertexFactory<TIdentity> VertexFactory, IEdgeFactory<TIdentity> EdgeFactory, IIdentityProvider<TIdentity> IdentityProvider)
            : base(VertexFactory, EdgeFactory, IdentityProvider)
        { }

        public CliqueInfo<TIdentity> Create(int CliqueSize)
        {
            if (CliqueSize < 2)
            {
                throw new ArgumentOutOfRangeException("CliqueSize", "Clique size must be at least 2");
            }

            Graph<TIdentity> clique = new Graph<TIdentity>(this.IdentityProvider);

            List<Vertex<TIdentity>> vertices = new List<Vertex<TIdentity>>();
            for (int i = 0; i < CliqueSize; i++)
            {
                Vertex<TIdentity> v = this.VertexFactory.NewVertex(this.IdentityProvider);
                vertices.Add(v);
                clique.AddVertex(v);
            }

            for (int i = 0; i <= CliqueSize-1; i++)
            {
                Vertex<TIdentity> v1 = vertices[i];
                List<Vertex<TIdentity>> remaining = vertices.Skip(i + 1).ToList();

                foreach (Vertex<TIdentity> v2 in remaining)
                {
                    clique.AddEdge(this.EdgeFactory.NewEdge(v1, v2, this.IdentityProvider), false);
                }
            }

            return new CliqueInfo<TIdentity>(clique);
        }
    }
}
