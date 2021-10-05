using System;
using System.Collections.Generic;
using System.Text;

namespace KMChartsUpdater.BLL.Responses
{
    public class ErrorResponse : Response
    {
        public string Error { get; set; }

        public ErrorResponse()
        {

        }

        public ErrorResponse(string text)
        {
            Error = text;
        }
    }
}
