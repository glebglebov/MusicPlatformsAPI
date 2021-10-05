using Newtonsoft.Json;

namespace VkApi.Models
{
    public class AudioArtist
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("domain")]
        public long Domain { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }
    }
}
