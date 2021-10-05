using Newtonsoft.Json;

namespace AppleMusicApi.Models
{
    public class PlayParameters
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("kind")]
        public string Kind { get; set; }
    }
}
