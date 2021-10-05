using System.Collections.Generic;
using KMChartsUpdater.BLL.Charts.Models;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface IMusicApiAdapter
    {
        void Auth();

        List<UnifiedAudioModel> GetChart(string type);

        UnifiedPlaylistModel GetPlaylist(string playlistId);

        List<UnifiedAlbumModel> GetAlbumChart(string type);

        void GetAlbum(string albumId);
    }
}
