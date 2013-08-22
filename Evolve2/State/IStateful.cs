using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evolve2.State
{
    public interface IStateful<T>
        where T : struct
    {
        IState<T> State { get; }
    }
}
