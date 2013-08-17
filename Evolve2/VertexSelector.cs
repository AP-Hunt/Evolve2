using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class VertexSelector : IVertexSelector
    {
        public T Select<T>(IEnumerable<T> Vertices, Graph<T> G, Random Random) where T : struct
        {
            int N = Vertices.Count();
            int i = Random.Next(0, N);
            return Vertices.ElementAt(i);
        }
    }
}
