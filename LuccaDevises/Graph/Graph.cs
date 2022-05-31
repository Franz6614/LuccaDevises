namespace Convertor
{
    public class Graph : IGraph
    {
        /// <summary>
        /// Référentiel des noeuds composants le graphe
        /// </summary>
        public Dictionary<string, INode> Nodes { get; set; }

        public Graph()
        {
            Nodes = new Dictionary<string, INode>();
        }

        /// <summary>
        /// Crée un noeud dans le référentiel
        /// </summary>
        /// <param name="name"></param>
        public void CreateNode(string name)
        {
            if (TestExistenceOfNodes(name))
            {
                return;
            }
            Nodes.Add(name, new Node(name));
        }

        /// <summary>
        /// Crée un lien entre noeuds dans le référentiel
        /// Crée les noeuds designés quand ils n'existent pas
        /// </summary>
        /// <param name="nodeOrigine">Noeud d'origine</param>
        /// <param name="nodeDestination">Noeud de destination</param>
        /// <param name="value"></param>
        public void CreateEdge(string nodeOrigine, string nodeDestination, decimal value)
        {
            if (nodeOrigine == nodeDestination)
            {
                return;
            }
            if (!TestExistenceOfNodes(nodeOrigine))
            {
                Nodes.Add(nodeOrigine, new Node(nodeOrigine));
            }
            if (!TestExistenceOfNodes(nodeDestination))
            {
                Nodes.Add(nodeDestination, new Node(nodeDestination));
            }
            Nodes[nodeOrigine].CreateEdge(Nodes[nodeDestination], value);
            Nodes[nodeDestination].CreateEdge(Nodes[nodeOrigine], 1 / value);
        }

        /// <summary>
        /// Récupère une queue composée du chemin le plus court pour aller d'un noeud à un autre
        /// </summary>
        /// <param name="nodeOrigine">Noeud d'origine</param>
        /// <param name="nodeDestination">Noeud de destination</param>
        /// <returns></returns>
        public Queue<IEdge> GetShortestPath(string nodeOrigine, string nodeDestination)
        {
            Queue<IEdge> queue = new();
            if (!TestExistenceOfNodes(nodeOrigine))
            {
                return queue;
            }
            IList<string> visitedNodes = new List<string> { nodeOrigine };

            queue = Nodes[nodeOrigine].GetPath(queue, nodeDestination, visitedNodes);

            return queue;
        }

        /// <summary>
        /// Convertit la valeur d'origine dans la valeur de destination
        /// </summary>
        /// <param name="nodeOrigine">Noeud d'origine</param>
        /// <param name="nodeDestination">Noeud de destination</param>
        /// <param name="initialValue">Valeur d'origine</param>
        /// <returns></returns>
        public decimal GetValue(string nodeOrigine, string nodeDestination, decimal initialValue)
        {
            if (!TestExistenceOfNodes(nodeOrigine))
            {
                return 0m;
            }
            Queue<IEdge> queue = GetShortestPath(nodeOrigine, nodeDestination);
            if (queue.Count == 0)
            {
                return 0m;
            }

            var value = initialValue;
            foreach (var edge in queue)
            {
                value *= edge.Value;
            }
            return decimal.Round(value);
        }

        private bool TestExistenceOfNodes(string node) => Nodes.ContainsKey(node);

    }
}
