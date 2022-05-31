namespace Convertor
{
    internal static class TestsUnitaires
    {
        public static void LaunchTest()
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
            graph.CreateNode("ZAR");

            graph.CreateEdge("EUR", "USD", 1.0567m);
            graph.CreateEdge("EUR", "YEN", 135.09m);
            graph.CreateEdge("EUR", "GBP", 0.8465m);
            graph.CreateEdge("GBP", "AUD", 1.7733m);
            graph.CreateEdge("AUD", "NOK", 6.8643m);
            graph.CreateEdge("DKK", "NOK", 1.3843m);
            graph.CreateEdge("YEN", "NOK", 0.0765m);
            graph.CreateEdge("USD", "ZAR", 15.4769m);

            foreach (var item in graph.Nodes)
            {
                Console.WriteLine($" Devise : {item.Key}");
                foreach (var edge in item.Value.Edges)
                {
                    Console.WriteLine($" > change : {edge.Value.DestinationNode.Name} : {edge.Value.Value}");
                }
            }

            Console.WriteLine($" -");
            Queue<IEdge> path;
            decimal initialValue = 3;

            path = graph.GetShortestPath("USD", "YEN");
            Console.WriteLine($" USD-YEN - hauteur de pile : {path.Count} - Init : {initialValue} - conversion : {graph.GetValue("USD", "YEN", initialValue)}");
            WriteResult(path);


            Console.WriteLine($" {Environment.NewLine} -");
            path = graph.GetShortestPath("USD", "NOK");
            Console.WriteLine($" USD-NOK - hauteur de pile : {path.Count} - Init : {initialValue} - conversion : {graph.GetValue("USD", "NOK", initialValue)}");
            WriteResult(path);

            Console.WriteLine($" {Environment.NewLine} -");
            path = graph.GetShortestPath("USD", "DKK");
            Console.WriteLine($" USD-DKK - hauteur de pile : {path.Count} - Init : {initialValue} - conversion : {graph.GetValue("USD", "DKK", initialValue)}");
            WriteResult(path);

            Console.WriteLine($" {Environment.NewLine} -");
            path = graph.GetShortestPath("AUD", "EUR");
            Console.WriteLine($" AUD-EUR - hauteur de pile : {path.Count} - Init : {initialValue} - conversion : {graph.GetValue("AUD", "EUR", initialValue)}");
            WriteResult(path);

            Console.WriteLine($" {Environment.NewLine} -");
            path = graph.GetShortestPath("GBP", "DKK");
            Console.WriteLine($" GBP-DKK - hauteur de pile : {path.Count}- Init : {initialValue} - conversion : {graph.GetValue("GBP", "DKK", initialValue)}");
            WriteResult(path);

            Console.WriteLine($" {Environment.NewLine} -");
            path = graph.GetShortestPath("BIC", "DKK");
            Console.WriteLine($" BIC-DKK - hauteur de pile : {path.Count} - Init : {initialValue} - conversion : {graph.GetValue("BIC", "DKK", initialValue)}");
            WriteResult(path);

            Console.WriteLine($" {Environment.NewLine} -");
            path = graph.GetShortestPath("BOC", "DKK");
            Console.WriteLine($" BOC-DKK - hauteur de pile : {path.Count} - Init : {initialValue} - conversion : {graph.GetValue("BOC", "DKK", initialValue)}");
            WriteResult(path);

        }

        private static void WriteResult(Queue<IEdge> path)
        {
            foreach (var item in path)
            {
                Console.Write($" > {item.DestinationNode.Name}");
            }
        }
    }
}
