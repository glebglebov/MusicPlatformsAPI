using Microsoft.AspNetCore.Mvc;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;

namespace KMChartsUpdater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ISecurityService _securityService;

        public AccountController(IAccountService accountService, ISecurityService securityService)
        {
            _accountService = accountService;
            _securityService = securityService;
        }

        [HttpPost("register")]
        public Response Post(string login, string password, string organization, string token)
        {
            if (!_securityService.CheckToken(token))
                return new ErrorResponse("token is not valid");

            return _accountService.Register(login, password, organization);
        }

        [HttpPost("login")]
        public Response Post(string login, string password)
        {
            return _accountService.Login(login, password);
        }

        [HttpGet("login")]
        public Response Get(string id, string username, string token)
        {
            return _accountService.LoginThroughVk(id, username, token);
        }

        [HttpGet("validate")]
        public ValidateResponse Get(int uid, string key)
        {
            var account = _accountService.GetAccount(uid, key);
            if (account == null)
                return new ValidateResponse { IsDataValid = false };

            return new ValidateResponse { IsDataValid = true, Username = account.Username };
        }
    }
}