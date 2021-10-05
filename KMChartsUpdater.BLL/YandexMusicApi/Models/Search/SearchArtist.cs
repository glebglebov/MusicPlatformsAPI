using KMChartsUpdater.BLL.YandexMusicApi.Models.Artist;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace KMChartsUpdater.BLL.YandexMusicApi.Models.Search
{
    public class SearchArtist
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cover")]
        public ArtistCover Cover { get; set; }

        [JsonProperty("composer")]
        public bool Composer { get; set; }

        [JsonProperty("various")]
        public bool Various { get; set; }

        [JsonProperty("counts")]
        public Counts Counts { get; set; }

        [JsonProperty("genres")]
        public ReadOnlyCollection<string> Genres { get; set; }

        [JsonProperty("ticketsAvailable")]
        public bool TicketsAvailable { get; set; }

        [JsonProperty("popularTracks")]
        public ReadOnlyCollection<SearchTrack> PopularTracks { get; set; }

        [JsonProperty("regions")]
        public ReadOnlyCollection<string> Regions { get; set; }
    }
}
