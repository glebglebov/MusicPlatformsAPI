using Newtonsoft.Json;

namespace AppleMusicApi.Models.LibraryPlaylists
{
    public class Attributes
    {
        [JsonProperty("artwork")]
        public Artwork Artwork { get; set; }

        [JsonProperty("canEdit")]
        public bool CanEdit { get; set; }

        [JsonProperty("dateAdded")]
        public string DateAdded { get; set; }

        [JsonProperty("description")]
        public DescriptionAttribute Description { get; set; }

        [JsonProperty("hasCatalog")]
        public bool HasCatalog { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("playParams")]
        public PlayParameters PlayParams { get; set; }
    }
}
