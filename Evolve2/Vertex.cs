using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class Vertex<TIdentity> : GraphElement<TIdentity>, ICloneable
        where TIdentity : struct
    {
        public Vertex(Util.IIdentityProvider<TIdentity> IdentityProvider) : base (IdentityProvider)
        { }

        public virtual object Clone()
        {
            Vertex<TIdentity> v = new Vertex<TIdentity>(_identProvider);
            v.Identity = this.Identity;

            return v;
        }
    }
}
