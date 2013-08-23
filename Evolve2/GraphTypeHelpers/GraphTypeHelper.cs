using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.Util;
using Evolve2.Util.Factories;

namespace Evolve2.GraphTypeHelpers
{
    public class GraphTypeHelper<TIdentity>
        where TIdentity : struct
    {
        protected IVertexFactory<TIdentity> VertexFactory { get; private set; }
        protected IEdgeFactory<TIdentity> EdgeFactory { get; private set; }
        protected IIdentityProvider<TIdentity> IdentityProvider { get; private set; }

        public GraphTypeHelper(IVertexFactory<TIdentity> VertexFactory, IEdgeFactory<TIdentity> EdgeFactory, IIdentityProvider<TIdentity> IdentityProvider)
        {
            if (VertexFactory == null)
            {
                throw new ArgumentNullException("VertexFactory");
            }

            if (EdgeFactory == null)
            {
                throw new ArgumentNullException("EdgeFactory");
            }

            if (IdentityProvider == null)
            {
                throw new ArgumentNullException("IdentityProvider");
            }

            this.VertexFactory = VertexFactory;
            this.EdgeFactory = EdgeFactory;
            this.IdentityProvider = IdentityProvider;
        }
    }
}
