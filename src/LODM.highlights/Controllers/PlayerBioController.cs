using System.Collections.Generic;
using System.Linq;
using LODM.highlights.Models.BioViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LODM.highlights.Controllers
{
    public class PlayerBioController : Controller
    {
        private readonly AppSettings _appSettings;

        public PlayerBioController(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        // GET: /PlayerBio/PlayerBio
        [HttpGet]
        public IActionResult PlayerBio(string selectedPlayerGamerTag)
        {
            var allPlayerBios = getPlayerBio(_appSettings.Members.FirstOrDefault(x => x.GamerTag == selectedPlayerGamerTag));
            return View(allPlayerBios);
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
                        Genre = "FPS"
                    },
                    new GameInformation
                    {
                        Title = "Destiny",
                        Publisher = "Bungie",
                        Genre = "FPS Looter Shooter"
                    },
                    new GameInformation
                    {
                        Title = "Division",
                        Publisher = "Ubisoft",
                        Genre = "3rd person Cover Shooter Travesty"
                    }
                }
            };
        }
    }
}