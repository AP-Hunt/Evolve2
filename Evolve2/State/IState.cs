using System;
namespace Evolve2.State
{
    public interface IState<T> : IComparable<IState<T>>
    {
        T CurrentState { get; }
        bool ChangeStateValue(T NewStateValue);
        bool StateChangeIsValid(T NewStateValue);
    }
}
