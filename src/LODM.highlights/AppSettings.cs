using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LODM.highlights
{
    public class AppSettings
    {
        public AppSettings()
        {
            Members = new List<Member>
            {
                new Member
                {
                    GamerTag = "KingxMurphy",
                    FirstName = "King",
                    LastName = "Murphy",
                    YouTubeApiKey = "",
                    YouTubePlaylistId = ""
                },
                new Member
                {
                    GamerTag = "Take2Chance",
                    FirstName = "Take",
                    LastName = "Achance",
                    YouTubeApiKey = "",
                    YouTubePlaylistId = ""
                },
                new Member
                {
                    GamerTag = "TheYungJacques",
                    FirstName = "Jacques",
                    LastName = "Miiiiiigo",
                    YouTubeApiKey = "",
                    YouTubePlaylistId = ""
                },
                new Member
                {
                    GamerTag = "JimmyPotato",
                    FirstName = "Jimmy",
                    LastName = "Potato",
                    YouTubeApiKey = "",
                    YouTubePlaylistId = ""
                },
            };
        }
        public string YouTubeApplicationName { get; set; }
        public List<Member> Members { get; set; }
    }
}
