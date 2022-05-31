namespace Convertor
{
    public interface IGraph
    {
        Dictionary<string, INode> Nodes { get; set; }
        void CreateEdge(string nodeOrigine, string nodeDestination, decimal value);
        void CreateNode(string name);
        Queue<IEdge> GetShortestPath(string nodeOrigine, string nodeDestination);
        decimal GetValue(string nodeOrigine, string nodeDestination, decimal initialValue);
    }
}