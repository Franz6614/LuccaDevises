namespace Convertor
{
    public class Edge : IEdge
    {
        /// <summary>
        /// pour l'arrondi : nombre de chiffres après la virgule 
        /// </summary>
        private const int Precision = 4;
         
        public INode DestinationNode { get; set; }

        public decimal Value { get; set; }

        public Edge(INode destinationNode, decimal value)
        {
            DestinationNode = destinationNode;
            Value = decimal.Round(value, Precision);
        }
    }
}
