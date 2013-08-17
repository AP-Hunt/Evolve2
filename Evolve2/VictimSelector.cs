using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class VictimSelector : IVictimSelector
    {
        public T Select<T>(IEnumerable<T> Vertices, Graph<T> G, Random Random) where T : struct
        {
            int N = Vertices.Count();
            int i = Random.Next(0, N - 1);
            return Vertices.ElementAt(i); 
        }
    }
}
