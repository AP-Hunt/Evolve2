using Evolve2.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Simulations.ModifiedMoranProcess
{
    public interface IStateSelector
    {
        IEnumerable<TIdent> Select<TIdent>(Graph<TIdent> graph, Random Random, double MutantFitness) where TIdent : struct;
    }
}
