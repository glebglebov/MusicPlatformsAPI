using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Chart
{
    public class ChartTrack : Track.Track
    {
        [JsonProperty("chart")]
        public ChartInfo Chart { get; set; }

        [JsonProperty("prefix")]
        public string Prefix { get; set; }
    }
}
