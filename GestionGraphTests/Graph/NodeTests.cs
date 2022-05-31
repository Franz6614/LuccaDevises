using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Convertor.Tests
{
    [TestClass()]
    public class NodeTests
    {
        private INode? node;

        private readonly string nodeNameOrigine = "Noeud1";
        private readonly string nodeNameDestination = "Noeud2";
        private readonly string fakeNodeName = "Noeud3";
        private readonly decimal value = 1.2m;

        /// <summary>
        /// vérifie la réalité de l'existence d'un lien 
        /// </summary>
        [TestMethod()]
        public void IsTrueContainsEdgeTest()
        {
            //arrange 
            node = new Node(nodeNameOrigine);
            node.Edges.Add(nodeNameDestination, new Edge(new Node(nodeNameDestination), value));

            //act
            var resultValue = node.ContainsEdge(nodeNameDestination);

            //asert
            Assert.IsTrue(resultValue);
        }

        /// <summary>
        /// vérifie l'absence d'un lien 
        /// </summary>
        [TestMethod()]
        public void IsFalseContainsEdgeTest()
        {
            //arrange 
            node = new Node(nodeNameOrigine);

            //act
            var resultValue = node.ContainsEdge(fakeNodeName);

            //asert
            Assert.IsFalse(resultValue);
        }

        /// <summary>
        /// vérifie l'efficacité de création d'un lien
        /// </summary>
        [TestMethod()]
        public void IsTrueCreateEdgeTest()
        {
            //arrange 
            node = new Node(nodeNameOrigine);

            //act
            node.CreateEdge(new Node(nodeNameDestination), value);

            //asert
            Assert.IsTrue(node.ContainsEdge(nodeNameDestination));
        }

        [TestMethod()]
        public void IsFalseCreateEdgeTest()
        {
            //arrange 
            node = new Node(nodeNameOrigine);

            //act
            node.CreateEdge(default, value);

            //asert
            Assert.IsFalse(node.ContainsEdge(fakeNodeName));
        }

    }
}