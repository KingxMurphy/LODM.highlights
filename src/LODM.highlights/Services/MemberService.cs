using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LODM.highlights.Services.Interfaces;

namespace LODM.highlights.Services
{
    public class MemberService : IMember
    {
        //TODO: This is kind of cheating but I'm not using any type of DB or API 
        //If I were to use an API this is where I would do an HTTPGET and pass the route... something like "apiname/members"
        public IEnumerable<Member> GetAll()
        {
            return new List<Member>
            {
                new Member
                {
                    GamerTag = "KingxMurphy",
                    FirstName = "King",
                    LastName = "Murphy",
                    Description = "An avid gamer who will always find a way to steal all of your kills",
                    YouTubeApiKey = "",
                    YouTubePlaylistId = ""
                },
                new Member
                {
                    GamerTag = "Take2Chance",
                    FirstName = "Take",
                    LastName = "Achance",
                    Description = "An avid gamer who will always find a way to heal your team",
                    YouTubeApiKey = "",
                    YouTubePlaylistId = ""
                },
                new Member
                {
                    GamerTag = "TheYungJacques",
                    FirstName = "Jacques",
                    LastName = "Miiiiiigo",
                    Description = "An avid gamer who will always find a way to use a celebrity voice in game",
                    YouTubeApiKey = "",
                    YouTubePlaylistId = ""
                },
                new Member
                {
                    GamerTag = "JimmyPotato",
                    FirstName = "Jimmy",
                    LastName = "Potato",
                    Description = "An avid gamer who will always find a way to laugh as loud and as happily as he can",
                    YouTubeApiKey = "",
                    YouTubePlaylistId = ""
                },
            };
        }
    }
}
