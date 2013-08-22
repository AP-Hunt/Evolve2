using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Util.Factories
{
    public class DefaultVertexFactory : IVertexFactory<Guid>
    {
        public Vertex<Guid> NewVertex(IIdentityProvider<Guid> IdentityProvider)
        {
            return new Vertex<Guid>(IdentityProvider);
        }
    }
}
