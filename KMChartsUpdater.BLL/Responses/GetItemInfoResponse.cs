using System;
using System.Collections.Generic;
using System.Text;

namespace KMChartsUpdater.BLL.Responses
{
    public class GetItemInfoResponse<T>
    {
        public T ItemInfo { get; set; }
    }
}
