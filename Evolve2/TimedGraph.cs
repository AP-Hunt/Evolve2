using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Evolve2.Util;

namespace Evolve2
{
    public class TimedGraph<TIdentity> : Graph<TIdentity>
        where TIdentity : struct
    {
        private int _maxTimeStep;
        private int _currentTimeStep;
        private ICollection<TimedVertex<TIdentity>> _timedVertices;
        private Dictionary<int, ICollection<Vertex<TIdentity>>> _cachedTimeStepVertices;
        private IEnumerable<Vertex<TIdentity>> _computedVertexSet;

        public override IEnumerable<Vertex<TIdentity>> Vertices
        {
            get
            {
                return _computedVertexSet;
            }
        }

        public TimedGraph(IIdentityProvider<TIdentity> IdentityProvider, int MaxTimeStep)
            : base(IdentityProvider)
        {
            _maxTimeStep = MaxTimeStep;
            _currentTimeStep = 0;
            _cachedTimeStepVertices = new Dictionary<int,ICollection<Vertex<TIdentity>>>();
            _timedVertices = new List<TimedVertex<TIdentity>>();
        }

        public void Tick()
        {
            if (_currentTimeStep >= _maxTimeStep)
            {
                throw new TimeStepsExceededException();
            }

            Tick(_currentTimeStep++);
        }

        public void Tick(int NextStep)
        {
            if (NextStep > _maxTimeStep)
            {
                throw new ArgumentOutOfRangeException("NextStep", "NextStep cannot be greater than the maximum step value of the graph");
            }
            _currentTimeStep = NextStep;
        }

        public void SetMaxTimeSteps(int MaxSteps)
        {
            _maxTimeStep = MaxSteps;
        }

        public void AddTimedVertex(TimedVertex<TIdentity> Vertex)
        {
            if (Vertex == null)
            {
                throw new ArgumentNullException("Vertex");
            }

            _timedVertices.Add(Vertex);
            for (int i = 0; i <= _maxTimeStep; i++)
            {
                if (Vertex.IsPresentAt(this, i))
                {
                    addToTimedVerticesCache(i, Vertex);
                }
            }

            computeVertexSet(_currentTimeStep);
        }

        private void addToTimedVerticesCache(int T, TimedVertex<TIdentity> Vertex)
        {
            if (!_cachedTimeStepVertices.ContainsKey(T))
            {
                _cachedTimeStepVertices[T] = new List<Vertex<TIdentity>>();
            }

            _cachedTimeStepVertices[T].Add(Vertex);
        }

        private void computeVertexSet(int T)
        {
            IEnumerable<Vertex<TIdentity>> vertices = base.Vertices;
            vertices.Union(cachedVerticesAtTimeStep(T));

            _computedVertexSet = vertices;
        }

        private IEnumerable<Vertex<TIdentity>> cachedVerticesAtTimeStep(int T)
        {
            if (_cachedTimeStepVertices.ContainsKey(T))
            {
                return _cachedTimeStepVertices[T];
            }

            return new List<Vertex<TIdentity>>();
        }
    }
}
