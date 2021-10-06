using System;
using System.Collections.Generic;
using System.Linq;
using KMChartsUpdater.BLL.Charts;
using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Infrastructure;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.ReportGenerator;
using KMChartsUpdater.BLL.Responses;
using KMChartsUpdater.BLL.Utils;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;
using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.Services
{
    public class TaskService : ITaskService
    {
        private readonly UnitOfWork _uow;

        public TaskService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public Response DeleteTask(int id)
        {
            var task = _uow.AudioTasks.Get(id);

            if (task == null || !task.IsActive)
                return new ErrorResponse("task does not exist");

            task.IsActive = false;

            _uow.AudioTasks.Update(task);
            _uow.Save();

            return new DoneResponse("deleted");
        }

        public Response GetAccountTasks(Account account)
        {
            List<AudioTaskDto> tasks = new List<AudioTaskDto>();

            foreach (var task in account.AudioTasks)
            {
                if (!task.IsActive)
                    continue;

                tasks.Add(new AudioTaskDto
                {
                    Id = task.Id,
                    Artist = task.Audio.Artist,
                    Title = task.Audio.Title,
                    Cover = task.Audio.SavedThumb
                });
            }

            tasks.Reverse();

            var response = new GetAccountTasksResponse
            {
                Tasks = tasks
            };

            return response;
        }

        public Response GetTask(int id, Account account)
        {
            var task = _uow.AudioTasks.Get(id);

            if (task == null || task.Account != account || !task.IsActive)
                return new ErrorResponse("task does not exist");

            List<ReportDto> reports = new List<ReportDto>();

            foreach (var report in task.Reports)
            {
                reports.Add(new ReportDto
                {
                    Name = report.Name,
                    Filename = report.FileName,
                    FilePath = report.FilePath,
                    Updated = report.Updated
                });
            }

            reports.Reverse();

            var taskDto = new AudioTaskDto
            {
                Id = task.Id,
                Artist = task.Audio.Artist,
                Title = task.Audio.Title,
                Cover = task.Audio.SavedThumb
            };

            return new GetTaskDetailsResponse
            {
                Task = taskDto,
                Reports = reports
            };
        }

        public void CreateTaskFromPlaylist(Account account, string playlistId)
        {
            var audios = GetAudiosFromPlaylist(playlistId);

            foreach (var audio in audios)
            {
                if (IsThisTaskAlreadyExists(audio, account))
                    continue;

                AudioTask task = new AudioTask
                {
                    IsActive = true,
                    Account = account,
                    Audio = audio,
                    Created = DateTime.Now
                };

                _uow.AudioTasks.Add(task);
            }

            _uow.Save();
        }

        public void UpdateActiveTasks()
        {
            var query = from t in _uow.AudioTasks.GetAll
                where t.IsActive == true 
                select t;

            var tasks = query.ToList();

            var playlists = _uow.Playlists.GetAll.ToList();

            foreach (var task in tasks)
            {
                var report = CreateReport(task, playlists);

                _uow.Reports.Add(report);
            }

            _uow.Save();
        }

        private bool IsThisTaskAlreadyExists(Audio audio, Account account)
        {
            var tasks = from t in _uow.AudioTasks.GetAll
                where t.Audio == audio && t.Account == account && t.IsActive
                select t.Id;

            return tasks.Any();
        }

        private List<Audio> GetAudiosFromPlaylist(string playlistId)
        {
            var vkApi = ApiFactory.Instance.VkApi;
            vkApi.Auth();

            var unifiedPlaylist = vkApi.GetPlaylist(playlistId);

            var updater = new AudioChartUpdater(_uow);

            List<Audio> audios = new List<Audio>();

            foreach (var unifiedAudio in unifiedPlaylist.Audios)
            {
                var audio = updater.GetOrCreateAudio(unifiedAudio);
                audios.Add(audio);
            }

            return audios;
        }

        private Report CreateReport(AudioTask task, List<Playlist> playlists)
        {
            string reportTitle = task.Audio.Artist + " - " + task.Audio.Title;
            string reportSubtitle = DateTime.Now.ToString("dd.MM.yyyy");

            var reportContent = new ReportContent
            {
                ReportTitle = reportTitle,
                ReportSubtitle = reportSubtitle,
                Groups = new List<ReportGroup>()
            };

            var platforms = _uow.Platforms.GetAll.ToList();

            string titleNormalized = task.Audio.TitleNormalized;
            string artistNormalized = task.Audio.ArtistNormalized;

            foreach (var platform in platforms)
            {
                var group = new ReportGroup
                {
                    Name = platform.Name,
                    Elements = new List<ReportElement>()
                };

                foreach (var playlist in platform.Playlists)
                {
                    var tracks = JsonConvert.DeserializeObject<List<PlaylistAudioDto>>(playlist.Tracks);

                    var track = tracks?.FirstOrDefault(x =>
                        x.TitleNormalized == titleNormalized
                        && Normalizer.ArtistEqual(artistNormalized, x.ArtistNormalized));

                    if (track != null)
                    {
                        System.Diagnostics.Debug.WriteLine("found");

                        group.Elements.Add(new ReportElement
                        {
                            PlaylistName = playlist.Name,
                            PlaylistCoverPath = playlist.Cover,
                            PlaylistLink = playlist.Link,
                            TrackPosition = track.Position
                        });
                    }
                }

                reportContent.Groups.Add(group);
            }

            string reportName = DateTime.Now.ToString("dd.MM.yyyy");

            string artistAndTitle = artistNormalized.Replace(' ', '-') + "--"
                                    + titleNormalized.Replace(' ', '-');

            string filename = artistAndTitle + "__" + DateTime.Now.ToString("dd-MM-yyyy") + "_report.pdf";

            var report = new Report
            {
                AudioTask = task,
                Name = reportName,
                FileName = filename,
                FilePath = "/Uploads/Reports/" + filename,
                Updated = DateTime.Now
            };

            var pdf = new PdfReport(reportContent);
            pdf.CreateAndSave(filename);

            return report;
        }
    }
}
