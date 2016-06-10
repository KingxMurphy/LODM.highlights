using System.Collections.Generic;
using System.Threading.Tasks;
using LODM.highlights.Services;
using LODM.highlights.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LODM.highlights.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHighlightService _highlightService;
        private readonly AppSettings _appSettings; 

        public HomeController(IHighlightService highlightService, IOptions<AppSettings> appSettings )
        {
            _highlightService = highlightService;
            _appSettings = appSettings.Value;
        }

        [Route("/")]
        public async Task<IActionResult> Index(bool? disableCache)
        {

            var highlightList = new List<HighlightList>();//await _highlightService.GetOverwatchHighlightsAsync("KingxMurphy",User, disableCache ?? false);
            foreach (var member in _appSettings.Members)
            {
                highlightList.Add(
                    await _highlightService.GetOverwatchHighlightsAsync(member.GamerTag, User, disableCache ?? false));
            }

            return View(new HomeViewModel
            {
                MembersList = _appSettings.Members,
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
