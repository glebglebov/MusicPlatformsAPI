using KMChartsUpdater.BLL.Responses;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface ISecurityService
    {
        bool CheckToken(string token);

        GlobalStatsResponse GetGlobalStats();

        Response CleanAudios();

        void ChangeAudioInFixes(int audio1Id, int audio2Id, int from, int to);
    }
}
