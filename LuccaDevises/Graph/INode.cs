namespace Convertor
{
    public interface INode
    {
        IDictionary<string, IEdge> Edges { get; set; }

        string Name { get; set; }

        bool ContainsEdge(string name);

        void CreateEdge(INode node, decimal value);

        Queue<IEdge> GetPath(Queue<IEdge> currentPath, string searchedName, IList<string> visited);
    }
}