using System;
using System.Collections.Generic;
using System.Text;
using KMChartsUpdater.BLL.DTO;

namespace KMChartsUpdater.BLL.Responses
{
    public class GetTaskDetailsResponse : Response
    {
        public AudioTaskDto Task { get; set; }

        public List<ReportDto> Reports { get; set; }
    }
}
