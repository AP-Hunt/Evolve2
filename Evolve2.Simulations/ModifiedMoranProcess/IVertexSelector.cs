using Evolve2.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Simulations.ModifiedMoranProcess
{
    public interface IVertexSelector
    {
        T Select<T>(IEnumerable<T> Vertices, Graph<T> G, Random Random) where T : struct;
    }
}
