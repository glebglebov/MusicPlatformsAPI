using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KMChartsUpdater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet("positions/{id}/{chartId}")]
        public async Task<GetAudioStatsResponse> Get(int id, int chartId)
        {
            return await _albumService.GetAlbumPositions(id, chartId);
        }
    }
}
