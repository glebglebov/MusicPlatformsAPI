using System;
using System.Linq;
using System.Threading;
using AppleMusicApi;
using KMChartsUpdater.BLL.YandexMusicApi;
using MooscleParser;
using SpotifyApi;
using VkApi;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestYaMusic();
            //TestSpotify();
            TestVk();
            //TestAppleMusic();

            //AppleMusic am = new AppleMusic();
            //am.Auth("eyJhbGciOiJFUzI1NiIsImtpZCI6IlBYODU1Q05SUDYifQ.eyJpc3MiOiJEQTRWQjNUUUFTIiwiaWF0IjoxNjI1NDEyNjY4LjAsImV4cCI6MTY0MDk2NDcyOC4wfQ.hH5QFWvfx24gAkWxHUWUFmdyJdalv4PHXKeJ6ptMZstki3RjkvT8EuFH2PbpHqjuKg1p9gPGtUfTAZ0ipETZcA");

            //var pl = am.GetCatalogPlaylist("ru", "pl.804a365786c24f2786b01cf79e3c0b88");
            //var playlist = pl.Data.FirstOrDefault();

            //foreach (var track in playlist.Relationships.Tracks.Data)
            //{
            //    Console.WriteLine(track.Attributes.ArtistName + " - " + track.Attributes.Name);
            //}

            //string artist = "hensy";
            //string query = artist + "+пить+и+курить";

            //var songResponse = am.Search(query, "songs", "ru", 1);

            //var songResult = songResponse.Results.Songs.Data.FirstOrDefault();

            //string albumName = songResult.Attributes.AlbumName;
            //string artistName = songResult.Attributes.ArtistName;

            //query = artistName + "+" + albumName;
            //query = query.Replace(" ", "+").Replace("&", "+").Replace("?", "+");
            //Console.WriteLine(artistName);
            //Console.WriteLine("QUERY: " + query);

            //var albumResponse = am.Search(query, "albums", "ru", 1);

            //var albumResult = albumResponse.Results.Albums.Data.FirstOrDefault();
            //string copyright = albumResult.Attributes.Artwork.Url;


            //Console.WriteLine("RESPONSE: " + albumResult.Attributes.Name + " - " + copyright);

            //VkAudioApi api = new VkAudioApi();
            //api.Auth("+79912094252", "izKLBQ");

            //var likes = api.LikesGetList("audio", "-2001756605", "94756605");
            //Console.WriteLine(likes.);

            //api.GetPlaylistById("-2000459677", "11459677");

            //api.CatalogGetAudio();
            //api.GetBlockItems("PUlQVA8GR0R3W0tMF1UHBDMGGilWXAoUMklFVA0WUVJyXlpHBgBaUGpJXFQPFg4eNgcGBFAWR0RySVNHGRZaU2RRWAs");

            //var chart = api.GetAlbumsChartSection("PUlQVA8GR0R3W0tMF1UHBDMGGilWXAoUMklFVA0WUVJyXlpHBgBaUGpJXFQPFg4eNgcGBFAWR0RySVNHGRZaU2RRWFoXBl1EfF9dQwYFWFJ3XRQ");

            //foreach (var album in chart.Playlists)
            //{
            //    string[] artists = album.MainArtists.Select(x => x.Name).ToArray();
            //    string artist = string.Join(", ", artists);

            //    Console.WriteLine(album.AudioChartInfo.Position.ToString() + ". " + artist + " - " + album.Title + ", " + album.Year);
            //}




            //SpotifySettings settings = new SpotifySettings
            //{
            //    ClientId = "02e781a4f005402f8f4ca505f5b1e6da",
            //    ClientSecret = "2048ff16164f4558a76f659b4109a654"
            //};

            //Spotify spotify = new Spotify(settings);
            //spotify.Auth();

            //var playlist = spotify.GetPlaylistItems("37i9dQZEVXbL8l7ra5vVdB", "ru");

            //int i = 1;
            //foreach (var track in playlist.Items)
            //{
            //    string[] artists = track.Track.Artists.Select(x => x.Name).ToArray();
            //    string artist = string.Join(", ", artists);

            //    Console.WriteLine(i.ToString() + ". " + artist + " - " + track.Track.Name);

            //    ++i;
            //}

            //var album = spotify.GetAlbum("3njbXvOfMafaXPM0QZxuyK");
            //Console.WriteLine(album.Name);




            //YandexMusic yandex = new YandexMusic();
            //var chart = yandex.GetChart();

            //foreach (var audio in chart.Chart.Tracks)
            //{
            //    string[] artists = audio.Artists.Select(x => x.Name).ToArray();
            //    string artist = string.Join(", ", artists);

            //    Console.WriteLine(audio.Chart.Position.ToString() + ". " + artist + " - " + audio.Title);
            //}

            //var search = yandex.Search("Måneskin Beggin'");
            //var result = search.Tracks.Items.FirstOrDefault();

            //Console.WriteLine(result.Title + ", " + result.Albums.FirstOrDefault().Labels.FirstOrDefault());

            //Parser parser = new Parser();
            //parser.ParseByDate(25, 10, 2018);

            while (true)
            {

            }
        }

        static string AddLineBreaks(string str, int lineLength)
        {
            string formatted = str;

            int i = lineLength;

            while (i < formatted.Length)
            {
                formatted = formatted.Insert(i, "\n");

                Console.WriteLine(formatted);

                i += lineLength;
            }

            return formatted;
        }

        static void TestAppleMusic()
        {
            AppleMusic am = new AppleMusic();
            am.Auth("eyJhbGciOiJFUzI1NiIsImtpZCI6IlBYODU1Q05SUDYifQ.eyJpc3MiOiJEQTRWQjNUUUFTIiwiaWF0IjoxNjI1NDEyNjY4LjAsImV4cCI6MTY0MDk2NDcyOC4wfQ.hH5QFWvfx24gAkWxHUWUFmdyJdalv4PHXKeJ6ptMZstki3RjkvT8EuFH2PbpHqjuKg1p9gPGtUfTAZ0ipETZcA");

            var pl = am.GetCatalogPlaylist("ru", "pl.f4d106fed2bd41149aaacabb233eb5eb");
            var playlistData = pl.Data?.FirstOrDefault();

            System.Diagnostics.Debug.WriteLine(playlistData.Attributes.Artwork.Url);
        }

        static void TestVk()
        {
            VkAudioApi api = new VkAudioApi();
            api.Auth("+79912094252", "izKLBQ");

            var playlistResponse = api.GetPlaylistById("-147845620_2212");
            var playlistAudioResponse = api.ExecuteGetPlaylist("-147845620_2212");
        }

        static void TestSpotify()
        {
            Spotify spotify = new Spotify();
            spotify.Auth("02e781a4f005402f8f4ca505f5b1e6da", "2048ff16164f4558a76f659b4109a654");

            var response = spotify.GetPlaylist("37i9dQZF1DX93L1kSOomqn", "RU");

            foreach (var track in response.Tracks.Items)
            {
                string[] artists = track.Track.Artists.Select(x => x.Name).ToArray();
                string artist = string.Join(", ", artists);

                Console.WriteLine(artist + " - " + track.Track.Name);
            }
        }

        static void TestYaMusic()
        {
            var playlistIds = new[]
            {
                "1096&owner=yamusic-top",
                //"1032&owner=yamusic-new",
                //"1058&owner=yamusic-new",
                //"2615&owner=music-blog",
                //"1006&owner=yamusic-new",
                //"1005&owner=yamusic-new",
                //"1176&owner=music-blog",
                //"1777&owner=music-blog",
                //"1049&owner=yamusic-new",
                //"1051&owner=yamusic-new",
                //"2071&owner=music-blog",
                //"1000&owner=yamusic-new",
                //"2464&owner=music-blog",
                //"1077&owner=yamusic-new",
                //"1012&owner=yamusic-new",
                //"2069&owner=music-blog",
                //"1014&owner=yamusic-new",
                //"1036&owner=yamusic-new",
                //"1039&owner=yamusic-new",
                //"1003&owner=yamusic-new",
                //"1007&owner=yamusic-new",
                //"1001&owner=yamusic-new",
                //"1064&owner=yamusic-new",
                //"2629&owner=music-blog",
                //"2440&owner=music-blog",
                //"2441&owner=music-blog",
                //"2466&owner=music-blog",
                //"1175&owner=music-blog"
            };

            YandexMusic yandex = new YandexMusic();

            foreach (var playlistId in playlistIds)
            {
                var playlist = yandex.GetPlaylist(playlistId);

                System.Diagnostics.Debug.WriteLine(playlist.Playlist.OgImage);

                var title = playlist.Playlist.Title;

                Console.WriteLine(title);

                Thread.Sleep(200);
            }
        }
    }
}
