using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    /// <summary>
    /// Forms the base for all graph elements (graphs, edges, vertices) and provides an identity
    /// </summary>
    /// <typeparam name="TIdentity"></typeparam>
    public abstract class GraphElement<TIdentity> where TIdentity : struct
    {
        protected TIdentity _ident;
        protected Util.IIdentityProvider<TIdentity> _identProvider;
        public TIdentity Identity
        {
            get
            {
                return _ident;
            }
            protected set
            {
                _ident = value;
            }
        }

        public GraphElement(Util.IIdentityProvider<TIdentity> IdentityProvider)
        {
            _ident = IdentityProvider.NewIdentity();
            _identProvider = IdentityProvider;
        }
    }
}
