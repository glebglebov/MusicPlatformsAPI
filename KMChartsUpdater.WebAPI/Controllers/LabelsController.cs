using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KMChartsUpdater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelService _labelService;
        private readonly ISecurityService _securityService;

        public LabelsController(ILabelService labelService, ISecurityService securityService)
        {
            _labelService = labelService;
            _securityService = securityService;
        }


        [HttpGet("{id}")]
        public Response Get(int id)
        {
            return _labelService.GetLabel(id);
        }

        [HttpPost("set/{id}")]
        public Response Post(int id, string name, string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            return _labelService.SetLabels(id, name);
        }

        [HttpPost("set_for_fix/{id}")]
        public Response Post(int id, string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            return _labelService.SetLabelsForFix(id);
        }

        [HttpPost("set_for_all")]
        public Response Post(string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            return _labelService.SetLabelsToAll();
        }

        [HttpGet("delete/{id}")]
        public Response Get(int id, string token, string s)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            return _labelService.RemoveLabels(id);
        }

        [HttpDelete("clear")]
        public Response Delete(string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            return _labelService.RemoveAll();
        }
    }
}
