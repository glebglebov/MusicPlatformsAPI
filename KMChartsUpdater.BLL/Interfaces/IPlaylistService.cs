using System.Collections.Generic;
using KMChartsUpdater.BLL.DTO;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface IPlaylistService
    {
        List<PlaylistShortDto> GetAll();

        void UpdatePlaylists(int platformId);

        void FindTracks(int chartId);
    }
}
