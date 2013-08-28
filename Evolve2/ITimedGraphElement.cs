using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.Util;

namespace Evolve2
{
    public interface ITimedGraphElement<TIdentity>
        where TIdentity : struct
    {
        bool IsPresentAt(Graph<TIdentity> Graph, int Time);
    }
}
