using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Simulations.EnvironmentalEvolutionaryGraph
{
    public class VictimSelector : IVictimSelector
    {
        public TIdentity Select<TIdentity, TEnvironment, TIndividual>(IEnumerable<TIdentity> Vertices, Graph<TIdentity> G, Random Random)
            where TIdentity : struct
            where TEnvironment : struct
            where TIndividual : struct
        {
            var vertList = Vertices.ToList(); //Force evaluation on the list, so we're not reevaluating later
            int N = vertList.Count();

            if (N == 0)
            {
                return default(TIdentity);
            }
            else
            {
                int i = Random.Next(0, N);
                return vertList[i];
            }
        }
    }
}
