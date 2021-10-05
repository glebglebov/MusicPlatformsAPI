using System.Collections.Generic;

namespace KMChartsUpdater.BLL.DTO
{
    public class ItemDto
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Artist { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string ThumbUrl { get; set; }

        public bool IsExplicit { get; set; }

        public int Year { get; set; }

        public string Genre { get; set; }

        public ICollection<LabelDto> Labels { get; set; }
    }
}
