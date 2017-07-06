using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LODM.highlights.Services;
using LODM.highlights.Services.Interfaces;
using LODM.highlights.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LODM.highlights.Controllers
{
    public class DestinyController : Controller
    {
        private readonly IHighlightService _highlightService;
        private readonly IMember _memberService;
        private readonly AppSettings _appSettings;

        public DestinyController(IHighlightService highlightService, IOptions<AppSettings> appSettings, IMember memberService)
        {
            _highlightService = highlightService;
            _memberService = memberService;
            _appSettings = appSettings.Value;
        }

        [Route("/Destiny")]
        public async Task<IActionResult> Destiny(bool? disableCache)
        {
            var highlightList = new List<HighlightList>();//await _highlightService.GetOverwatchHighlightsAsync("KingxMurphy",User, disableCache ?? false);
            var allMembers = _memberService.GetAll();
            foreach (var member in allMembers)
            {
                highlightList.Add(
                    await _highlightService.GetOverwatchHighlightsAsync(member.GamerTag, User, disableCache ?? false));
            }

            return View(new DestinyViewModel
            {
                MembersList = allMembers,
                HighlightsList = highlightList
            });
        }
    }
}