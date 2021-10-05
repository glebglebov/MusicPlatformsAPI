using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Artist
{
    public class Artist
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("various")]
        public bool Various { get; set; }

        [JsonProperty("composer")]
        public bool Composer { get; set; }

        [JsonProperty("cover")]
        public ArtistCover Cover { get; set; }
    }
}
