using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace YandexMusicApi.Models.Track
{
    public class Track
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("realId")]
        public long RealId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("contentWarning")]
        public string ContentWarning { get; set; }

        [JsonProperty("major")]
        public Major Major { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("availableForPremiumUsers")]
        public bool AvailableForPremiumUsers { get; set; }

        [JsonProperty("availableFullWithoutPermission")]
        public bool AvailableFullWithoutPermission { get; set; }

        [JsonProperty("durationMs")]
        public long DurationMs { get; set; }

        [JsonProperty("storageDir")]
        public string StorageDir { get; set; }

        [JsonProperty("fileSize")]
        public long FileSize { get; set; }

        [JsonProperty("normalization")]
        public Normalization Normalization { get; set; }

        [JsonProperty("previewDurationMs")]
        public long PreviewDurationMs { get; set; }

        [JsonProperty("artists")]
        public ReadOnlyCollection<Artist.Artist> Artists { get; set; }

        [JsonProperty("albums")]
        public ReadOnlyCollection<Album.Album> Albums { get; set; }

        [JsonProperty("coverUri")]
        public string CoverUri { get; set; }

        [JsonProperty("ogImage")]
        public string OgImage { get; set; }

        [JsonProperty("lyricsAvailable")]
        public bool LyricsAvailable { get; set; }

        [JsonProperty("best")]
        public bool Best { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("rememberPosition")]
        public bool RememberPosition { get; set; }

        //[JsonProperty("")]
        //public string BackgroundVideoUri { get; set; }

        [JsonProperty("trackSharingFlag")]
        public string TrackSharingFlag { get; set; }
    }
}
