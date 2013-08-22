using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Util.Factories
{
    public interface IVertexFactory<T>
        where T : struct
    {
        Vertex<T> NewVertex(IIdentityProvider<T> IdentityProvider);
    }
}
