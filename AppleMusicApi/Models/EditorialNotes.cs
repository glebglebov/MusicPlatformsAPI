using Newtonsoft.Json;

namespace AppleMusicApi.Models
{
    public class EditorialNotes
    {
        [JsonProperty("short")]
        public string Short { get; set; }

        [JsonProperty("standard")]
        public string Standart { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }
    }
}
