using KMChartsUpdater.BLL.Responses;
using System.Threading.Tasks;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface IAudioService
    {
        Task<GetAudioStatsResponse> GetAudioPositions(int id, int chartId);

        void ChangeAudio(int one, int two, int chartId);

        void MergeAll();

        void RenameArtist(int audioId, string name);

        Response WithoutCover();

        Response SetCover(int id);

        Response SetCoverToAll();
    }
}
