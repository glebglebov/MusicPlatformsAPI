using KMChartsUpdater.BLL.Responses;
using KMChartsUpdater.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Web;
using KMChartsUpdater.BLL.DTO;

namespace KMChartsUpdater.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("{query}")]
        public Response Get(string query, int page = 0, string type = "charts")
        {
            query = HttpUtility.UrlDecode(query);

            if (query.Length < 2 || query.Length > 50 || page > 10)
                return new ErrorResponse("Запрос может быть от 2 до 50 символов.");

            if (type == "charts")
            {
                return _searchService.Search(query, page);
            }
            else if (type == "playlists")
            {
                return _searchService.SearchInPlaylists(query, page);
            }
            else
            {
                return new ErrorResponse("invalid type");
            }
        }

        [HttpGet("stats/{id}")]
        public ICollection<ItemStatsDto> Get(int id)
        {
            return _searchService.GetStats(id);
        }
    }
}
