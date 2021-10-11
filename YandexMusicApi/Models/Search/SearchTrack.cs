using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace YandexMusicApi.Models.Search
{
    public class SearchTrack
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("availableAsRbt")]
        public bool AvailableAsRbt { get; set; }

        [JsonProperty("availableForPremiumUsers")]
        public bool AvailableForPremiumUsers { get; set; }

        [JsonProperty("lyricsAvailable")]
        public bool LyricsAvailable { get; set; }

        [JsonProperty("rememberPosition")]
        public bool RememberPosition { get; set; }

        [JsonProperty("trackSharingFlag")]
        public string TrackSharingFlag { get; set; }

        [JsonProperty("contentWarning")]
        public string ContentWarning { get; set; }

        [JsonProperty("albums")]
        public ReadOnlyCollection<SearchAlbum> Albums { get; set; }

        [JsonProperty("coverUri")]
        public string CoverUri { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("durationMs")]
        public long DurationMs { get; set; }

        [JsonProperty("explicit")]
        public bool Explicit { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("artists")]
        public ReadOnlyCollection<SearchArtist> Artists { get; set; }

        [JsonProperty("regions")]
        public ReadOnlyCollection<string> Regions { get; set; }

        [JsonProperty("chartPosition")]
        public int ChartPosition { get; set; }
    }
}
