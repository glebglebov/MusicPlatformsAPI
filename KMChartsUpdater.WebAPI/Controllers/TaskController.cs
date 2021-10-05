using Microsoft.AspNetCore.Mvc;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;

namespace KMChartsUpdater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITaskService _taskService;
        private readonly ISecurityService _securityService;

        public TaskController(IAccountService accountService, ITaskService taskService, ISecurityService securityService)
        {
            _accountService = accountService;
            _taskService = taskService;
            _securityService = securityService;
        }

        [HttpPost("updateAll")]
        public Response Post(string token)
        {
            if (!_securityService.CheckToken(token))
                return new ErrorResponse("token is not valid");

            _taskService.UpdateActiveTasks();

            return new DoneResponse();
        }

        [HttpPost("create")]
        public Response Post(string playlist, int uid, string key)
        {
            var account = _accountService.GetAccount(uid, key);

            if (account == null)
                return new ErrorResponse("account data is invalid");

            _taskService.CreateTaskFromPlaylist(account, playlist);

            return new DoneResponse();
        }

        [HttpGet("getAll")]
        public Response Get(int uid, string key)
        {
            var account = _accountService.GetAccount(uid, key);

            if (account == null)
                return new ErrorResponse("account data is invalid");

            return _taskService.GetAccountTasks(account);
        }

        [HttpGet("get")]
        public Response Get(int id, int uid, string key)
        {
            var account = _accountService.GetAccount(uid, key);

            if (account == null)
                return new ErrorResponse("account data is invalid");

            return _taskService.GetTask(id, account);
        }

        [HttpPost("delete")]
        public Response Post(int id, int uid, string key)
        {
            var account = _accountService.GetAccount(uid, key);

            if (account == null)
                return new ErrorResponse("account data is invalid");

            return _taskService.DeleteTask(id);
        }
    }
}
