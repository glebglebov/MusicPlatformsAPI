using KMChartsUpdater.BLL.Responses;
using KMChartsUpdater.DAL.Entities;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface ITaskService
    {
        Response DeleteTask(int id);

        Response GetTask(int id, Account account);

        Response GetAccountTasks(Account account);

        void CreateTaskFromPlaylist(Account account, string playlistId);

        void UpdateActiveTasks();
    }
}
