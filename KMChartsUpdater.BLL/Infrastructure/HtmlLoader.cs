using System.Net.Http;
using System.Threading.Tasks;

namespace KMChartsUpdater.BLL.Infrastructure
{
    class HtmlLoader
    {
        private static readonly HttpClient _client;

        public string UserAgent
        {
            set
            {
                _client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", value);
            }
        }

        public string Authorization
        {
            set
            {
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", value);
            }
        }

        static HtmlLoader()
        {
            _client = new HttpClient();
        }

        public async Task<string> GetHtmlResponse(string url)
        {
            HttpResponseMessage httpResponse = await _client.GetAsync(url);

            string response = await httpResponse.Content.ReadAsStringAsync();

            return response;
        }

    }
}
