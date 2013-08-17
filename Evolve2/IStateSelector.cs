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
        IEnumerable<Guid> Select(Graph graph, RandomProvider Random);
    }
}
