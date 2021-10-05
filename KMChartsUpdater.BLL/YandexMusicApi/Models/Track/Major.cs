using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Track
{
    public class Major
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
