using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace YandexMusicApi.Models.Playlist
{
    public class Playlist
    {
        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("kind")]
        public long Kind { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("descriptionFormatted")]
        public string DescriptionFormatted { get; set; }

        [JsonProperty("trackCount")]
        public int TrackCount { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("cover")]
        public PlaylistCover Cover { get; set; }

        [JsonProperty("owner")]
        public Owner Owner { get; set; }

        [JsonProperty("tracks")]
        public ReadOnlyCollection<Track.Track> Tracks { get; set; }

        [JsonProperty("modified")]
        public string Modified { get; set; }

        [JsonProperty("trackIds")]
        public ReadOnlyCollection<string> TrackIds { get; set; }

        [JsonProperty("ogImage")]
        public string OgImage { get; set; }

        [JsonProperty("tags")]
        public ReadOnlyCollection<Tag> Tags { get; set; }

        [JsonProperty("likesCount")]
        public long LikesCount { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("collective")]
        public bool Collective { get; set; }

        [JsonProperty("prerolls")]
        public ReadOnlyCollection<Preroll> Prerolls { get; set; }

        [JsonProperty("available")]
        public bool Available { get; set; }

        [JsonProperty("idForFrom")]
        public string IdForFrom { get; set; }

        [JsonProperty("doNotIndex")]
        public bool DoNotIndex { get; set; }
    }
}
