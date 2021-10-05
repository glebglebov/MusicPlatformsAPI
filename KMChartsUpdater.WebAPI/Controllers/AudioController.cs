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
    public class AudioController : ControllerBase
    {
        private readonly IAudioService _audioService;
        private readonly ISecurityService _securityService;

        public AudioController(IAudioService audioService, ISecurityService securityService)
        {
            _audioService = audioService;
            _securityService = securityService;
        }

        [HttpGet("positions/{id}/{chartId}")]
        public async Task<GetAudioStatsResponse> Get(int id, int chartId)
        {
            return await _audioService.GetAudioPositions(id, chartId);
        }

        [HttpGet("stats")]
        public Response Get(string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            return _audioService.WithoutCover();
        }

        [HttpPost("cover/{id}")]
        public Response Post(int id, string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            return _audioService.SetCover(id);
        }

        [HttpPost("cover_to_all")]
        public Response Post(string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            return _audioService.SetCoverToAll();
        }

        
    }
}
