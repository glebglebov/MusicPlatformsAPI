using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Chart
{
    public class ChartInfo
    {
        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("progress")]
        public string Progress { get; set; }

        [JsonProperty("listeners")]
        public long Listeners { get; set; }

        [JsonProperty("shift")]
        public int Shift { get; set; }

        [JsonProperty("bgColor")]
        public string BgColor { get; set; }
    }
}
