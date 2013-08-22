using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Simulations.ModifiedMoranProcess
{
    public class VictimSelector : IVictimSelector
    {
        public T Select<T>(IEnumerable<T> Vertices, Graph<T> G, Random Random) where T : struct
        {
            var vertList = Vertices.ToList(); //Force evaluation on the list, so we're not reevaluating later
            int N = vertList.Count();
            int i = Random.Next(0, N);
            return vertList[i];
        }
    }
}
