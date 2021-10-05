using KMChartsUpdater.BLL.Responses;
using KMChartsUpdater.DAL.Entities;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface IAccountService
    {
        Account GetAccount(int accountId, string key);

        Response Register(string login, string password, string organization);

        Response Login(string login, string password);

        Response LoginThroughVk(string id, string username, string token);
    }
}
