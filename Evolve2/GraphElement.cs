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
    /// <typeparam name="TIdent"></typeparam>
    public abstract class GraphElement<TIdent> where TIdent : struct
    {
        protected TIdent _ident;
        protected Util.IIdentityProvider<TIdent> _identProvider;
        public TIdent Identity
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

        public GraphElement(Util.IIdentityProvider<TIdent> IdentityProvider)
        {
            _ident = IdentityProvider.NewIdentity();
            _identProvider = IdentityProvider;
        }
    }
}
