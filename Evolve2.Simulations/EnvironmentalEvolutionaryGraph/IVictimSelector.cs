using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolve2.Simulations.EnvironmentalEvolutionaryGraph
{
    public interface IVictimSelector
    {
        TIdentity Select<TIdentity, TEnvironment, TIndividual>(IEnumerable<TIdentity> Vertices, Graph<TIdentity> G, Random Random)
            where TIdentity : struct
            where TEnvironment : struct
            where TIndividual : struct;
    }
}
