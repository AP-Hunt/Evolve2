using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.Util.Factories;
using Evolve2.State;

namespace Evolve2.Simulations.ModifiedMoranProcess
{
    public class StatefulVertexFactory<TState> : StatefulVertexFactory<Guid, TState>
        where TState : struct
    {
        public StatefulVertexFactory(IState<TState> DefaultState)
            : base(DefaultState)
        { }
    }

    public class StatefulVertexFactory<T, TState> : IVertexFactory<T>
        where T : struct
        where TState : struct
    {
        private IState<TState> _defaultState;
        public StatefulVertexFactory(IState<TState> DefaultState)
        {
            _defaultState = DefaultState;
        }

        public Vertex<T> NewVertex(Util.IIdentityProvider<T> IdentityProvider)
        {
            return new StatefulVertex<T, TState>(IdentityProvider, (IState<TState>)_defaultState.Clone());
        }
    }
}
