using System;
using KMChartsUpdater.BLL.Infrastructure;
using KMChartsUpdater.BLL.YandexMusicApi.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMChartsUpdater.BLL.YandexMusicApi
{
    public class YandexMusic
    {
        private const string _apiUrl = "https://music.yandex.ru/handlers/";

        private readonly HtmlLoader _loader;

        public YandexMusic()
        {
            _loader = new HtmlLoader();
        }

        public ChartResponse GetChart()
        {
            var parameters = new Dictionary<string, string>
            {
                { "what", "chart" },
                { "lang", "ru" }
            };

            return Call<ChartResponse>("main.jsx", parameters).Result;
        }

        public PlaylistResponse GetPlaylist(string idAndOwner)
        {
            var parameters = new Dictionary<string, string>
            {
                { "kinds", idAndOwner },
                { "lang", "ru" }
            };

            return Call<PlaylistResponse>("playlist.jsx", parameters).Result;
        }

        public SearchResponse Search(string query)
        {
            query = query.Replace(" ", "+");

            var parameters = new Dictionary<string, string>
            {
                { "text", query },
                { "nocorrect", "1" },
                { "type", "all" },
                { "lang", "ru" }
            };

            return Call<SearchResponse>("music-search.jsx", parameters).Result;
        }

        public async Task<T> Call<T>(string method, Dictionary<string, string> @params)
        {
            string url = _apiUrl + method + $"?{string.Join("&", @params.Select(p => $"{p.Key}={p.Value}"))}";

            string response = await _loader.GetHtmlResponse(url);

            //System.Diagnostics.Debug.WriteLine(response);

            try
            {
                JObject json = JObject.Parse(response);

                return JsonConvert.DeserializeObject<T>(json.ToString());
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}
