using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Util.Factories
{
    public class DefaultEdgeFactory : IEdgeFactory<Guid>
    {
        public Edge<Guid> NewEdge(Vertex<Guid> Source, Vertex<Guid> Destination, IIdentityProvider<Guid> IdentityProvider)
        {
            return new Edge<Guid>(Source, Destination, IdentityProvider);
        }
    }
}
