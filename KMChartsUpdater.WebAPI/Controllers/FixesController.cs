using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KMChartsUpdater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FixesController : ControllerBase
    {
        private readonly IChartService _chartService;
        private readonly ISecurityService _securityService;

        public FixesController(IChartService chartService, ISecurityService securityService)
        {
            _chartService = chartService;
            _securityService = securityService;
        }

        [HttpGet("{id}")]
        public ChartFixDto Get(int id)
        {
            var chartFixes = _chartService.GetAllFixes(id);

            return chartFixes;
        }

        [HttpDelete("{id}")]
        public Response Delete(int id, string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            _chartService.DeleteFix(id);

            return new DoneResponse("ok");
        }

        [HttpPost("date/{id}/{day}/{month}/{year}")]
        public Response Post(int id, int day, int month, int year, string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            _chartService.Date(id, day, month, year);

            return new DoneResponse("ok");
        }
    }
}
