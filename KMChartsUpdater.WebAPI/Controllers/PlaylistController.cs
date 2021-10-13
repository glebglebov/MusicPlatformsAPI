using System.Collections.Generic;
using KMChartsUpdater.BLL.DTO;
using Microsoft.AspNetCore.Mvc;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;

namespace KMChartsUpdater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly ISecurityService _securityService;

        public PlaylistController(IPlaylistService playlistService, ISecurityService securityService)
        {
            _playlistService = playlistService;
            _securityService = securityService;
        }

        [HttpGet("")]
        public List<PlaylistShortDto> Get()
        {
            return _playlistService.GetAll();
        }

        [HttpPost("update")]
        public Response Post(int platform, string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            _playlistService.UpdatePlaylists(platform);

            return new DoneResponse("ok");
        }

        [HttpGet("check")]
        public Response Get(int chart, string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            _playlistService.FindTracks(chart);

            return new DoneResponse("ok");
        }
    }
}
