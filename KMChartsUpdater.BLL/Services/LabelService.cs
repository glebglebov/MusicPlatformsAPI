using AppleMusicApi;
using KMChartsUpdater.BLL.Config;
using KMChartsUpdater.BLL.Infrastructure;
using KMChartsUpdater.BLL.Interfaces;
using KMChartsUpdater.BLL.Responses;
using KMChartsUpdater.DAL;
using KMChartsUpdater.DAL.Entities;
using System;
using System.Linq;

namespace KMChartsUpdater.BLL.Services
{
    public class LabelService : ILabelService
    {
        private readonly UnitOfWork _uow;

        public LabelService(UnitOfWork uow)
        {
            _uow = uow;
        }

        public Response GetLabel(int audioId)
        {
            Audio audio = _uow.Audios.Get(audioId);
            if (audio == null)
                return new ErrorResponse("wrong audio id");

            string[] labels = GetLabels(audio);
            string label = string.Join(", ", labels);

            return new DoneResponse(label);
        }

        public Response SetLabels(int audioId, string name)
        {
            Audio audio = _uow.Audios.Get(audioId);
            if (audio == null)
                return new ErrorResponse("wrong audio id");

            string[] labels = name.Split(',');

            if (labels != null)
                SetLabels(audio, labels);

            return new DoneResponse("done");
        }

        public Response SetLabels(int audioId)
        {
            Audio audio = _uow.Audios.Get(audioId);
            if (audio == null)
                return new ErrorResponse("wrong audio id");

            string[] labels = GetLabels(audio);

            if (labels != null)
                SetLabels(audio, labels);

            SetLabels(audio, labels);

            return new DoneResponse("done");
        }

        public Response RemoveLabels(int audioId)
        {
            Audio audio = _uow.Audios.Get(audioId);
            if (audio == null)
                return new ErrorResponse("wrong audio id");

            var ltas = audio.LabelToAudios.ToList();

            foreach (var lta in ltas)
            {
                _uow.LabelToAudios.Remove(lta.Id);
            }

            _uow.Save();

            return new DoneResponse("done");
        } 

        public Response SetLabelsForFix(int fixId)
        {
            ChartFix fix = _uow.ChartFixes.Get(fixId);
            if (fix == null)
                return new ErrorResponse("wrong fix id");

            foreach (var posFix in fix.PositionFixes.ToList())
            {
                try
                {
                    Audio audio = posFix.Audio;

                    if (audio.LabelToAudios.Count > 0)
                        continue;

                    string[] labels = GetLabels(audio);

                    if (labels != null)
                        SetLabels(audio, labels);
                }
                catch(Exception)
                {
                    continue;
                }
            }

            return new DoneResponse("done");
        }

        public Response SetLabelsToAll()
        {
            var audios = (from a in _uow.Audios.GetAll
                         where a.LabelToAudios.Count < 1
                         select a).ToList();

            foreach (var audio in audios)
            {
                if (audio.LabelToAudios.Count > 0)
                    continue;

                string[] labels = GetLabels(audio);

                if (labels != null)
                    SetLabels(audio, labels);
            }

            return new DoneResponse("done");
        }

        public Response RemoveAll()
        {
            var ltas = _uow.LabelToAudios.GetAll.ToList();

            int i = 0;
            foreach (var lta in ltas)
            {
                _uow.LabelToAudios.Remove(lta.Id);

                ++i;

                if (i % 1000 == 0) _uow.Save();
            }

            _uow.Save();

            return new DoneResponse("done");
        }
        
        private string[] GetLabels(Audio audio)
        {
            return null;
        }

        private void SetLabels(Audio audio, string[] labelNames)
        {
            foreach (var labelName in labelNames)
            {
                string name = labelName.Trim();

                Label label = _uow.Labels.GetAll.FirstOrDefault(x => x.Name == name);

                if (label is null)
                {
                    label = new Label { Name = name };
                    _uow.Labels.Add(label);
                }

                LabelToAudio lta = new LabelToAudio
                {
                    Label = label,
                    Audio = audio
                };

                _uow.LabelToAudios.Add(lta);
            }

            _uow.Save();
        }

        private Audio GetAudio(int id)
        {
            Audio audio = _uow.Audios.Get(id);

            return audio;
        }
    }
}
