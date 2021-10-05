using System;
using System.Collections.Generic;
using System.Linq;
using AppleMusicApi;
using KMChartsUpdater.BLL.Charts.Models;
using KMChartsUpdater.BLL.Config;
using KMChartsUpdater.BLL.Interfaces;

using AppleAlbum = AppleMusicApi.Models.Albums.Album;

namespace KMChartsUpdater.BLL.Adapters
{
    public class AppleMusicApiAdapter : IMusicApiAdapter
    {
        private readonly AppleMusic _api;
        private readonly MyConfig _config;

        public AppleMusicApiAdapter(MyConfig config)
        {
            _api = new AppleMusic();
            _config = config;
        }

        public void Auth()
        {
            _api.Auth(_config.Auth.Apple.Key);
        }

        public List<UnifiedAudioModel> GetChart(string type)
        {
            string storefront = type switch
            {
                "apple_music_ru" => "ru",
                "apple_music_ua" => "ua",
                "apple_music_kz" => "kz",
                _ => null
            };

            if (storefront == null)
                return null;

            var charts = _api.GetCatalogCharts(storefront, "songs", 100);
            var chart = charts.Results.Songs.FirstOrDefault(x => x.Chart == "most-played");

            List<UnifiedAudioModel> result = new List<UnifiedAudioModel>();

            int i = 1;

            foreach (var audio in chart.Data)
            {
                result.Add(UnifiedAudioModel.Create(audio, i));

                ++i;
            }

            return result;
        }

        public UnifiedPlaylistModel GetPlaylist(string playlistId)
        {
            var pl = _api.GetCatalogPlaylist("ru", playlistId);
            var playlistData = pl.Data?.FirstOrDefault();

            List<UnifiedAudioModel> result = new List<UnifiedAudioModel>();

            if (playlistData == null)
                return null;

            int i = 1;

            foreach (var audio in playlistData.Relationships.Tracks.Data)
            {
                //System.Diagnostics.Debug.WriteLine(audio.Artist + " - " + audio.Title);
                result.Add(UnifiedAudioModel.Create(audio, i));

                ++i;
            }

            string coverUrl = playlistData.Attributes.Artwork.Url.Replace("{w}x{h}", "300x300");

            var playlist = new UnifiedPlaylistModel
            {
                CoverUrl = coverUrl,
                Audios = result
            };

            return playlist;
        }

        public List<UnifiedAlbumModel> GetAlbumChart(string type)
        {
            throw new NotImplementedException();
        }

        public void GetAlbum(string albumId)
        {
            throw new NotImplementedException();
        }

        public AppleAlbum SearchAlbum(string artist, string title)
        {
            string titleFormatted = title.Replace("+", "%2b");
            string artistFormatted = artist.Replace("+", "%2b");

            string query = artistFormatted + "+" + titleFormatted;
            query = query.Replace(" ", "+");

            var songResponse = _api.Search(query, "songs", "ru", 1);

            System.Diagnostics.Debug.WriteLine(query);

            try
            {
                var songResult = songResponse.Results.Songs?.Data.FirstOrDefault();

                string albumName = songResult.Attributes.AlbumName;
                string artistName = songResult.Attributes.ArtistName;

                query = artistName + "+" + albumName;
                query = query.Replace(" ", "+").Replace("&", "+").Replace("?", "+"); ;

                var albumResponse = _api.Search(query, "albums", "ru", 1);

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
