using Convertor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GestionGraphTests
{
    [TestClass]
    public class GraphTests
    {
        private readonly string nodeNameOrigine = "Noeud1";
        private readonly string nodeNameDestination = "Noeud2";
        private readonly decimal edgeValue = 1.2m;

        private static IGraph GetGraf()
        {
            Graph graph = new();
            graph.CreateNode("EUR");
            graph.CreateNode("USD");
            graph.CreateNode("YEN");
            graph.CreateNode("AUD");
            graph.CreateNode("GBP");
            graph.CreateNode("DKK");
            graph.CreateNode("NOK");
            graph.CreateNode("BIC");

            graph.CreateEdge("EUR", "USD", 1.0567m);
            graph.CreateEdge("EUR", "YEN", 135.09m);
            graph.CreateEdge("EUR", "GBP", 0.8465m);
            graph.CreateEdge("GBP", "AUD", 1.7733m);
            graph.CreateEdge("AUD", "NOK", 6.8643m);
            graph.CreateEdge("DKK", "NOK", 1.3843m);
            graph.CreateEdge("YEN", "NOK", 0.0765m);

            return graph;
        }

        private static IGraph GetLuccaGraf()
        {
            Graph graph = new();
            graph.CreateNode("EUR");
            graph.CreateNode("USD");
            graph.CreateNode("KRW");
            graph.CreateNode("AUD");
            graph.CreateNode("INR");
            graph.CreateNode("JPY");
            graph.CreateNode("CHF");

            graph.CreateEdge("AUD", "CHF", 0.9661m);
            graph.CreateEdge("JPY", "KRW", 13.1151m);
            graph.CreateEdge("EUR", "CHF", 1.2053m);
            graph.CreateEdge("AUD", "JPY", 86.0305m);
            graph.CreateEdge("EUR", "USD", 1.2989m);
            graph.CreateEdge("YEN", "NOK", 0.6571m);

            return graph;
        }

        /// <summary>
        /// Vérifie que le chemin est vide lorsque les liens n'existent
        /// </summary>
        [TestMethod()]
        public void GetPathTestWithNoResult()
        {
            //arrange
            IGraph graph = GetGraf();
            var expectedValue = 0;

            //act
            Queue<IEdge> path = graph.GetShortestPath("BIC", "YEN");

            //assert
            Assert.AreEqual(expectedValue, path.Count);
        }

        [TestMethod()]
        public void GetPathTestWithTreeNodes()
        {
            //arrange
            IGraph graph = GetGraf();
            var expectedValue = 2;

            //act
            Queue<IEdge> path = graph.GetShortestPath("USD", "YEN");

            //assert
            Assert.AreEqual(expectedValue, path.Count);
        }

        [TestMethod()]
        public void GetPathTestWithFourNodes()
        {
            //arrange
            IGraph graph = GetGraf();
            var expectedValue = 3;

            //act
            Queue<IEdge> path = graph.GetShortestPath("GBP", "DKK");

            //assert
            Assert.AreEqual(expectedValue, path.Count);
        }

        [TestMethod()]
        public void GetValueWithLuccasGraph()
        {
            //arrange
            IGraph graph = GetLuccaGraf();
            var expectedValue = 59033;

            //act
            decimal resultValue = graph.GetValue("EUR", "JPY", 550);

            //assert
            Assert.AreEqual(expectedValue, resultValue);
        }

        [TestMethod]
        public void CreateNode_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IGraph graph = new Graph();
            string name = nodeNameOrigine;

            // Act
            graph.CreateNode(
                name);

            // Assert
            Assert.IsTrue(graph.Nodes.ContainsKey(name));
        }

        [TestMethod]
        public void CreateEdge_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IGraph graph = new Graph();
            string nodeName = nodeNameOrigine;
            string lookingNode = nodeNameDestination;
            decimal value = edgeValue;

            // Act
            graph.CreateEdge(
                nodeName,
                lookingNode,
                value);

            // Assert
            Assert.IsTrue(graph.Nodes[nodeName].ContainsEdge(lookingNode));
        }

        [TestMethod]
        public void GetValue_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            IGraph graph = new Graph();
            string nodeName = nodeNameOrigine;
            string lookingNode = nodeNameDestination;
            graph.CreateEdge(nodeName, lookingNode, edgeValue);

            var expectedValue = 5 * edgeValue;
            // Act
            var result = graph.GetValue(
                nodeName,
                lookingNode,
                5);

            // Assert
            Assert.AreEqual(result, expectedValue);
        }
    }
}
