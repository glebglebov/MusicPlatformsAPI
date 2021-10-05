using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace AppleMusicApi.Models.Albums
{
    public class Attributes
    {
        [JsonProperty("artistName")]
        public string ArtistName { get; set; }

        [JsonProperty("artwork")]
        public Artwork Artwork { get; set; }

        [JsonProperty("contentRating")]
        public string ContentRating { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        [JsonProperty("editorialNotes")]
        public EditorialNotes EditorialNotes { get; set; }

        [JsonProperty("genreNames")]
        public ReadOnlyCollection<string> GenreNames { get; set; }

        [JsonProperty("isComplete")]
        public bool IsComplete { get; set; }

        [JsonProperty("isMasteredForItunes")]
        public bool IsMasteredForItunes { get; set; }

        [JsonProperty("isSingle")]
        public bool IsSingle { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("playParams")]
        public PlayParameters PlayParams { get; set; }

        [JsonProperty("recordLabel")]
        public string RecordLabel { get; set; }

        [JsonProperty("releaseDate")]
        public string ReleaseDate { get; set; }

        [JsonProperty("trackCount")]
        public int TrackCount { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("isCompilation")]
        public bool IsCompilation { get; set; }

        [JsonProperty("upc")]
        public string Upc { get; set; }

        [JsonProperty("artistUrl")]
        public string ArtistUrl { get; set; }
    }
}
