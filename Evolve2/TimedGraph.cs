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

        /// <summary>
        /// Set of vertices including all basic vertices, plus those timed vertices which appear at the current time step
        /// </summary>
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

        /// <summary>
        /// Move to the next time step
        /// </summary>
        public void Tick()
        {
            if (_currentTimeStep >= _maxTimeStep)
            {
                throw new TimeStepsExceededException();
            }

            Tick(_currentTimeStep++);
        }

        /// <summary>
        /// Move to the specified time step
        /// </summary>
        /// <param name="NextStep"></param>
        public void Tick(int NextStep)
        {
            if (NextStep > _maxTimeStep)
            {
                throw new ArgumentOutOfRangeException("NextStep", "NextStep cannot be greater than the maximum step value of the graph");
            }
            _currentTimeStep = NextStep;
            recomputeVertexSet(_currentTimeStep);
        }

        /// <summary>
        /// Set the largest possible time step value
        /// </summary>
        /// <param name="MaxSteps"></param>
        public void SetMaxTimeSteps(int MaxSteps)
        {
            _maxTimeStep = MaxSteps;
        }

        /// <summary>
        /// Add a timed vertex to the graph.
        /// </summary>
        /// <param name="Vertex"></param>
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

            recomputeVertexSet(_currentTimeStep);
        }

        /// <summary>
        /// Adds a timed vertex to the cache of the timed vertices
        /// </summary>
        /// <param name="T"></param>
        /// <param name="Vertex"></param>
        private void addToTimedVerticesCache(int T, TimedVertex<TIdentity> Vertex)
        {
            if (!_cachedTimeStepVertices.ContainsKey(T))
            {
                _cachedTimeStepVertices[T] = new List<Vertex<TIdentity>>();
            }

            _cachedTimeStepVertices[T].Add(Vertex);
        }

        /// <summary>
        /// Recomputes the vertex set for the given timestep
        /// </summary>
        /// <param name="T"></param>
        private void recomputeVertexSet(int T)
        {
            IEnumerable<Vertex<TIdentity>> vertices = base.Vertices;
            vertices.Union(cachedVerticesAtTimeStep(T));

            _computedVertexSet = vertices;
        }

        /// <summary>
        /// Gets the timed vertices which appear the given timestep
        /// </summary>
        /// <param name="T"></param>
        /// <returns></returns>
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
