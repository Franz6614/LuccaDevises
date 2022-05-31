using System.Globalization;
namespace Convertor.CurrencyConvertor
{
    public class ConvertUtils
    {
        /// <summary>
        /// index de la première ligne
        /// </summary>
        public const int FirstLine = 0;

        /// <summary>
        /// index de la seconde ligne
        /// </summary>
        public const int SecundLine = 1;

        /// <summary>
        /// index de la 3eme ligne
        /// </summary>
        public const int ThirdLine = 2;

        /// <summary>
        /// nombre minimum de lignes dans le fichier
        /// </summary>
        public const int MinimumNumberLines = 3;

        /// <summary>
        /// Caractère séparateur dans chaque ligne
        /// </summary>
        public const char Separator = ';';

        /// <summary>
        /// Valeur erreur 
        /// </summary>
        public const int ErrorValue = -999;

        /// <summary>
        /// Nombre fixe d'informations par ligne, séparées par ';'
        /// </summary>
        public const int NumberOfInformationsPerLine = 3;

        public static readonly NumberStyles Style = NumberStyles.AllowDecimalPoint;
        public static readonly CultureInfo Culture = CultureInfo.CreateSpecificCulture("en-US");

        /// <summary>
        /// Valide le contenu du fichier
        /// </summary>
        /// <param name="content">Liste des lignes du fichier</param>
        /// <returns>true si l'ensemble des ligne est conforme à l'attendu</returns>
        public static bool ValideFileContent(List<string> content)
        {
            if (content == null)
            {
                return false;
            }
            if (content.Count < MinimumNumberLines)
            {
                return false;
            }
            for (int i = 0; i < content.Count; i++)
            {
                if (!ValidateLineContent(content[i], i))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Valide le format de la ligne :
        ///  un nombre entier si c'est la deuxième ligne
        ///  et 2 caractères ';' pour la première ligne et les lignes 3 et + 
        /// </summary>
        /// <param name="line">Numéro de ligne</param>
        /// <param name="number">numéro de ligne</param>
        /// <returns></returns>
        private static bool ValidateLineContent(string line, int number)
        {
            if (number == SecundLine)
            {
                return int.TryParse(line, out _);
            }
            else
            {
                return line.Count(caract => caract == Separator) == 2;
            }
        }

        /// <summary>
        /// Split la première ligne et retourne les éléments utiles
        /// </summary>
        /// <param name="line">ligne au format AAA;999;BBB</param>
        /// <param name="nodeOrigine">Devise d origine AAA</param>
        /// <param name="value">Nombre à convertir</param>
        /// <param name="nodeDestination">Devise de destination BBB</param>
        public static void GetNodesAndValue(string line, out string nodeOrigine, out decimal value, out string nodeDestination)
        {
            string[] nodesAndValue = line.Trim().Split(Separator);
            if (nodesAndValue.Length != NumberOfInformationsPerLine
                || !decimal.TryParse(nodesAndValue[1], Style, Culture, out value))
            {
                nodeOrigine = nodeDestination = string.Empty;
                value = 0;
            }
            else
            {
                nodeOrigine = nodesAndValue[0];
                nodeDestination = nodesAndValue[2];
            }
        }

        /// <summary>
        /// Split la 3ème (ou plus) ligne pour en retourner les éléments utiles
        /// </summary>
        /// <param name="line">ligne au format AAA;BBB;9.99</param>
        /// <param name="nodeOrigine">Devise d origine AAA</param>
        /// <param name="nodeDestination">Devise de destination BBB</param>
        /// <param name="value">Taux de conversion</param>
        public static void GetNodesAndValue(string line, out string nodeOrigine, out string nodeDestination, out decimal value)
        {
            string[] nodesAndValue = line.Trim().Split(Separator);

            if (nodesAndValue.Length != NumberOfInformationsPerLine
                || !decimal.TryParse(nodesAndValue[2], Style, Culture, out value))
            {
                nodeOrigine = nodeDestination = string.Empty;
                value = 0;
            }
            else
            {
                nodeOrigine = nodesAndValue[0];
                nodeDestination = nodesAndValue[1];
            }
        }

        /// <summary>
        /// Récupère le nombre de lignes ayant un taux de conversion entre deux devises
        /// </summary>
        /// <param name="content">Liste des lignes contenues dans le fichier</param>
        /// <returns>Nombre de lignes ou ErrorValue si le nombre attendu diffère du nombre effectif</returns>
        public static int GetNumberOfLinesWithConversionRate(List<string> content)
        {
            if (content == null || content.Count < MinimumNumberLines)
            {
                return ErrorValue;
            }

            if (!int.TryParse(content[SecundLine], out int result))
            {
                return ErrorValue;
            }

            if (result + ThirdLine != content.Count)
            {
                return ErrorValue;
            }

            return result;
        }
    }
}
