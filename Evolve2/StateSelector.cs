﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class StateSelector : IStateSelector
    {
        public IEnumerable<Guid> Select(Graph graph, RandomProvider RandomProvider)
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
            double R = 1.0d;
            int N = graph.Vertices.Count();
            int m = graph.Vertices.Count(v => v.State == State.MUTANT);
            double prMutant = ((R*m)/((R*m)+(N-m)));
            double pr = RandomProvider.Random.NextDouble();

            if (pr <= prMutant)
            {
                return graph.Vertices.Where(v => v.State == State.MUTANT).Select(v => v.Identity);
            }
            else
            {
                return graph.Vertices.Where(v => v.State == State.HEALTHY).Select(v => v.Identity);
            }
        }
    }
}