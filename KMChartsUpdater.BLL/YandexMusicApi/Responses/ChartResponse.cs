using KMChartsUpdater.BLL.YandexMusicApi.Models.Chart;
using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.YandexMusicApi.Responses
{
    public class ChartResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("typeForFrom")]
        public string TypeForFrom { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("chartDescription")]
        public string ChartDescription { get; set; }

        // menu

        [JsonProperty("chart")]
        public Chart Chart { get; set; }

        [JsonProperty("chartRegionName")]
        public string ChartRegionName { get; set; }
    }
}
