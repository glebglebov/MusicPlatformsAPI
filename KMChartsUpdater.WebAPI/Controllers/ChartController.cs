using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KMChartsUpdater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IChartService _chartService;

        public ChartController(IChartService chartService)
        {
            _chartService = chartService;
        }

        [HttpGet("{id}")]
        public Response Get(int id, string date, int get_more = 0)
        {
            if (date?.Length > 10)
                return new ErrorResponse("wrong");

            if (get_more == 1)
            {
                return _chartService.GetChart(id, 15, 85, date);
            }

            return _chartService.GetChart(id, 0, 15, date);
        }

        //[HttpDelete("govno/{id}")]
        //public void Delete(int id)
        //{
        //    _chartService.DeleteChart(id);
        //}

        [HttpPost]
        public void Post()
        {
            
        }
    }
}
