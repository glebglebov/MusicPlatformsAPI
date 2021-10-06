using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KMChartsUpdater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IAudioService _audioService;

        public SecretController(ISecurityService securityService, IAudioService audioService)
        {
            _securityService = securityService;
            _audioService = audioService;
        }

        [HttpGet("stats")]
        public Response Get(string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            return _securityService.GetGlobalStats();
        }

        [HttpPost("merge/{one}/{two}")]
        public Response Post(int one, int two, string token, int chart_id = 0)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            _audioService.ChangeAudio(one, two, chart_id);

            return new DoneResponse("ok");
        }

        [HttpPost("merge_all")]
        public Response Post(string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            _audioService.MergeAll();

            return new DoneResponse("ok");
        }

        [HttpDelete("clean")]
        public Response Delete(string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            return _securityService.CleanAudios();
        }

        [HttpPost("change_in_fix/{audio1Id}/{audio2Id}")]
        public Response Post(int audio1Id, int audio2Id, int from, int to, string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            _securityService.ChangeAudioInFixes(audio1Id, audio2Id, from, to);

            return new DoneResponse("ok");
        }

        [HttpPost("rename_artist/{audioId}")]
        public Response Post(int audioId, string name, string token)
        {
            bool b = _securityService.CheckToken(token);
            if (!b)
                return new ErrorResponse("wrong");

            _audioService.RenameArtist(audioId, name);

            return new DoneResponse("ok");
        }
    }
}
