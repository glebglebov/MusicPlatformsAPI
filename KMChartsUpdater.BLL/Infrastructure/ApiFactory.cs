using KMChartsUpdater.BLL.Adapters;
using KMChartsUpdater.BLL.Config;
using KMChartsUpdater.BLL.Interfaces;

namespace KMChartsUpdater.BLL.Infrastructure
{
    public class ApiFactory
    {
        private static MyConfig _config;

        public static void SetConfig(MyConfig config)
        {
            _config = config;
        }

        private VkMusicApiAdapter _vkApi;
        private SpotifyApiAdapter _spotifyApi;
        private AppleMusicApiAdapter _appleMusicApi;
        private YandexMusicApiAdapter _yandexMusicApi;

        public static ApiFactory Instance { get; } = new ApiFactory();

        private ApiFactory()
        {

        }

        public static IMusicApiAdapter GetByPlatformCode(string code)
        {
            IMusicApiAdapter api = null;

            switch (code)
            {
                case "vk":
                    api = Instance.VkApi;
                    break;
                case "spotify":
                    api = Instance.SpotifyApi;
                    break;
                case "yandex_music":
                    api = Instance.YandexMusicApi;
                    break;
                case "apple_music":
                    api = Instance.AppleMusicApi;
                    break;
            }

            return api;
        }

        public VkMusicApiAdapter VkApi
        {
            get
            {
                if (_vkApi == null)
                    _vkApi = new VkMusicApiAdapter(_config);

                return _vkApi;
            }
        }

        public SpotifyApiAdapter SpotifyApi
        {
            get
            {
                if (_spotifyApi == null)
                    _spotifyApi = new SpotifyApiAdapter(_config);

                return _spotifyApi;
            }
        }

        public AppleMusicApiAdapter AppleMusicApi
        {
            get
            {
                if (_appleMusicApi == null)
                    _appleMusicApi = new AppleMusicApiAdapter(_config);

                return _appleMusicApi;
            }
        }

        public YandexMusicApiAdapter YandexMusicApi
        {
            get
            {
                if (_yandexMusicApi == null)
                    _yandexMusicApi = new YandexMusicApiAdapter();

                return _yandexMusicApi;
            }
        }
    }
}
