using System.Collections.Generic;
using KMChartsUpdater.BLL.DTO;

namespace KMChartsUpdater.BLL.Responses
{
    public class GetAccountTasksResponse : Response
    {
        public ICollection<AudioTaskDto> Tasks { get; set; }
    }
}
