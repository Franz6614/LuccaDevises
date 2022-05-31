namespace Convertor.CurrencyConvertor
{
    public class Convert
    {
        /// <summary>
        /// Charge le contenu du fichier 
        /// et effectue la conversion demandée et décrite en première ligne
        /// </summary>
        /// <param name="fileName">nom du fichier au format chemin\nomdefichier</param>
        /// <returns>Message informant sur l'erreur lors du chargement ou de la tentative de conversion
        ///  ou message donnant le résultat de la conversion décrite</returns>
        public static string Execute(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return $"Le fichier {fileName} n'existe pas";
            }

            List<string> content = GetContentFile(fileName);
            if (!ConvertUtils.ValideFileContent(content))
            {
                return $"Le contenu du fichier {fileName} n'est pas valide";
            }

            IGraph graph = CreateGraph(content);
            if (graph.Nodes.Count == 0)
            {
                return $"Format incorrect pour le fichier {fileName}";
            }

            ConvertUtils.GetNodesAndValue(content[ConvertUtils.FirstLine], out string nodeOrigine, out decimal Value, out string nodeDestination);

            decimal convertValue = graph.GetValue(nodeOrigine, nodeDestination, Value);

            return $"Conversion de {Value} {nodeOrigine} vers {nodeDestination} = {convertValue}";
        }

        /// <summary>
        /// Charge le fichier 
        /// </summary>
        /// <param name="fileName">chemin+nom du fichier</param>
        /// <returns>liste des lignes composant le fichier</returns>
        public static List<string> GetContentFile(string fileName)
        {
            string[] strings = File.ReadAllLines(fileName);
            return strings.Select(s => s.Trim()).ToList();
        }

        /// <summary>
        /// Création du graphe qui permettrta la conversion demandée
        /// </summary>
        /// <param name="content">Contenu du fichier chargé</param>
        /// <returns>Graphe contenant les noeuds et les liens entre noeuds</returns>
        public static IGraph CreateGraph(List<string> content)
        {
            Graph graph = new();
            int result = ConvertUtils.GetNumberOfLinesWithConversionRate(content);
            if (result == ConvertUtils.ErrorValue)
            {
                return graph;
            }

            for (int i = ConvertUtils.ThirdLine; i < ConvertUtils.ThirdLine + result; i++)
            {
                ConvertUtils.GetNodesAndValue(content[i], out string nodeOrigine, out string nodeDestination, out decimal Value);
                if (nodeOrigine != string.Empty && nodeDestination != string.Empty)
                {
                    graph.CreateEdge(nodeOrigine, nodeDestination, Value);
                }
            }

            return graph;
        }

    }
}
