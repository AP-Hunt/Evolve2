using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Evolve2;
using Evolve2.Util;
using Evolve2.Util.Factories;
using Evolve2.State;
using Evolve2.GraphTypeHelpers;
using Evolve2.Simulations.ModifiedMoranProcess;

namespace Evolve2.Tests.ModifiedMoranProcess
{
    /// <summary>
    /// A series of tests verifying that the modified moran process gets the expected results
    /// </summary>
    [TestClass]
    public class VerificationTests
    {
        private MoranProcessRunner runner;
        private int Repetitions = 2000;
        private int Iterations = 10000;

        [TestInitialize]
        public void Setup()
        {
            runner = new MoranProcessRunner(StateSelector: new StateSelector(),
                                            VertexSelector: new VertexSelector(),
                                            VictimSelector: new VictimSelector());
        }

        [TestMethod]
        public void Verify_Chain_MutantAtStart_p1Fixation()
        {
            ChainInfo<Guid> chainInfo = (new Chain<Guid>(new StatefulVertexFactory<VertexState>(new EnumState(VertexState.HEALTHY)), 
                                                         new DefaultEdgeFactory(), 
                                                         new DefaultIdentityProvider()))
                                        .Create(50, true);
            ((StatefulVertex<Guid, VertexState>)chainInfo.StartOfChain.Vertex).State.ChangeStateValue(VertexState.MUTANT);

            MoranProcessResult result = runner.RunOn(chainInfo.Graph, Repetitions, Iterations, 3.0d);
            Assert.AreEqual(result.FixationProbability, 1.0d);
            Assert.AreEqual(result.ExtinctionProbability, 0.0d);
        }

        [TestMethod]
        public void Verify_Chain_MutantAtSecond_p0Fixation()
        {
            ChainInfo<Guid> chainInfo = (new Chain<Guid>(new StatefulVertexFactory<VertexState>(new EnumState(VertexState.HEALTHY)),
                                                         new DefaultEdgeFactory(),
                                                         new DefaultIdentityProvider()))
                                        .Create(50, true);
            ((StatefulVertex<Guid, VertexState>)chainInfo.StartOfChain.NextVertex.Vertex).State.ChangeStateValue(VertexState.MUTANT);

            MoranProcessResult result = runner.RunOn(chainInfo.Graph, Repetitions, Iterations, 3.0d);
            Assert.AreEqual(result.FixationProbability, 0.0d);
            Assert.AreEqual(result.ExtinctionProbability, 1.0d);
        }

        [TestMethod]
        public void Verify_Clique_MutantAtAny_Fitness3_p0_6ishFixation()
        {
            CliqueInfo<Guid> cliqueInfo = (new Clique<Guid>(new StatefulVertexFactory<VertexState>(new EnumState(VertexState.HEALTHY)),
                                                         new DefaultEdgeFactory(),
                                                         new DefaultIdentityProvider()))
                                         .Create(50);
            cliqueInfo.Graph.Vertices.OfType<StatefulVertex<Guid, VertexState>>()
                                     .First()
                                        .State.ChangeStateValue(VertexState.MUTANT);

            MoranProcessResult result = runner.RunOn(cliqueInfo.Graph, Repetitions, Iterations, 3.0d);

            //Probabilities are allowed to be within 2%
            //This is a tradeoff for having shorter running tests
            Assert.IsTrue((result.FixationProbability >= 0.65) && (result.FixationProbability <= 0.67));
            Assert.IsTrue((result.ExtinctionProbability >= 0.32) && (result.ExtinctionProbability <= 0.34));
            
        }

        [TestMethod]
        public void Verify_Clique_MutantAtAny_Fitness5_p0_8ishFixation()
        {
            CliqueInfo<Guid> cliqueInfo = (new Clique<Guid>(new StatefulVertexFactory<VertexState>(new EnumState(VertexState.HEALTHY)),
                                                         new DefaultEdgeFactory(),
                                                         new DefaultIdentityProvider()))
                                         .Create(50);
            cliqueInfo.Graph.Vertices.OfType<StatefulVertex<Guid, VertexState>>()
                                     .First()
                                        .State.ChangeStateValue(VertexState.MUTANT);

            MoranProcessResult result = runner.RunOn(cliqueInfo.Graph, Repetitions, Iterations, 5.0d);

            //Probabilities are allowed to be within 2%
            //This is a tradeoff for having shorter running tests
            Assert.IsTrue((result.FixationProbability >= 0.79) && (result.FixationProbability <= 0.81));
            Assert.IsTrue((result.ExtinctionProbability >= 0.19) && (result.ExtinctionProbability <= 0.21));

        }
    }
}
