using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2.Util
{
    public abstract class IdentityProvider<T> 
        where T : struct 
    {
        public virtual T NewIdentity();
        public virtual bool Equals(T a, T b);
    }
}
