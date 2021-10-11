using System.Collections.ObjectModel;
using Newtonsoft.Json;
using YandexMusicApi.Models.Playlist;

namespace YandexMusicApi.Models.Chart
{
    public class Chart
    {
        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("playlistUuid")]
        public string PlaylistUuid { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("kind")]
        public int Kind { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("descriptionFormatted")]
        public string DescriptionFormatted { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("snapshot")]
        public int Snapshot { get; set; }

        [JsonProperty("trackCount")]
        public int TrackCount { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("collective")]
        public bool Collective { get; set; }

        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("isBanner")]
        public bool IsBanner { get; set; }

        [JsonProperty("isPremiere")]
        public bool IsPremiere { get; set; }

        [JsonProperty("durationMs")]
        public long DurationMs { get; set; }

        [JsonProperty("cover")]
        public PlaylistCover Cover { get; set; }

        [JsonProperty("ogImage")]
        public string OgImage { get; set; }

        [JsonProperty("tracks")]
        public ReadOnlyCollection<ChartTrack> Tracks { get; set; }

        [JsonProperty("tags")]
        public ReadOnlyCollection<Tag> Tags { get; set; }

        [JsonProperty("likesCount")]
        public long LikesCount { get; set; }

        [JsonProperty("prerolls")]
        public ReadOnlyCollection<Preroll> Prerolls { get; set; }

        // similarPlaylists

        [JsonProperty("trackIds")]
        public ReadOnlyCollection<string> TrackIds { get; set; }
    }
}
