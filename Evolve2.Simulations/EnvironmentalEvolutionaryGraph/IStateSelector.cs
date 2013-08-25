using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolve2.Simulations.EnvironmentalEvolutionaryGraph
{
    public interface IStateSelector
    {
        IEnumerable<TIdentity> Select<TIdentity, TEnvironment, TIndividual>(Graph<TIdentity> G, Random Random, double MismatchedFitness)
            where TIdentity     : struct
            where TEnvironment  : struct
            where TIndividual   : struct;
    }
}
