using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Responses;
using System.Collections.Generic;
using KMChartsUpdater.BLL.ReportGenerator;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface ISearchService
    {
        SearchResponse<ItemDto> Search(string query, int page);

        SearchResponse<Group<PlaylistWithTracksDto>> SearchInPlaylists(string query, int page);

        ICollection<ItemStatsDto> GetStats(int audioId);
    }
}
