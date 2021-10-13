using KMChartsUpdater.BLL.Config.Models;

namespace KMChartsUpdater.BLL.Config
{
    public class MyConfig
    {
        public ChartsData Charts { get; set; }

        public Credentials Credentials { get; set; }

        public Proxy[] Proxies { get; set; }
    }
}
