using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.Util;
using Evolve2.Util.Factories;

namespace Evolve2.GraphTypeHelpers
{
    public class GraphTypeHelper<T>
        where T : struct
    {
        protected IVertexFactory<T> VertexFactory { get; private set; }
        protected IEdgeFactory<T> EdgeFactory { get; private set; }
        protected IIdentityProvider<T> IdentityProvider { get; private set; }

        public GraphTypeHelper(IVertexFactory<T> VertexFactory, IEdgeFactory<T> EdgeFactory, IIdentityProvider<T> IdentityProvider)
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
