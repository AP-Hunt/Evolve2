using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Simulations.ModifiedMoranProcess
{
    public class StateSelector : IStateSelector
    {
        public IEnumerable<T> Select<T>(Graph<T> graph, Random Random) 
            where T : struct
        {
            /**
             * Process
             * --------
             * [0] => R is mutant fitness
             * [1] => Calculate prMutant = (R*m)/((R*m)+(N-m)) as the probability of selecting a mutant node
             * [2] => Generate a random number Pr
             * [3] => Select mutant node set when Pr <= prMutant
             *      [3.1] => Otherwise choose healthy node set
             */
            double R = 3.0d;
            int N = graph.Vertices.Count();
            int m = graph.Vertices.OfType<StatefulVertex<Guid, VertexState>>().Count(v => v.State.CurrentState == VertexState.MUTANT);
            double prMutant = ((R*m)/((R*m)+(N-m)));
            double pr = Random.NextDouble();

            if (pr <= prMutant)
            {
                return graph.Vertices.OfType<StatefulVertex<T, VertexState>>().Where(v => v.State.CurrentState == VertexState.MUTANT).Select(v => v.Identity);
            }
            else
            {
                return graph.Vertices.OfType<StatefulVertex<T, VertexState>>().Where(v => v.State.CurrentState == VertexState.HEALTHY).Select(v => v.Identity);
            }
        }
    }
}
