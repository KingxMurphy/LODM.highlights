using System.Collections.Generic;
using System.Linq;
using LODM.highlights.Models.BioViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using LODM.highlights.Services.Interfaces;

namespace LODM.highlights.Controllers
{
    public class PlayerBioController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly IMember _memberService;

        public PlayerBioController(IOptions<AppSettings> appSettings, IMember memberService)
        {
            _appSettings = appSettings.Value;
            _memberService = memberService;
        }
        // GET: /PlayerBio/PlayerBio
        [HttpGet]
        public IActionResult PlayerBio(string selectedPlayerGamerTag)
        {
            var selectedPlayer = _memberService.GetAll().FirstOrDefault(x => x.GamerTag == selectedPlayerGamerTag);
            var playerBio = getPlayerBio(selectedPlayer);
            ViewData["SelectedMember"] = selectedPlayerGamerTag;

            return View(playerBio);
        }

        private static BioViewModel getPlayerBio(Member player)
        {
            if(player == null)
                return new BioViewModel();

            return new BioViewModel
            {
                PlayerBio = player,
                GameInformationList = new List<GameInformation>
                {
                    new GameInformation
                    {
                        Title = "Overwatch",
                        Publisher = "Blizzard",
                        Genre = "FPS",
                        BackgroundUrl = "overWatchbackground.png"
                    },
                    new GameInformation
                    {
                        Title = "Destiny",
                        Publisher = "Bungie",
                        Genre = "FPS Looter Shooter",
                        BackgroundUrl = "destinyBackground.jpg"
                    },
                    new GameInformation
                    {
                        Title = "Division",
                        Publisher = "Ubisoft",
                        Genre = "3rd person Cover Shooter Travesty",
                        BackgroundUrl = "tempBackground.jpg"
                    }
                }
            };
        }
    }
}