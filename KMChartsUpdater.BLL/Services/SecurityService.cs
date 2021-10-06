using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;
using System.Linq;

namespace KMChartsUpdater.BLL.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly UnitOfWork _uow;

        public SecurityService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public bool CheckToken(string token)
        {
            var query = _uow.AccessTokens.GetAll.FirstOrDefault(x => x.Token == token);

            return (query != null);
        }

        public GlobalStatsResponse GetGlobalStats()
        {
            var pf = _uow.PositionFixes.GetAll.Count();
            var cf = _uow.ChartFixes.GetAll.Count();
            var a = _uow.Audios.GetAll.Count();

            var response = new GlobalStatsResponse
            {
                PoisitonFixesCount = pf,
                ChartFixesCount = cf,
                Audios = a
            };

            _uow.Playlists.Add(new Playlist {Link = "aaa", Name = "eeee"});
            _uow.Save();

            return response;
        }

        public Response CleanAudios()
        {
            var audios = _uow.Audios.GetAll.ToList();

            foreach (var audio in audios)
            {
                if (audio.PositionFixes.Count < 1)
                {
                    foreach (var lta in audio.LabelToAudios.ToList())
                    {
                        _uow.LabelToAudios.Remove(lta.Id);
                    }

                    _uow.Audios.Remove(audio.Id);

                    System.Diagnostics.Debug.WriteLine("Deleted");
                }
            }

            _uow.Save();

            return new DoneResponse("clean");
        }

        public void ChangeAudioInFixes(int audio1Id, int audio2Id, int from, int to)
        {
            Audio audio1 = _uow.Audios.Get(audio1Id);
            Audio audio2 = _uow.Audios.Get(audio2Id);
            if (audio1 == null || audio2 == null)
                return;

            int i = from - 1;
            while (i <= to)
            {
                ++i;

                ChartFix fix = _uow.ChartFixes.Get(i);
                if (fix == null)
                    continue;

                PositionFix pos = fix.PositionFixes.FirstOrDefault(x => x.Audio == audio1);
                if (pos == null)
                    continue;

                pos.Audio = audio2;

                _uow.PositionFixes.Update(pos);
            }

            _uow.Save();
        }
    }
}
