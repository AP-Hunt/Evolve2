using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evolve2
{
    public class Vertex<T> : ICloneable
        where T : struct
    {
        private T _ident;
        private Util.IdentityProvider<T> _identProvider;
        public T Identity
        {
            get
            {
                return _ident;
            }
            internal set
            {
                _ident = value;
            }
        }

        public State State { get; private set; }

        public Vertex(Util.IdentityProvider<T> IdentityProvider)
            : this(IdentityProvider, State.HEALTHY){}

        public Vertex(Util.IdentityProvider<T> IdentityProvider, State state)
        {
            this._ident = IdentityProvider.NewIdentity();
            this.State = state;
        }

        public void SwitchState(State NewState)
        {
            this.State = NewState;
        }

        public object Clone()
        {
            Vertex v = new Vertex(this.State);
            v.Identity = this.Identity;

            return v;
        }
    }
}
