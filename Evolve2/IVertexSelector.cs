using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public interface IVertexSelector
    {
        Guid Select(IEnumerable<Guid> Vertices, Graph G, RandomProvider RandomProvider);
    }
}
