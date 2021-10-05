using Newtonsoft.Json;

namespace VkApi.Models
{
    public class AudioChartInfo
    {
        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("state")]
        public int State { get; set; }
    }
}
