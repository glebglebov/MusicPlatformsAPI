
namespace KMChartsUpdater.BLL.Responses
{
    public class DoneResponse : Response
    {
        public string Text { get; set; }

        public DoneResponse()
        {

        }

        public DoneResponse(string text)
        {
            Text = text;
        }
    }
}
