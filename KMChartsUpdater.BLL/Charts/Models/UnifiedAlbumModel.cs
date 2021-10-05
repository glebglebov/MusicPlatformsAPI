using System.Linq;

using VkAlbum = VkApi.Models.AudioCatalogAlbum;

namespace KMChartsUpdater.BLL.Charts.Models
{
    public class UnifiedAlbumModel
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public bool IsExplicit { get; set; }
        public string ThumbUrl { get; set; }
        public string[] Genres { get; set; }
        public int Position { get; set; }
        public long Plays { get; set; }

        public static UnifiedAlbumModel Create(VkAlbum album, int position)
        {
            string[] artists = album.MainArtists.Select(x => x.Name).ToArray();
            string artist = string.Join(", ", artists);

            string[] genres = album.Genres.Select(x => x.Name).ToArray();

            return new UnifiedAlbumModel
            {
                Artist = artist,
                Title = album.Title,
                Year = album.Year,
                IsExplicit = album.IsExplicit,
                ThumbUrl = album.Photo.Photo300,
                Genres = genres,
                Position = position,
                Plays = album.Plays
            };
        }
    }
}
