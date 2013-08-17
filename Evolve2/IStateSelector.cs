using Evolve2.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public interface IStateSelector
    {
        IEnumerable<T> Select<T>(Graph<T> graph, Random Random) where T : struct;
    }
}
