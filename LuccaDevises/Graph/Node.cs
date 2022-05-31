namespace Convertor
{
    public class Node : INode
    {
        /// <summary>
        /// Référentiel des liens depuis le noeud
        /// </summary>
        public IDictionary<string, IEdge> Edges { get; set; } = new Dictionary<string, IEdge>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public Node(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Nom du noeud
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Ajoute un lien vers un autre noeud
        /// </summary>
        /// <param name="node">Autre noeud</param>
        /// <param name="value">valeur associée</param>
        public void CreateEdge(INode node, decimal value)
        {
            if (node == null)
            {
                return;
            }
            if (!ContainsEdge(node.Name))
            {
                Edges.Add(node.Name, new Edge(node, value));
            }
        }

        /// <summary>
        /// Verifie l'existence du lien entre le noeud courant et le noeud nommé en paramètre
        /// </summary>
        /// <param name="name">Nom du noeud destinataire du lien</param>
        /// <returns>True si le lien existe</returns>
        public bool ContainsEdge(string name) => Edges.ContainsKey(name);

        /// <summary>
        /// Crée une clone de la queue et ajoute le lien en cours
        /// </summary>
        /// <param name="currentPath">Queue à cloner</param>
        /// <param name="edge">lien à ajouter</param>
        /// <returns>Queue</returns>
        private static Queue<IEdge> CreateQueue(Queue<IEdge> currentPath, IEdge edge)
        {
            var queue = new Queue<IEdge>(currentPath);
            queue.Enqueue(edge);
            return queue;
        }

        /// <summary>
        /// Crée un clone de la liste des noeuds déjà visités
        /// </summary>
        /// <param name="visited"></param>
        /// <returns>nouvelle liste</returns>
        private static IList<string> CreateVisitedList(IList<string> visited)
        {
            var newList = new List<string>(visited);
            return newList;
        }

        /// <summary>
        /// Récupère le plus court chemin vers un noeud depuis le noeud courant
        /// </summary>
        /// <param name="currentPath">chemin courant</param>
        /// <param name="searchedName">nom du noeud recherché</param>
        /// <param name="visited">liste des noeuds déjà visités</param>
        /// <returns>Queue du plus court chemin</returns>
        public Queue<IEdge> GetPath(Queue<IEdge> currentPath, string searchedName, IList<string> visited)
        {
            Queue<IEdge> shortestPath = new();
            IList<string> localVisited = CreateVisitedList(visited);

            foreach (var item in Edges)
            {
                if (localVisited.Contains(item.Key))
                {
                    continue;
                }
                localVisited.Add(item.Value.DestinationNode.Name);

                Queue<IEdge> path = CreateQueue(currentPath, item.Value);
                if (item.Value.DestinationNode.Name == searchedName)
                {
                    return path;
                }
                Queue<IEdge> result = item.Value.DestinationNode.GetPath(path, searchedName, localVisited);
                if (shortestPath == null || shortestPath.Count == 0 || (result.Count != 0 && shortestPath.Count > result.Count))
                {
                    shortestPath = result;
                }
            }
            return shortestPath;
        }
    }
}
