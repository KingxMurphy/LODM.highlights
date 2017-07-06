using System.Collections.Generic;
using System.Linq;
using LODM.highlights.Models;
using LODM.highlights.Services;

namespace LODM.highlights.ViewModels
{
    public class DestinyViewModel
    {
        public IEnumerable<Member> MembersList { get; set; }
        public List<HighlightList> HighlightsList { get; set; }
        public bool ShowPreviousShows => HighlightsList.Count > 0;
        public string MoreShowsUrl { get; set; }
        public bool ShowMoreShowsUrl => !string.IsNullOrEmpty(MoreShowsUrl);
        public string DestinyStatsUrl => "https://www.bungie.com";
        public List<Highlight> ReturnHighlightsByGamerTag(string gamerTag)
        {
            var gamerHighlights = HighlightsList.FirstOrDefault(x => x.GamerTag == gamerTag);
            MoreShowsUrl = gamerHighlights?.MoreHighlightsUrl ?? "https://www.google.com";
            return gamerHighlights?.Highlights.ToList();
        }

    }
}
