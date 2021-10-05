using KMChartsUpdater.BLL.Responses;
using System.Threading.Tasks;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface IAlbumService
    {
        Task<GetAudioStatsResponse> GetAlbumPositions(int id, int chartId);
    }
}
