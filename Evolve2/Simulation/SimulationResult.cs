﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolve2.Simulation
{
    public class SimulationResult
    {
        public int RepetitionsPerformed { get; set; }

        public int Fixations { get; set; }
        public int Extinctions { get; set; }
        public int Timeout { get; set; }

        public double FixationProbability
        {
            get
            {
                return ((double)Fixations / (double)RepetitionsPerformed);
            }
        }

        public double ExtinctionProbability
        {
            get
            {
                return ((double)Extinctions / (double)RepetitionsPerformed);
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
