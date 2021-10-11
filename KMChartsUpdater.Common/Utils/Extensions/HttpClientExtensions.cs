using System.Net.Http;

namespace KMChartsUpdater.Common.Utils.Extensions
{
    public static class HttpClientExtensions
    {
        public static void AddHeader(this HttpClient client, string name, string value)
        {
            client.DefaultRequestHeaders.TryAddWithoutValidation(name, value);
        }
    }
}
