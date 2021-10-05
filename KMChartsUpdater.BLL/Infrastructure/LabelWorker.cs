using AppleMusicApi;
using System;
using System.Linq;

using AppleAlbum = AppleMusicApi.Models.Albums.Album;

namespace KMChartsUpdater.BLL.Infrastructure
{
    public class LabelWorker
    {
        private static readonly AppleMusic _api;

        public LabelWorker()
        {
            
        }

        public static string GetLabelFromAppleMusic(AppleMusic am, string artist, string song)
        {
            var album = GetAlbum(am, artist, song);

            return album?.Attributes.RecordLabel;
        }

        public static string GetCoverUrl(AppleMusic am, string artist, string song)
        {
            var album = GetAlbum(am, artist, song);

            return album?.Attributes.Artwork.Url.Replace("{w}x{h}", "150x150");
        }

        private static AppleAlbum GetAlbum(AppleMusic am, string artist, string song)
        {
            song = song.Replace("+", "%2b");
            artist = artist.Replace("+", "%2b");

            string query = artist + " " + song;
            query = query.Replace(" ", "+");

            var songResponse = am.Search(query, "songs", "ru", 1);

            System.Diagnostics.Debug.WriteLine(query);

            try
            {
                var songResult = songResponse.Results.Songs?.Data.FirstOrDefault();

                string albumName = songResult.Attributes.AlbumName;
                string artistName = songResult.Attributes.ArtistName;

                query = artistName + "+" + albumName;
                query = query.Replace(" ", "+").Replace("&", "+").Replace("?", "+"); ;

                var albumResponse = am.Search(query, "albums", "ru", 1);

                var albumResult = albumResponse.Results.Albums?.Data.FirstOrDefault();

                return albumResult;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
