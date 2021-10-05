using System.Linq;
using System.Text.RegularExpressions;

namespace KMChartsUpdater.BLL.Utils
{
    public class Normalizer
    {
        public static string ArtistNormalize(string artist)
        {
            string normalized = artist
                .ToLower()
                .Replace("ё", "е")
                .Replace(",", " ")
                .Replace("&", " ")
                .Replace("feat.", " ")
                .Replace("feat", " ");

            Regex regex = new Regex("[ ]{2,}", RegexOptions.None);
            normalized = regex.Replace(normalized, " ");

            return normalized;
        }

        public static string TitleNormalize(string title)
        {
            string normalized = title
                .ToLower()
                .Replace("ё", "е");

            normalized = Regex
                .Replace(normalized, @"(?:\(|\[)\b(?:prod\.?|prod\.? by|feat\.?|при уч\.?) \b.*?(?:\]|\))", "")
                .Trim();

            return normalized;
        }

        public static bool ArtistEqual(string artist1, string artist2)
        {
            if (artist1 == null || artist2 == null)
                return false;

            string[] artists1 = artist1.Split(' ');
            string[] artists2 = artist2.Split(' ');

            return artists1.SequenceEqual(artists2);
        }
    }
}
