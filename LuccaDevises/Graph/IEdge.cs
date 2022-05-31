namespace Convertor
{
    public interface IEdge
    {
        INode DestinationNode { get; set; }
        decimal Value { get; set; }
    }
}