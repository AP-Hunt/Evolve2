using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Util.Factories
{
    public interface IEdgeFactory<T>
        where T : struct
    {
        Edge<T> NewEdge(Vertex<T> Source, Vertex<T> Destination, IIdentityProvider<T> IdentityProvider);
    }
}
