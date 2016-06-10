using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LODM.highlights.Services;
using LODM.highlights.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LODM.highlights.Controllers
{
    public class DestinyController : Controller
    {
        private readonly IHighlightService _highlightService;
        private readonly AppSettings _appSettings;

        public DestinyController(IHighlightService highlightService, IOptions<AppSettings> appSettings)
        {
            _highlightService = highlightService;
            _appSettings = appSettings.Value;
        }

        [Route("/Destiny")]
        public async Task<IActionResult> Destiny(bool? disableCache)
        {
            var highlightList = new List<HighlightList>();//await _highlightService.GetOverwatchHighlightsAsync("KingxMurphy",User, disableCache ?? false);
            foreach (var member in _appSettings.Members)
            {
                highlightList.Add(
                    await _highlightService.GetOverwatchHighlightsAsync(member.GamerTag, User, disableCache ?? false));
            }

            return View(new DestinyViewModel
            {
                MembersList = _appSettings.Members,
                HighlightsList = highlightList
            });
        }
    }
}