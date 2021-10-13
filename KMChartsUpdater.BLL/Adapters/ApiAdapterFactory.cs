using System;
using System.Net;
using System.Net.Http;
using AppleMusicApi;
using KMChartsUpdater.BLL.Config;
using KMChartsUpdater.BLL.Interfaces;
using SpotifyApi;
using VkApi;
using YandexMusicApi;

namespace KMChartsUpdater.BLL.Adapters
{
    public class ApiAdapterFactory : IApiAdapterFactory
    {
        private readonly MyConfig _config;

        private static readonly Random Random = new Random();

        public ApiAdapterFactory(MyConfig config)
        {
            _config = config;
        }

        public VkMusicApiAdapter CreateVkApiAdapter(bool useProxy, int proxyIndex = -1)
        {
            var client = useProxy ? CreateProxiedClient(proxyIndex) : CreateClient();
            var vkApi = new VkAudioApi(client);

            var adapter = new VkMusicApiAdapter(vkApi, _config);

            adapter.SetAccount(0);

            return adapter;
        }

        public SpotifyApiAdapter CreateSpotifyApiAdapter(bool useProxy, int proxyIndex = -1)
        {
            var client = useProxy ? CreateProxiedClient(proxyIndex) : CreateClient();
            var spotifyApi = new Spotify(client);

            spotifyApi.Auth(_config.Credentials.Spotify[0].ClientId, "2048ff16164f4558a76f659b4109a654");

            return new SpotifyApiAdapter(spotifyApi, _config);
        }

        public YandexMusicApiAdapter CreateYandexMusicApiAdapter(bool useProxy, int proxyIndex = -1)
        {
            var client = useProxy ? CreateProxiedClient(proxyIndex) : CreateClient();
            var yaApi = new YandexMusic(client);

            return new YandexMusicApiAdapter(yaApi);
        }

        public AppleMusicApiAdapter CreateAppleMusicApiAdapter(bool useProxy, int proxyIndex = -1)
        {
            var client = useProxy ? CreateProxiedClient(proxyIndex) : CreateClient();
            var amApi = new AppleMusic(client);

            amApi.Auth(_config.Credentials.Apple[0].Key);

            return new AppleMusicApiAdapter(amApi);
        }

        private HttpClient CreateClient()
        {
            HttpMessageHandler handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(5)
            };

            return new HttpClient(handler);
        }

        private HttpClient CreateProxiedClient(int proxyIndex = -1)
        {
            int index = (proxyIndex < 0) ? Random.Next(0, _config.Proxies.Length) : proxyIndex;

            var proxy = _config.Proxies[index];

            var uri = new Uri(proxy.Url);
            var credentials = new NetworkCredential(proxy.Login, proxy.Password);

            var webProxy = new WebProxy(uri, true, null, credentials);

            System.Diagnostics.Debug.WriteLine(proxy.Url);

            HttpMessageHandler handler = new SocketsHttpHandler
            {
                Proxy = webProxy,
                UseProxy = true,
                PooledConnectionLifetime = TimeSpan.FromMinutes(5)
            };

            return new HttpClient(handler);
        }
    }
}
