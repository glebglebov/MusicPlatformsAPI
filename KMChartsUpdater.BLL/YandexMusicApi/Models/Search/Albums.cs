using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Search
{
    public class Albums
    {
        [JsonProperty("items")]
        public ReadOnlyCollection<SearchAlbum> Items { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("perPage")]
        public int PerPage { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }
    }
}
