using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Simulations.EnvironmentalEvolutionaryGraph
{
    public class StateSelector : IStateSelector
    {
        public IEnumerable<TIdentity> Select<TIdentity, TEnvironment, TIndividual>(Graph<TIdentity> graph, Random Random, double MismatchedFitness)
            where TIdentity     : struct
            where TEnvironment  : struct
            where TIndividual   : struct
        {
            /**
             * Process
             * --------
             * [0] => R is mismatched fitness
             * [1] => Calculate prMismatch = (R*m)/((R*m)+(N-m)) as the probability of selecting a mutant node,
             *        where m is the number of vertices whose foreground and background are mismatched
             * [2] => Generate a random number Pr
             * [3] => Select mismatched node set when Pr <= prMutant
             *      [3.1] => Otherwise choose matched node set
             */
            double R = MismatchedFitness;
            int N = graph.Vertices.Count();
            int m = graph.Vertices.OfType<EnvironmentalVertex<TIdentity, TEnvironment, TIndividual>>().Count(v => !v.InMatchingEnvironment);
            double prMutant = ((R*m)/((R*m)+(N-m)));
            double pr = Random.NextDouble();

            if (pr <= prMutant)
            {
                return graph.Vertices.OfType<EnvironmentalVertex<TIdentity, TEnvironment, TIndividual>>().Where(v => !v.InMatchingEnvironment).Select(v => v.Identity);
            }
            else
            {
                return graph.Vertices.OfType<EnvironmentalVertex<TIdentity, TEnvironment, TIndividual>>().Where(v => v.InMatchingEnvironment).Select(v => v.Identity);
            }
        }
    }
}
