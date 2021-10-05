using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using System.Linq;
using AngleSharp.Dom;
using System.Text.RegularExpressions;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;

namespace MooscleParser
{
    public class Parser
    {
        private readonly UnitOfWork _uow;

        public Parser(UnitOfWork uow)
        {
            _uow = uow;
        }

        public void ParseByDate(int day, int month, int year)
        {
            Chart vkChart = _uow.Charts.Get(4);

            DateTime dateTime = new DateTime(year, month, day);
            string date = dateTime.ToString("dd.MM.yyyy");

            ChartFix fix = new ChartFix
            {
                Updated = dateTime,
                NormalDate = date,
                Chart = vkChart
            };

            _uow.ChartFixes.Add(fix);
            _uow.Save();

            string url1 = $"https://mooscle.com/charts/?date={date}&type=song&tab=9";
            string url2 = $"https://mooscle.com/wp/wp-admin/admin-ajax.php?date={date}&type=song&tab=9&action=chart_items_more&customer_id=9";

            //System.Diagnostics.Debug.WriteLine(url1 + ". " + date);

            string html = Load(url1).Result;

            var parser = new HtmlParser();

            var document = parser.ParseDocument(html);
            var chart = document.QuerySelector("[id='9']");

            MakePositionFixesFromHtml(chart, fix);

            string json = Load(url2).Result;
            html = JObject.Parse(json)["content"].ToString();
            html = html.Replace(@"\", "");

            document = parser.ParseDocument(html);
            chart = document.QuerySelector("div.charts-items-more");

            MakePositionFixesFromHtml(chart, fix);
        }

        private void MakePositionFixesFromHtml(IElement dom, ChartFix fix)
        {
            var items = dom.QuerySelectorAll("div.chart-list-item");

            foreach (var item in items)
            {
                bool isNew = false;
                int shift;

                var position = Convert.ToInt32(item.QuerySelector("div.position-wrap").TextContent);

                var prevPos = item.QuerySelector("div.prev-position-wrap span");

                if (prevPos == null)
                {
                    shift = Convert.ToInt32(item.QuerySelector("div.prev-position-wrap").TextContent);
                }
                else
                {
                    shift = 0;
                    if (prevPos.TextContent == "new") isNew = true;
                }

                var positionInfo = item.QuerySelector("div.position-info");

                var link = item.QuerySelector("a").GetAttribute("href");

                string title = positionInfo.QuerySelector("div.title").TextContent;
                string titleNormalized = TitleNormalize(title);

                //string artist = link
                //    .Replace("https://vk.com/audio?q=", "")
                //    .Trim();

                //artist = Regex.Replace(artist, Regex.Escape(title), "".Replace("$", "$$"), RegexOptions.IgnoreCase);
                //artist = Regex.Replace(artist, @" ?\(.*?\)", string.Empty).Trim();

                string artist = positionInfo
                    .QuerySelector("div.meta div.inline")
                    ?.GetAttribute("data-original-title");

                if (artist is null)
                    artist = positionInfo.QuerySelector("div.meta div.inline").TextContent;

                string artistNormalized = ArtistNormalize(artist);

                Audio audio = _uow.Audios.GetAll
                    .FirstOrDefault(x => x.ArtistNormalized == artistNormalized && x.TitleNormalized == titleNormalized);

                if (audio is null)
                {
                    string label = positionInfo
                    .QuerySelector("div.meta div.label.main-v")
                    ?.GetAttribute("title");

                    if (label?.Length < 1)
                        label = positionInfo.QuerySelector("div.meta div.label.main-v").TextContent;

                    string thumb = item
                        .QuerySelector("div.thumbnail-wrap img")
                        .GetAttribute("src");

                    audio = new Audio
                    {
                        Artist = artist,
                        Title = title,
                        ThumbUrl = thumb,
                        ArtistNormalized = artistNormalized,
                        TitleNormalized = titleNormalized
                    };

                    _uow.Audios.Add(audio);

                    if (label != null)
                        SetLabels(audio, label);
                }

                PositionFix positionFix = new PositionFix
                {
                    ChartFix = fix,
                    Position = position,
                    IsNew = isNew,
                    Shift = shift,
                    Audio = audio,
                    Chart = fix.Chart,
                    Date = fix.Updated
            };

                _uow.PositionFixes.Add(positionFix);
            }

            _uow.Save();
        }

        public void ParseSpotify(int day, int month, int year)
        {
            Chart vkChart = _uow.Charts.Get(2);

            DateTime dateTime = new DateTime(year, month, day);
            DateTime dateTimeRu = dateTime.AddDays(1);

            string date = dateTimeRu.ToString("dd.MM.yyyy");
            string spotifyDate = dateTime.ToString("yyyy-MM-dd");

            ChartFix fix = new ChartFix
            {
                Updated = dateTimeRu,
                NormalDate = date,
                Chart = vkChart
            };

            _uow.ChartFixes.Add(fix);

            string url = $"https://spotifycharts.com/regional/ru/daily/{spotifyDate}";
            System.Diagnostics.Debug.WriteLine(url);

            string html = Load(url).Result;

            var parser = new HtmlParser();

            var document = parser.ParseDocument(html);
            var chart = document.QuerySelector("tbody");

            MakeSpotifyPositionFixesFromHtml(chart, fix);
        }

        private void MakeSpotifyPositionFixesFromHtml(IElement dom, ChartFix fix)
        {
            var items = dom.QuerySelectorAll("tr");

            int i = 1;
            foreach (var item in items)
            {
                if (i > 50)
                    break;

                bool isNew = false;
                int shift;

                int position = i;

                var prevPosDiv = item.QuerySelector("td.chart-table-trend div.chart-table-trend__icon svg");
                var prevPos = prevPosDiv.GetAttribute("fill");

                if (prevPos == "#84bd00")
                {
                    shift = 999;
                }
                else if(prevPos == "#bd3200")
                {
                    shift = -999;
                }
                else if(prevPos == "#3e3e40")
                {
                    shift = 0;
                }
                else
                {
                    shift = 0;
                    isNew = true;
                }

                var trackInfo = item.QuerySelector("td.chart-table-track");

                string title = trackInfo.QuerySelector("strong").TextContent;
                string titleNormalized = TitleNormalize(title);

                string artist = trackInfo.QuerySelector("span").TextContent;
                artist = artist.Replace("by ", "");
                string artistNormalized = ArtistNormalize(artist);

                Audio audio = _uow.Audios.GetAll
                    .FirstOrDefault(x => x.ArtistNormalized == artistNormalized && x.TitleNormalized == titleNormalized);

                if (audio is null)
                {
                    string thumb = item
                        .QuerySelector("td.chart-table-image a img")
                        .GetAttribute("src");

                    audio = new Audio
                    {
                        Artist = artist,
                        Title = title,
                        ThumbUrl = thumb,
                        ArtistNormalized = artistNormalized,
                        TitleNormalized = titleNormalized
                    };

                    _uow.Audios.Add(audio);
                }

                PositionFix positionFix = new PositionFix
                {
                    ChartFix = fix,
                    Position = position,
                    IsNew = isNew,
                    Shift = shift,
                    Audio = audio,
                    Chart = fix.Chart,
                    Date = fix.Updated
                };

                _uow.PositionFixes.Add(positionFix);

                ++i;
            }

            _uow.Save();

            
        }

        public string ArtistNormalize(string artist)
        {
            string normalized = artist
                .ToLower()
                .Replace("ё", "е")
                .Replace(",", " ")
                .Replace("&", " ")
                .Replace("feat.", " ")
                .Replace("feat", " ");

            Regex regex = new Regex("[ ]{2,}", RegexOptions.None);
            normalized = regex.Replace(normalized, " ");

            return normalized;
        }

        public string TitleNormalize(string title)
        {
            string normalized = title
                .ToLower()
                .Replace("ё", "е");

            normalized = Regex
                .Replace(normalized, @"(?:\(|\[)\b(?:prod\.?|prod\.? by|feat\.?|при уч\.?) \b.*?(?:\]|\))", "")
                .Trim();

            return normalized;
        }

        private async Task<string> Load(string url)
        {
            //string cookie = "_ga=GA1.2.315841056.1622474202; X-Mapping-kjhgfmmm=CB7D9AF271975ED18B8AC7F219033D2E; cf_chl_prog=a9; cf_clearance=4bef60d2d5f3c8f5ee8459957e8b9a0766317fc1-1625171968-0-250; _gid=GA1.2.1994532692.1625171971; __cf_bm=569d488f38c0e70db2f42620092cfeab6bb5bbe8-1625173069-1800-AVl28R7oYRRoyYFZaZwVABSxgaWqiyBYXOpv1Jd+hejPp/Pz2gFD+cbn6YJk5wVXEdPpJwhJpbd0vPTEgMkPhuBLPXR5uK6hSADQnz+NW9IZYn3/MgdSgK8pK3jzvzPcRg==; XSRF-TOKEN=eyJpdiI6IlJTVzVjMVhORENZZkhlQ3hIc3JRaEE9PSIsInZhbHVlIjoibTBSQjJTb01mTUxSWHo5b3grT1BVeTN1eG13UjQ2ajlKenIrZnFIWHdvcngybWtURFBMbWFPaDVlbEkxYVRwWExsS3dBYjFEbnE1ZlgxMitcL0FMYmlnPT0iLCJtYWMiOiIzOGMxMjUxNmRlZjc2NDEzOGZhYWM3OTUyYzVlOTkxNzU5ODNhMjViMTBlMzE2YjFjYmI4NDQyMjk2MDdmMWEzIn0%3D; laravel_session=eyJpdiI6InR2c2FMVFlUbENCV056VG1PSnNWb0E9PSIsInZhbHVlIjoiS21yWUNqNWh0UFNaMWhNd3VhWjBvVHU5RTZFQXJBSFRIdTlvZFQ2MW5QRHVWSFRjb1Q2cWhGazRnK1VPRHplOXRsYk1iSzBxZWpUN2k1OVJBWnVKa2c9PSIsIm1hYyI6ImIwM2JjMzZiZmQxNzlmOGUxODlkNzU5ZDFhYjIyN2E5NmJkZTc0NjMzYWNhMmE4ZjE0MmRmMDQ5MTYwY2FmZGYifQ%3D%3D; 71c600bb79e96c9a3b5b6f2caed342d40d8c2555=eyJpdiI6IkxFRVRGNWxVMXQ3UTAxcll0WkhMdFE9PSIsInZhbHVlIjoiVXNQaEJnRlFneW0wXC96OFBoYlwvSlVWUHp6UzdrQkFVczRPd3JrTlh1RzVvdlJITVNndEkyQVwvVEp3THkrdko5eGpIeCtDb25cL21vK09zaEFUS0FQTGZybmtDKzNnTHNtdk9jREdYZXdPN2kra0N3bXpnTlRDY2tndGVEQU1DaVlTWURHS0xoWHk2WGg1M1RXU0FFQWEyVlJRRlR1a1o2UnpLb0R1M1Vya0NOXC9vRFR2OG4zU1wvQmNkM05yXC9zVXRzZlQ5VGhBXC9nYWdza0x6XC9OaUN4eWJ2MnJmNzQ2SXBxQ01JMVhTU2Rhd1FZb1FMZlpcL1JFXC9hRkJON1JPMlJcL0FmSG5UNHhjcE4zNjFDNGZqRFlxdjI0WmYxYnB6em9JWFBUU1hhZTM2OHVRdis5ZFF4blwvZTBEZUk0SjVVWm1hVHA0bE9DXC94N0NQUWJBVktpbUdidlBiSlhXQXArN1llUnFTSXprWHNpVkc2R0dEKzVUU2U1VXAxR3dLK1pJTkhVZ3R1K1dkNlR1ZzFcL29TYittelFDQkpXVDloOUFXUUd0Ylg1WmJpdUhRbEpsUStuTW4xQVU2M2d6bVJoOEJndmVUZzhNcEhtRFwvdUY0XC9held0dHhOOStvYzFOM1Z4QTlxNk9WZEtrYVc1d3oyND0iLCJtYWMiOiIwOTE5NmRiZGYzMDZjNzU5ZDg1NjY0ZjQ0YjU4YTg1N2IwZDFhN2E4OGJhMjJiZDk1OGZlMjBkYTc2ZjQ4ODFjIn0%3D";
            
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.114 Safari/537.36");
            //client.DefaultRequestHeaders.Add("cookie", cookie);

            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            return responseBody;
        }

        private void SetLabels(Audio audio, string labels)
        {
            labels = labels.Replace(" / ", "/");
            string[] labelNames = labels.Split("/");

            foreach (var labelName in labelNames)
            {
                Label label = _uow.Labels.GetAll.FirstOrDefault(x => x.Name == labelName);

                if (label is null)
                {
                    label = new Label { Name = labelName };
                    _uow.Labels.Add(label);
                }

                LabelToAudio lta = new LabelToAudio
                {
                    Label = label,
                    Audio = audio
                };

                _uow.LabelToAudios.Add(lta);
            }
        }
    }
}
