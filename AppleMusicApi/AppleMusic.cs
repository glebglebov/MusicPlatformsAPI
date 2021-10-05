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
        private static readonly HttpClient _client;

        private string _apiUrl { get; set; } = "https://api.music.apple.com/v1/";

        public string AccessToken { get; set; }

        static AppleMusic()
        {
            _client = new HttpClient();
        }

        public AppleMusic()
        {

        }

        public void Auth(string token)
        {
            //var iat = Math.Round((DateTime.UtcNow.AddMinutes(-1) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds, 0);
            //var exp = Math.Round((DateTime.UtcNow.AddDays(180) - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds, 0);

            //var payload = new Dictionary<string, object>()
            //{
            //    { "iss", "DA4VB3TQAS" },
            //    { "iat", iat },
            //    { "exp", exp }
            //};

            //var extraHeader = new Dictionary<string, object>()
            //{
            //    { "alg", "ES256" },
            //    //{ "typ", "JWT" },
            //    { "kid", "PX855CNRP6" }
            //};

            //var keyString = "MIGTAgEAMBMGByqGSM49AgEGCCqGSM49AwEHBHkwdwIBAQQgPJ0PmfjIpW5subs9imAx7JJbfs9tFnaEwwWAPX2AlUigCgYIKoZIzj0DAQehRANCAAT39UbxzAT9wN6RYUCKeldWGILB1p1pGsnJk7Lwb2NeQvTAmj14Y5naNe+AZMBRHVHsCkIu/c5F4WOjfwiACNc2";

            //CngKey privateKey = CngKey.Import(Convert.FromBase64String(keyString), CngKeyBlobFormat.Pkcs8PrivateBlob);

            //string token1 = JWT.Encode(payload, privateKey, JwsAlgorithm.ES256, extraHeader);

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
