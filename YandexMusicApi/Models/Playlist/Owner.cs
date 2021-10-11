using Newtonsoft.Json;

namespace YandexMusicApi.Models.Playlist
{
    public class Owner
    {
        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sex")]
        public string Sex { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }

        [JsonProperty("avatarHash")]
        public string AvatarHash { get; set; }
    }
}
