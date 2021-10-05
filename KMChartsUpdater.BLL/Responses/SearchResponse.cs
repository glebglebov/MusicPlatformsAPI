using KMChartsUpdater.BLL.DTO;
using System.Collections.Generic;

namespace KMChartsUpdater.BLL.Responses
{
    public class SearchResponse<T> : Response
    {
        public string Query { get; set; }

        public int Count { get; set; }

        public int Pages { get; set; }

        public string ItemsType { get; set; }

        public ICollection<T> Items { get; set; }
    }
}
