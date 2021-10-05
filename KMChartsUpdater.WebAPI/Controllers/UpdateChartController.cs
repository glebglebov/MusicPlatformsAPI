using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KMChartsUpdater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateChartController : ControllerBase
    {
        private readonly IChartService _chartService;
        private readonly ISecurityService _securityService;

        public UpdateChartController(IChartService chartService, ISecurityService securityService)
        {
            _chartService = chartService;
            _securityService = securityService;
        }

        [HttpPost("{chart}")]
        public Response Post(int chart, string token)
        {
            if (!_securityService.CheckToken(token))
                return new ErrorResponse("token is not valid");

            _chartService.UpdateChart(chart);

            return new DoneResponse("ok");
        }
    }
}
