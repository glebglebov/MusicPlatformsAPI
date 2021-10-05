using System.Linq;

using AppleMusicAudio = AppleMusicApi.Models.Songs.Song;
using VkAudio = VkApi.Models.AudioCatalogAudio;
using SpotifyAudio = SpotifyApi.Models.Track;

namespace KMChartsUpdater.BLL.Charts.Models
{
    public class UnifiedAudioModel
    {
        public ChartPlatform Platform { get; set; }
        public string AlbumId { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; } = null;
        public bool IsExplicit { get; set; }
        public string ThumbUrl { get; set; }
        public int Position { get; set; }
        public long Plays { get; set; }

        public UnifiedAudioModel()
        {

        }

        public static UnifiedAudioModel Create(AppleMusicAudio audio, int position)
        {
            bool isExplicit = audio.Attributes.ContentRating == "explicit";

            string thumb = audio.Attributes.Artwork.Url.Replace("{w}x{h}", "150x150");

            return new UnifiedAudioModel
            {
                Platform = ChartPlatform.Apple,
                Artist = audio.Attributes.ArtistName,
                Title = audio.Attributes.Name,
                IsExplicit = isExplicit,
                ThumbUrl = thumb,
                Position = position
            };
        }

        public static UnifiedAudioModel Create(VkAudio audio, int position)
        {
            return new UnifiedAudioModel
            {
                Platform = ChartPlatform.VK,
                Artist = audio.Artist,
                Title = audio.Title,
                Subtitle = audio.Subtitle,
                IsExplicit = audio.IsExplicit,
                ThumbUrl = audio.Album?.Thumb.Photo300,
                Position = position
            };
        }

        public static UnifiedAudioModel Create(SpotifyAudio audio, int position)
        {
            string[] artists = audio.Artists.Select(x => x.Name).ToArray();
            string artist = string.Join(", ", artists);

            string thumb = audio.Album.Images
                        .Where(x => x.Height == "300")
                        .Select(x => x.Url)
                        .FirstOrDefault();

            return new UnifiedAudioModel
            {
                Platform = ChartPlatform.Spotify,
                AlbumId = audio.Album.Id,
                Artist = artist,
                Title = audio.Name,
                IsExplicit = audio.Explicit,
                ThumbUrl = thumb,
                Position = position
            };
        }

        public static UnifiedAudioModel Create(YandexMusicApi.Models.Chart.ChartTrack audio)
        {
            string[] artists = audio.Artists.Select(x => x.Name).ToArray();
            string artist = string.Join(", ", artists);

            string thumb = "https://" + audio.OgImage.Replace("%%", "150x150");

            return new UnifiedAudioModel
            {
                Platform = ChartPlatform.Yandex,
                Artist = artist,
                Title = audio.Title,
                IsExplicit = audio.ContentWarning == "explicit",
                ThumbUrl = thumb,
                Position = audio.Chart.Position,
                Plays = audio.Chart.Listeners
            };
        }

        public static UnifiedAudioModel Create(YandexMusicApi.Models.Track.Track audio, int position)
        {
            string[] artists = audio.Artists.Select(x => x.Name).ToArray();
            string artist = string.Join(", ", artists);

            string thumb = "https://" + audio.OgImage.Replace("%%", "150x150");

            return new UnifiedAudioModel
            {
                Position = position,
                Platform = ChartPlatform.Yandex,
                Artist = artist,
                Title = audio.Title,
                IsExplicit = audio.ContentWarning == "explicit",
                ThumbUrl = thumb
            };
        }
    }
}
