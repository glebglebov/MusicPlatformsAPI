using System.Linq;
using KMChartsUpdater.BLL.Charts.Models;
using KMChartsUpdater.BLL.Infrastructure;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Utils;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;

namespace KMChartsUpdater.BLL.Charts
{
    public class AudioChartUpdater : ChartUpdater
    {
        public AudioChartUpdater(UnitOfWork uow, IApiAdapterFactory apiFactory) : base(uow, apiFactory)
        {

        }

        public override void Update(Chart chart)
        {
            var api = GetApiInstance(chart);

            var items = api?.GetChart(chart.Code);

            if (items == null)
                return;

            ChartFix lastChartFix = chart.ChartFixes
                .OrderBy(x => x.Updated)
                .LastOrDefault();

            var chartFix = CreateChartFix(chart);

            foreach (var item in items)
            {
                Audio audio = GetOrCreateAudio(item);

                var fix = UpdateTrackPosition(audio, lastChartFix, item.Position);

                //System.Diagnostics.Debug.WriteLine(item.Position + ". " + audio.Artist + " - " + audio.Title);

                fix.ChartFix = chartFix;
                fix.Chart = chart;
                fix.Date = chartFix.Updated;

                Uow.PositionFixes.Add(fix);
            }

            Uow.Save();
        }

        public Audio GetOrCreateAudio(UnifiedAudioModel item)
        {
            string artistNormalized = Normalizer.ArtistNormalize(item.Artist);
            string titleNormalized = Normalizer.TitleNormalize(item.Title);

            Audio audio = Uow.Audios.GetAll
                .AsEnumerable()
                .FirstOrDefault(x => Normalizer.ArtistEqual(x.ArtistNormalized, artistNormalized)
                                     && x.TitleNormalized == titleNormalized);

            if (audio == null)
            {
                audio = CreateAudio(item, artistNormalized, titleNormalized);

                Uow.Audios.Add(audio);

                string labels = GetLabel(artistNormalized, titleNormalized);

                if (labels != null)
                    SetLabels(audio, labels);

                Uow.Save();
            }

            return audio;
        }

        private Audio CreateAudio(UnifiedAudioModel item, string artistNormalized, string titleNormalized)
        {
            string thumb = ImgSaver.SaveFromUrl("/Uploads/", item.ThumbUrl);
            //string thumb = null;

            Audio audio = new Audio
            {
                Artist = item.Artist,
                Title = item.Title,
                Subtitle = item.Subtitle,
                ThumbUrl = item.ThumbUrl,
                SavedThumb = thumb,
                IsExplicit = item.IsExplicit,
                ArtistNormalized = artistNormalized,
                TitleNormalized = titleNormalized
            };

            return audio;
        }

        private string GetLabel(string artist, string title)
        {
            var appleMusicApi = ApiFactory.CreateAppleMusicApiAdapter(false);
            var album = appleMusicApi.SearchAlbum(artist, title);

            string label = album?.Attributes.RecordLabel;

            return label;
        }

        private PositionFix UpdateTrackPosition(Audio item, ChartFix lastChartFix, int position)
        {
            var last = (lastChartFix != null)
                ? item.PositionFixes?.FirstOrDefault(x => x.Chart == lastChartFix.Chart)
                : null;

            bool isNew;
            int shift;

            if (last == null)
            {
                isNew = true;
                shift = 0;
            }
            else
            {
                var prevFix = lastChartFix?.PositionFixes
                    ?.FirstOrDefault(x => x.Audio == item);

                if (prevFix != null)
                {
                    isNew = false;
                    shift = prevFix.Position - position;
                }
                else
                {
                    isNew = false;
                    shift = 999;
                }
            }

            var positionFix = new PositionFix
            {
                Audio = item,
                Position = position,
                IsNew = isNew,
                Shift = shift
            };

            return positionFix;
        }

        private void SetLabels(Audio audio, string labels)
        {
            string[] labelNames = labels.Split('/');

            foreach (var labelName in labelNames)
            {
                string name = labelName.Trim();

                Label label = Uow.Labels.GetAll.FirstOrDefault(x => x.Name == name);

                if (label is null)
                {
                    label = new Label { Name = name };
                    Uow.Labels.Add(label);
                }

                LabelToAudio lta = new LabelToAudio
                {
                    Label = label,
                    Audio = audio
                };

                Uow.LabelToAudios.Add(lta);
            }
        }
    }
}
