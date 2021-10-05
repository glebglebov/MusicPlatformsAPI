using System;
using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Infrastructure;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using KMChartsUpdater.BLL.Utils;
using Newtonsoft.Json;

namespace KMChartsUpdater.BLL.Services
{
    public class SearchService : ISearchService
    {
        private readonly UnitOfWork _uow;

        public SearchService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public SearchResponse<ItemDto> Search(string query, int page)
        {
            int offset = page * 10;

            string queryNormalized = Normalizer.ArtistNormalize(query);
            queryNormalized = Normalizer.TitleNormalize(queryNormalized);

            var search = _uow.Audios.GetAll
                .Where(x => x.ArtistNormalized.Contains(queryNormalized) || x.TitleNormalized.Contains(queryNormalized))
                .Take(100);
                
            int count = search.Count();
            int pages = (int) Math.Ceiling((double)(count) / 10);

            var audios = search
                .OrderByDescending(x => x.PositionFixes.Count)
                .Skip(offset)
                .Take(10)
                .ToList();

            List<ItemDto> items = new List<ItemDto>();

            foreach (var audio in audios)
            {
                List<LabelDto> labels = new List<LabelDto>();

                foreach (var lta in audio.LabelToAudios)
                {
                    LabelDto l = new LabelDto
                    {
                        Id = lta.Label.Id,
                        Name = lta.Label.Name
                    };

                    labels.Add(l);
                }

                var item = new ItemDto
                {
                    Id = audio.Id,
                    Artist = audio.Artist,
                    Title = audio.Title,
                    Subtitle = audio.Subtitle,
                    ThumbUrl = audio.SavedThumb,
                    IsExplicit = audio.IsExplicit,
                    Labels = labels
                };

                items.Add(item);
            }

            return new SearchResponse<ItemDto>
            {
                Query = query,
                Count = count,
                Pages = pages,
                ItemsType = "tracks",
                Items = items
            };
        }

        public SearchResponse<PlaylistWithTracksDto> SearchInPlaylists(string query, int page)
        {
            int offset = page * 10;

            string queryNormalized = Normalizer.ArtistNormalize(query);
            queryNormalized = Normalizer.TitleNormalize(queryNormalized);

            var playlists = _uow.Playlists.GetAll.ToList();

            var items = new List<PlaylistWithTracksDto>();

            const int maxCount = 100;
            int count = 0;

            foreach (var playlist in playlists)
            {
                var allTracks = JsonConvert.DeserializeObject<List<PlaylistAudioDto>>(playlist.Tracks);

                string pattern = @"\b" + queryNormalized + @"\b";

                var tracks = allTracks
                    ?.Where(x =>
                        Regex.IsMatch(x.TitleNormalized, pattern)
                        || Regex.IsMatch(x.ArtistNormalized, pattern))
                    .ToList();

                if (tracks == null || tracks.Count < 1)
                    continue;

                items.Add(new PlaylistWithTracksDto
                {
                    Name = playlist.Name,
                    Link = playlist.Link,
                    Cover = playlist.Cover,
                    Tracks = tracks
                });

                count += tracks.Count;

                if (count >= maxCount)
                    break;
            }

            return new SearchResponse<PlaylistWithTracksDto>
            {
                Query = query,
                Count = count,
                Pages = 0,
                ItemsType = "playlists",
                Items = items
            };
        }

        public ICollection<ItemStatsDto> GetStats(int audioId)
        {
            ICollection<ItemStatsDto> items = new List<ItemStatsDto>();

            Audio audio = _uow.Audios.Get(audioId);
            if (audio is null)
                return items;

            var groups = audio.PositionFixes
                .GroupBy(x => x.Chart);

            foreach (var group in groups)
            {
                int best = group.Min(x => x.Position);
                int days = group.Count();

                var sorted = group.OrderBy(x => x.Date);
                string first = sorted.FirstOrDefault().Date.ToString("dd.MM.yyyy");
                int firstPlace = sorted.FirstOrDefault().Position;

                var item = new ItemStatsDto
                {
                    ChartId = group.Key.Id,
                    Chart = group.Key.Name,
                    Best = best,
                    Days = days,
                    First = first,
                    FirstPlace = firstPlace
                };

                items.Add(item);
            }

            return items;
        }
    }
}
