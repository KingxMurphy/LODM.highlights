using System.Collections.Generic;
using System.Threading.Tasks;
using LODM.highlights.Services;
using LODM.highlights.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using LODM.highlights.Services.Interfaces;

namespace LODM.highlights.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHighlightService _highlightService;
        private readonly IMember _memberService;
        private readonly AppSettings _appSettings; 

        public HomeController(IHighlightService highlightService, IOptions<AppSettings> appSettings, IMember memberService)
        {
            _highlightService = highlightService;
            _appSettings = appSettings.Value;
            _memberService = memberService;
        }

        [Route("/")]
        public async Task<IActionResult> Index(bool? disableCache)
        {
            ViewData["SelectedMember"] = "noone";
            var highlightList = new List<HighlightList>();//await _highlightService.GetOverwatchHighlightsAsync("KingxMurphy",User, disableCache ?? false);
            var allMembers = _memberService.GetAll();
            foreach (var member in allMembers)
            {
                highlightList.Add(
                    await _highlightService.GetOverwatchHighlightsAsync(member.GamerTag, User, disableCache ?? false));
            }

            return View(new HomeViewModel
            {
                MembersList = allMembers,
                HighlightsList = highlightList               
            });
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Route("/error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
