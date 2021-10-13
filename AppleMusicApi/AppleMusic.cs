using AppleMusicApi.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AppleMusicApi
{
    public class AppleMusic
    {
        private readonly HttpClient _client;

        private string _apiUrl { get; set; } = "https://api.music.apple.com/v1/";

        public string AccessToken { get; set; }

        public AppleMusic(HttpClient client)
        {
            _client = client;
        }

        public void Auth(string token)
        {
            AccessToken = token;
        }

        public PlaylistsResponse GetCatalogPlaylist(string storefront, string id)
        {
            var parameters = new Dictionary<string, string>
            {
                
            };

            string method = "catalog/" + storefront + "/playlists/" + id;

            return Call<PlaylistsResponse>(method, parameters).Result;
        }

        public ChartResponse GetCatalogCharts(string storefront, string types, int limit)
        {
            var parameters = new Dictionary<string, string>
            {
                { "types", types },
                { "limit", limit.ToString() }
            };

            string method = "catalog/" + storefront + "/charts";

            return Call<ChartResponse>(method, parameters).Result;
        }

        public SearchResponse Search(string term, string types, string storefront, int limit)
        {
            var parameters = new Dictionary<string, string>
            {
                { "term", term },
                { "types", types },
                { "limit", limit.ToString() }
            };

            string method = "catalog/" + storefront + "/search";

            return Call<SearchResponse>(method, parameters).Result;
        }

        public async Task<T> Call<T>(string method, Dictionary<string, string> @params)
        {
            string url = _apiUrl + method + $"?{string.Join("&", @params.Select(p => $"{p.Key}={p.Value}"))}";

            string auth = "Bearer " + AccessToken;

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            request.Headers.Add("Authorization", auth);

            var response = _client.SendAsync(request).Result;
            string raw = await response.Content.ReadAsStringAsync();

            System.Diagnostics.Debug.WriteLine(raw);

            JObject json = JObject.Parse(raw);
            string j = json.ToString();

            return JsonConvert.DeserializeObject<T>(j);
        }
    }
}
