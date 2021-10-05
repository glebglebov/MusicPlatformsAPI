using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace KMChartsUpdater.BLL.Interfaces
{
    interface IChartParser
    {
        JObject GetChart();
    }
}
