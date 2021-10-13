using System;
using System.Collections.Generic;
using System.Text;

namespace KMChartsUpdater.BLL.ReportGenerator
{
    public class Group<T>
    {
        public string Name { get; set; }

        public List<T> Items { get; set; }
    }
}
