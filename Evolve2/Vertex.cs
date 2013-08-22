using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class Vertex<T> : GraphElement<T>, ICloneable
        where T : struct
    {
        public Vertex(Util.IIdentityProvider<T> IdentityProvider) : base (IdentityProvider)
        { }

        public virtual object Clone()
        {
            Vertex<T> v = new Vertex<T>(_identProvider);
            v.Identity = this.Identity;

            return v;
        }
    }
}
