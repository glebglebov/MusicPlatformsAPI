using Newtonsoft.Json;

namespace VkApi.Models
{
    public class AudioGenre
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
