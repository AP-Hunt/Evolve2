using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public interface IVictimSelector
    {
        T Select<T>(IEnumerable<T> Vertices, Graph<T> G) where T : struct;
    }
}
