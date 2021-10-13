using KMChartsUpdater.BLL.Adapters;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface IApiAdapterFactory
    {
        VkMusicApiAdapter CreateVkApiAdapter(bool useProxy, int proxyIndex = -1);

        SpotifyApiAdapter CreateSpotifyApiAdapter(bool useProxy, int proxyIndex = -1);

        YandexMusicApiAdapter CreateYandexMusicApiAdapter(bool useProxy, int proxyIndex = -1);

        AppleMusicApiAdapter CreateAppleMusicApiAdapter(bool useProxy, int proxyIndex = -1);
    }
}