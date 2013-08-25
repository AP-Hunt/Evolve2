using System;
using System.Collections.Generic;
namespace Evolve2.State
{
    public interface IState<T> : IComparable<IState<T>>, IComparer<IState<T>>, IEquatable<IState<T>>, ICloneable
        where T : struct
    {
        T CurrentState { get; }
        bool ChangeStateValue(T NewStateValue);
        bool StateChangeIsValid(T NewStateValue);
    }
}
