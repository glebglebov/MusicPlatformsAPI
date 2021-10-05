using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkApi.Infrastructure;
using VkApi.Models;
using VkApi.Responses;

namespace VkApi
{
    public class VkAudioApi
    {
        private readonly HtmlLoader _loader;

        public VkApiSettings Settings { get; }

        public string AccessToken { get; set; }

        public VkAudioApi()
        {
            Settings = new VkApiSettings();

            _loader = new HtmlLoader
            {
                UserAgent = Settings.UserAgent
            };
        }

        public void Auth(string username, string password)
        {
            var parameters = new Dictionary<string, string>
            {
                { "grant_type", "password" },
                { "scope", "audio" },
                { "client_id", Settings.ClientId },
                { "client_secret", Settings.ClientSecret },
                { "validate_token", "true" },
                { "username", username },
                { "password", password }
            };

            Authorize(parameters);
        }

        public LikesGetListResponse LikesGetList(string type, string ownerId, string itemId,
            int friendsOnly = 0, int offset = 0, int count = 0)
        {
            var parameters = new Dictionary<string, string>
            {
                { "v", Settings.ApiVersion },
                { "type", type },
                { "owner_id", ownerId },
                { "item_id", itemId },
                { "friends_only", friendsOnly.ToString() },
                { "offset", offset.ToString() },
                { "count", count.ToString() },
                { "access_token", AccessToken }
            };

            return GetRepsonse<LikesGetListResponse>("likes.getList", parameters).Result;
        }

        public AudioCatalogAlbum GetPlaylistById(string ownerId, string playlistId, string accessKey = null)
        {
            var parameters = new Dictionary<string, string>
            {
                { "v", Settings.ApiVersion },
                { "owner_id", ownerId },
                { "playlist_id", playlistId },
                { "access_token", AccessToken }
            };

            return GetRepsonse<AudioCatalogAlbum>("audio.getPlaylistById", parameters).Result;
        }

        public AudioCatalogAlbum GetPlaylistById(string id)
        {
            string[] ids = id.Split('_');

            return GetPlaylistById(ids[0], ids[1]);
        }

        public GetSectionResponse GetChartSection(string sectionId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "v", Settings.ApiVersion },
                { "section_id", sectionId },
                { "access_token", AccessToken }
            };

            return GetRepsonse<GetSectionResponse>("catalog.getSection", parameters).Result;
        }

        public GetSectionPlaylistsResponse GetAlbumsChartSection(string sectionId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "v", Settings.ApiVersion },
                { "section_id", sectionId },
                { "access_token", AccessToken }
            };

            return GetRepsonse<GetSectionPlaylistsResponse>("catalog.getSection", parameters).Result;
        }

        public void GetBlockItems(string blockId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "v", Settings.ApiVersion },
                { "block_id", blockId },
                { "access_token", AccessToken }
            };

            JObject json = GetRepsonse("catalog.getBlockItems", parameters).Result;

            string j = json["response"].ToString();
        }

        public ExecuteGetPlaylistResponse ExecuteGetPlaylist(string id, string ownerId)
        {
            var parameters = new Dictionary<string, string>
            {
                { "v", Settings.ApiVersion },
                { "owner_id", ownerId },
                { "id", id },
                { "access_token", AccessToken }
            };

            return GetRepsonse<ExecuteGetPlaylistResponse>("execute.getPlaylist", parameters).Result;
        }

        public ExecuteGetPlaylistResponse ExecuteGetPlaylist(string id)
        {
            string[] ids = id.Split('_');

            return ExecuteGetPlaylist(ids[1], ids[0]);
        }

        public async Task<JObject> GetRepsonse(string method, Dictionary<string, string> @params)
        {
            string url = Settings.ApiUrl + method + $"?{string.Join("&", @params.Select(p => $"{p.Key}={p.Value}"))}";

            string response = await _loader.GetHtmlResponse(url);

            //System.Diagnostics.Debug.WriteLine(response);

            JObject json = JObject.Parse(response);

            return json;
        }

        public async Task<T> GetRepsonse<T>(string method, Dictionary<string, string> @params)
        {
            string url = Settings.ApiUrl + method + $"?{string.Join("&", @params.Select(p => $"{p.Key}={p.Value}"))}";

            string response = await _loader.GetHtmlResponse(url);

            System.Diagnostics.Debug.WriteLine(response);

            JObject json = JObject.Parse(response);
            string j = json["response"].ToString();

            return JsonConvert.DeserializeObject<T>(j);
        }

        private void Authorize(Dictionary<string, string> @params)
        {
            string url = Settings.AuthUrl + $"?{string.Join("&", @params.Select(p => $"{p.Key}={p.Value}"))}";

            string response = _loader.GetHtmlResponse(url).Result;
            JObject json = JObject.Parse(response);

            AccessToken = json["access_token"]?.ToString();
        }
    }
}
