using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolve2.Simulations.EnvironmentalEvolutionaryGraph
{
    public class EEGResult
    {
        public int RepetitionsPerformed { get; set; }
        public int Fixations { get; set; }
        public int Timeout { get; set; }

        public double FixationProbability
        {
            get
            {
                return ((double)Fixations / (double)RepetitionsPerformed);
            }
        }

        public double TimeoutProbability
        {
            get
            {
                return ((double)Timeout / (double)RepetitionsPerformed);
            }
        }
    }
}
