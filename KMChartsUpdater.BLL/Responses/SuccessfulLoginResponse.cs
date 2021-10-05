namespace KMChartsUpdater.BLL.Responses
{
    public class SuccessfulLoginResponse : Response
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Key { get; set; }
    }
}
