using KMChartsUpdater.BLL.DTO;
using KMChartsUpdater.BLL.Responses;
using System.Collections.Generic;

namespace KMChartsUpdater.BLL.Interfaces
{
    public interface ISearchService
    {
        SearchResponse<ItemDto> Search(string query, int page);

        SearchResponse<PlaylistWithTracksDto> SearchInPlaylists(string query, int page);

        ICollection<ItemStatsDto> GetStats(int audioId);
    }
}
