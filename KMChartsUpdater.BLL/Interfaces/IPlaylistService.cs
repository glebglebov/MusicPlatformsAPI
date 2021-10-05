using System.Threading.Tasks;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface IPlaylistService
    {
        void UpdatePlaylists(int platformId);

        void FindTracks(int chartId);
    }
}
