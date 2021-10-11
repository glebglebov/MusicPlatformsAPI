using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace YandexMusicApi.Models.Search
{
    public class Artists
    {
        [JsonProperty("items")]
        public ReadOnlyCollection<SearchArtist> Items { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("perPage")]
        public int PerPage { get; set; }

        [JsonProperty("order")]
        public int Order { get; set; }
    }
}
