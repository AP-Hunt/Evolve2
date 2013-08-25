using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Simulations.EnvironmentalEvolutionaryGraph
{
    public class VertexSelector : IVertexSelector
    {
        public TIdentity Select<TIdentity, TEnvironment, TIndividual>(IEnumerable<TIdentity> Vertices, Graph<TIdentity> G, Random Random)
            where TIdentity : struct
            where TEnvironment : struct
            where TIndividual : struct
        {
            int N = Vertices.Count();
            int i = Random.Next(0, N);
            return Vertices.ElementAt(i);
        }
    }
}
