using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyApi
{
    public class Spotify
    {
        private static readonly HttpClient _client;

        public string ApiUrl { get; set; } = "https://api.spotify.com/v1/";
        public string AuthUrl { get; set; } = "https://accounts.spotify.com/api/token";

        public string Token { get; set; }

        static Spotify()
        {
            _client = new HttpClient();
        }

        public Spotify()
        {

        }

        public void Auth(string clientId, string clientSecret)
        {
            var client = Encoding.UTF8.GetBytes(clientId + ":" + clientSecret);
            string authorization = Convert.ToBase64String(client);

            Dictionary<string, string> parameters = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" },
                { "authorization", authorization }
            };

            Authorize(parameters);
        }

        public Paging<PlaylistTrack> GetPlaylistItems(string id, string market)
        {
            var parameters = new Dictionary<string, string>
            {
                { "market", market },
                { "offset", "0" },
                { "limit", "100" }
            };

            string method = "playlists/" + id + "/tracks";

            JObject json = GetResponse(method, parameters).Result;
            string j = json.ToString();

            return JsonConvert.DeserializeObject<Paging<PlaylistTrack>>(j);
        }

        public Playlist GetPlaylist(string id, string market)
        {
            var parameters = new Dictionary<string, string>
            {
                { "market", market }
            };

            string method = "playlists/" + id;

            JObject json = GetResponse(method, parameters).Result;
            string j = json.ToString();

            return JsonConvert.DeserializeObject<Playlist>(j);
        }

        public Album GetAlbum(string id, string market = "RU")
        {
            var parameters = new Dictionary<string, string>
            {
                { "market", market }
            };

            string method = "albums/" + id;

            JObject json = GetResponse(method, parameters).Result;
            string j = json.ToString();

            return JsonConvert.DeserializeObject<Album>(j);
        }

        public async Task<JObject> GetResponse(string method, Dictionary<string, string> @params)
        {
            string url = ApiUrl + method + $"?{string.Join("&", @params.Select(p => $"{p.Key}={p.Value}"))}";

            string auth = "Bearer " + Token;

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(url),
                Method = HttpMethod.Get
            };
            request.Headers.Add("Authorization", auth);

            var response = _client.SendAsync(request).Result;
            string raw = await response.Content.ReadAsStringAsync();

            //System.Diagnostics.Debug.WriteLine(raw);

            JObject json = JObject.Parse(raw);

            return json;
        }

        private async void Authorize(Dictionary<string, string> @params)
        {    
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", @params["grant_type"]),
            });

            string auth = "Basic " + @params["authorization"];

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(AuthUrl),
                Method = HttpMethod.Post,
                Content = formContent
            };
            request.Headers.Add("Authorization", auth);

            var response = _client.SendAsync(request).Result;
            string raw = await response.Content.ReadAsStringAsync();

            JObject json = JObject.Parse(raw);

            Token = json["access_token"]?.ToString();
        }
    }
}
