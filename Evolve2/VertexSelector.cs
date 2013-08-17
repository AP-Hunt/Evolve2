using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class VertexSelector : IVertexSelector
    {
        public Guid Select(IEnumerable<Guid> Vertices, Graph G, RandomProvider RandomProvider)
        {
            int N = Vertices.Count();
            int i = RandomProvider.Random.Next(0, N - 1);
            return Vertices.ElementAt(i);
        }
    }
}
