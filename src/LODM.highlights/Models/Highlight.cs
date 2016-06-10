using System;

namespace LODM.highlights.Models
{
    public class Highlight
    {
        public int Id { get; set; }

        public string Provider { get; set; }

        public string ProviderId { get; set; }

        public string Title { get; set; }

        public bool HasTitle => !string.IsNullOrEmpty(Title);

        public string Description { get; set; }

        public DateTimeOffset HighlightDate { get; set; }

        public bool IsNew => (DateTimeOffset.Now - HighlightDate).TotalDays <= 7;

        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }
    }
}
