using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LODM.highlights.Models;

namespace LODM.highlights.Services
{
    public class HighlightList
    {
        public string GamerTag { get; set; }
        public IList<Highlight> Highlights { get; set; }
        public string MoreHighlightsUrl { get; set; }
    }
}
